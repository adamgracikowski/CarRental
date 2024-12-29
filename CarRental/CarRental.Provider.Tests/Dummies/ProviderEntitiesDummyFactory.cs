using CarRental.Common.Core.ProviderEntities;

namespace CarRental.Provider.Tests.Dummies;

public static class ProviderEntitiesDummyFactory
{
	public static Customer CreateCustomerDummy()
	{
		return new Customer()
		{
			EmailAddress = string.Empty,
			FirstName = string.Empty,
			LastName = string.Empty
		};
	}

	public static Insurance CreateInsuranceDummy()
	{
		return new Insurance() { Name = string.Empty };
	}

	public static Segment CreateSegmentDummy()
	{
		return new Segment()
		{
			Name = string.Empty,
			Insurance = CreateInsuranceDummy()
		};
	}

	public static Make CreateMakeDummy()
	{
		return new Make() { Name = string.Empty };
	}

	public static Model CreateModelDummy()
	{
		return new Model()
		{
			Name = string.Empty,
			Make = CreateMakeDummy(),
			Segment = CreateSegmentDummy()
		};
	}

	public static Car CreateCarDummy()
	{
		return new Car()
		{
			Model = CreateModelDummy(),
		};
	}

	public static Offer CreateOfferDummy()
	{
		return new Offer()
		{
			Key = string.Empty,
			GeneratedBy = string.Empty,
			Car = CreateCarDummy()
		};
	}

	public static Rental CreateRentalDummy()
	{
		return new Rental()
		{
			Customer = CreateCustomerDummy(),
			Offer = CreateOfferDummy()
		};
	}

	public static RentalReturn CreateRentalReturnDummy()
	{
		return new RentalReturn() { Rental = CreateRentalDummy() };
	}
}