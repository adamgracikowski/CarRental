using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Comparer.Infrastructure.CarComparisons.DTOs.Rentals;

public sealed record RentalIdWithDateTimesDto(
    string id, 
    DateTime GeneratedAt,
    DateTime ExpiresAt);
