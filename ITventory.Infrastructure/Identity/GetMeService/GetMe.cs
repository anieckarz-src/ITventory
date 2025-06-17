using ITventory.Shared.Abstractions.Queries;

namespace ITventory.Infrastructure.Identity.GetMeService;

public sealed record GetMe : IQuery<GetMeResponse>;

public sealed record GetMeResponse(
    string Id,
    string Username,
    string Email
);