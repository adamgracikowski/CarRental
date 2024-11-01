using CarRental.Common.Core.Enums;

namespace CarRental.Common.Core.ProviderEntities;

public sealed class Car : EntityBase
{
    public required Model Model { get; set; }

    public int ModelId { get; set; }

    public ICollection<Offer> Offers { get; set; } = [];

    public int ProductionYear { get; set; }

    public FuelType FuelType { get; set; }

    public TransmissionType TransmissionType { get; set; }

    public decimal Longitude { get; set; }

    public decimal Latitude { get; set; }

    public CarStatus Status { get; set; }
}