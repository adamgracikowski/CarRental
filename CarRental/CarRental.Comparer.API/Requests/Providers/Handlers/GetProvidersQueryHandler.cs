﻿using Ardalis.Result;
using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Comparer.API.Requests.Providers.Queries;
using CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Providers;
using MediatR;

namespace CarRental.Comparer.API.Requests.Providers.Handlers;

public sealed class GetProvidersQueryHandler : IRequestHandler<GetProvidersQuery, Result<ProvidersDto>>
{
    private readonly IRepositoryBase<Provider> providersRepository;
    private readonly ILogger<GetProvidersQueryHandler> logger;

    public GetProvidersQueryHandler(IRepositoryBase<Provider> providersRepository,
        ILogger<GetProvidersQueryHandler> logger)
    {
        this.providersRepository = providersRepository;
        this.logger = logger;
    }

    public async Task<Result<ProvidersDto>> Handle(GetProvidersQuery request, CancellationToken cancellationToken)
    {
        var providers = await this.providersRepository.ListAsync(cancellationToken);

        if (providers == null || providers.Count == 0)
        {
			this.logger.LogWarning("No providers found.");
            return Result<ProvidersDto>.NotFound();
        }

        var providerDtoList = providers
            .Select(provider => new ProviderDto(provider.Id, provider.Name))
            .ToList();

        var providersDto = new ProvidersDto(providerDtoList);

        return Result<ProvidersDto>.Success(providersDto);
    }
}