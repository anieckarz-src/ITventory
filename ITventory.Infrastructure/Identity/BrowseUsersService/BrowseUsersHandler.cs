using System.Security.Claims;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ITventory.Infrastructure.Identity.BrowseUsersService;

internal sealed class BrowseUsersHandler(
    UserManager<UserIdentity> userManager,
    IHttpContextAccessor httpContextAccessor
)
    : IQueryHandler<BrowseUsers, BrowseUsersResponse>
{
    public async Task<BrowseUsersResponse> HandleAsync(BrowseUsers query)
    {
        var currentUserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(currentUserId)) throw new UnauthorizedAccessException("User is not authenticated.");

        var users = await userManager.Users
            .Where(user => user.Id != currentUserId)
            .Select(user => new UserDto(
                user.Id,
                user.NormalizedUserName,
                user.Email
            ))
            .ToListAsync();

        return new BrowseUsersResponse(users);
    }
}