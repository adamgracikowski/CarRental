﻿using CarRental.Comparer.Web.Requests.DTOs.Makes;

namespace CarRental.Comparer.Web.PagesNavModels;

public sealed record HomeToRentalCars
{
    public ICollection<MakeWithModelsDto> Makes { get; set; }

    public string SelectedMake { get; set; } = string.Empty;

    public string SelectedModel { get; set; } = string.Empty;

    public HomeToRentalCars(ICollection<MakeWithModelsDto> makes)
    {
        Makes = makes;
    }
}
