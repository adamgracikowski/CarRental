using System.Net.Http.Headers;

namespace CarRental.Comparer.Infrastructure.CarProviders.Authorization;

public sealed class TokenAuthorizationHandler : DelegatingHandler
{
    private readonly ITokenService tokenService;

    public TokenAuthorizationHandler(ITokenService tokenService)
    {
        this.tokenService = tokenService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await tokenService.GetTokenAsync(cancellationToken);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }
}