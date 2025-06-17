using System.Security.Claims;
using ITventory.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ITventory.Infrastructure.Identity.GetMeService;

public sealed class GetMeHandler(
    UserManager<UserIdentity> userManager,
    IHttpContextAccessor httpContextAccessor
) : IQueryHandler<GetMe, GetMeResponse>
{
    public Task<GetMeResponse> HandleAsync(GetMe query)
    {
        var userId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) throw new UnauthorizedAccessException("User is not authenticated.");

        var user = userManager.FindByIdAsync(userId).Result;
        if (user == null) throw new InvalidOperationException($"User with ID '{userId}' not found.");

        return Task.FromResult(new GetMeResponse(
            user.Id,
            user.UserName,
            user.Email
        ));
    }
}