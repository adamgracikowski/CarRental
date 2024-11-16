using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Comparer.API.BackgroundJobs.RentalServices;

public interface IRentalComparerStatusCheckerService
{
    Task CheckAndUpdateRentalStatusAsync(
        string providerName, 
        int innerRentalId, 
        int outerRentalId, 
        string jobId, 
        DateTime jobExpirationTime, 
        CancellationToken cancellationToken);
}
