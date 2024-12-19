using CarRental.Common.Core.ProviderEntities;

namespace CarRental.Provider.Tests.Dummies;

public static class ProviderEntitiesDummyFactory
{
	public static Customer CreateDummyCustomer()
	{
		return new Customer()
		{
			EmailAddress = string.Empty,
			FirstName = string.Empty,
			LastName = string.Empty
		};
	}

	public static Insurance CreateDummyInsurance()
	{
		return new Insurance() { Name = string.Empty };
	}

	public static Segment CreateDummySegment()
	{
		return new Segment()
		{
			Name = string.Empty,
			Insurance = CreateDummyInsurance()
		};
	}

	public static Make CreateDummyMake()
	{
		return new Make() { Name = string.Empty };
	}

	public static Model CreateDummyModel()
	{
		return new Model()
		{
			Name = string.Empty,
			Make = CreateDummyMake(),
			Segment = CreateDummySegment()
		};
	}

	public static Car CreateDummyCar()
	{
		return new Car()
		{
			Model = CreateDummyModel(),
		};
	}

	public static Offer CreateDummyOffer()
	{
		return new Offer()
		{
			Key = string.Empty,
			GeneratedBy = string.Empty,
			Car = CreateDummyCar()
		};
	}

	public static Rental CreateDummyRental()
	{
		return new Rental()
		{
			Customer = CreateDummyCustomer(),
			Offer = CreateDummyOffer()
		};
	}

	public static RentalReturn CreateDummyRentalReturn()
	{
		return new RentalReturn() { Rental = CreateDummyRental() };
	}
}