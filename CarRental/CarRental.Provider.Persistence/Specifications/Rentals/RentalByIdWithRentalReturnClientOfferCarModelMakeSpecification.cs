﻿using Ardalis.Specification;
using CarRental.Common.Core.ProviderEntities;

namespace CarRental.Provider.Persistence.Specifications.Rentals;

public sealed class RentalByIdWithRentalReturnClientOfferCarModelMakeSpecification : Specification<Rental>
{
    public RentalByIdWithRentalReturnClientOfferCarModelMakeSpecification(int id)
    {
        Query.Where(r => r.Id == id)
            .Include(r => r.RentalReturn)
            .Include(r => r.Customer)
            .Include(r => r.Offer)
            .ThenInclude(o => o.Car)
            .ThenInclude(c => c.Model)
            .ThenInclude(mo => mo.Make);
    }
}