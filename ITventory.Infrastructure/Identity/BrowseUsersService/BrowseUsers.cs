using ITventory.Shared.Abstractions.Queries;

namespace ITventory.Infrastructure.Identity.BrowseUsersService;

public sealed record BrowseUsers : IQuery<BrowseUsersResponse>;

public sealed record UserDto(
    string Id,
    string? Username,
    string? Email
);

public sealed record BrowseUsersResponse(
    IEnumerable<UserDto> Users
);