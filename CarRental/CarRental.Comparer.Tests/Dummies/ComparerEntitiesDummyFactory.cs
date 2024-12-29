using CarRental.Common.Core.ComparerEntities;

namespace CarRental.Comparer.Tests.Dummies;

public sealed class ComparerEntitiesDummyFactory
{
	public static Provider CreateProviderDummy()
	{
		return new Provider() 
		{ 
			Name = string.Empty 
		};
	}

	public static Employee CreateEmployeeDummy()
	{
		return new Employee()
		{
			Email = string.Empty,
			FirstName = string.Empty,
			LastName = string.Empty,
			Provider = CreateProviderDummy()
		};
	}

	public static CarDetails CreateCarDetailsDummy()
	{
		return new CarDetails()
		{
			OuterId = string.Empty,
			Make = string.Empty,
			Model = string.Empty,
		};
	}

	public static User CreateUserDummy()
	{
		return new User()
		{
			Email = string.Empty,
			Name = string.Empty,
			Lastname = string.Empty,
		};
	}

	public static RentalTransaction CreateRentalTransactionDummy()
	{
		return new RentalTransaction()
		{
			RentalOuterId = string.Empty,
			Provider = CreateProviderDummy(),
			CarDetails = CreateCarDetailsDummy(),
			User = CreateUserDummy()
		};
	}
}