using ITventory.Domain;
using ITventory.Domain.Repositories;
using ITventory.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Identity;

namespace ITventory.Infrastructure.Identity.RegistrationService;

public class RegisterUserHandler : ICommandHandler<RegisterUser>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly UserManager<UserIdentity> _userManager;
    private readonly UserManagerDbContext _userManagerDbContext;

    public RegisterUserHandler(IEmployeeRepository employeeRepository, UserManager<UserIdentity> userManager,
        UserManagerDbContext userManagerDbContext)
    {
        _employeeRepository = employeeRepository;
        _userManager = userManager;
        _userManagerDbContext = userManagerDbContext;
    }

    public async Task HandleAsync(RegisterUser command)
    {
        var (username, email, password) = command;
        if (await _employeeRepository.ExistsByUsername(username))
            throw new InvalidOperationException($"User with '{username}' username already exists");

        var identityUser = new UserIdentity
        {
            Email = email,
            UserName = username
        };

        using var transaction = await _userManagerDbContext.Database.BeginTransactionAsync();

        var identityResult = await _userManager.CreateAsync(identityUser, password);

        if (!identityResult.Succeeded)
        {
            // rollback automatically when disposing transaction without commit
            var errors = string.Join(", ", identityResult.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Identity user creation failed: {errors}");
        }

        var identityId = identityUser.Id;

        var domainUser = Employee.CreateMinimal(username, identityId);

        await _employeeRepository.AddAsync(domainUser);

        await _userManagerDbContext.SaveChangesAsync();
        await transaction.CommitAsync();
    }
}