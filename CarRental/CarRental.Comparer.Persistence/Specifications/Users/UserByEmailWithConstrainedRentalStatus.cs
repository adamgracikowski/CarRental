using Ardalis.Specification;
using CarRental.Common.Core.ComparerEntities;
using CarRental.Common.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Comparer.Persistence.Specifications.Users;

public class UserByEmailWithConstrainedRentalStatus : Specification<User>
{
    public UserByEmailWithConstrainedRentalStatus(string email)
    {
		Query.Where(u => u.Email == email)
			.Where(u => !u.RentalTransactions.Any(r =>
				r.Status == RentalStatus.Active ||
				r.Status == RentalStatus.ReadyForReturn ||
				r.Status == RentalStatus.Unconfirmed));
	}
}
