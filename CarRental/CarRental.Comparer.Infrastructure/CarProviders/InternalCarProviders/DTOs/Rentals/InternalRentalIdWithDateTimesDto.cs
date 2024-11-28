using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Comparer.Infrastructure.CarProviders.InternalCarProviders.DTOs.Rentals;

public sealed record InternalRentalIdWithDateTimesDto(
    int id, 
    DateTime GeneratedAt,
    DateTime ExpiresAt);
