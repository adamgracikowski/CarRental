using System.Globalization;

namespace CarRental.Provider.Tests.TestData;

public sealed class OfferCalculatorServiceTheoryData
	: TheoryData<int, int, decimal, decimal, decimal, decimal>
{
	public OfferCalculatorServiceTheoryData()
	{
		var lines = File.ReadAllLines(Path.Combine("TestData", "OfferCalculatorServiceTestData.csv"));
		var culture = CultureInfo.InvariantCulture;

		foreach (var line in lines)
		{
			var tokens = line
				.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

			try
			{
				Add(
					int.Parse(tokens[0], culture),
					int.Parse(tokens[1], culture),
					decimal.Parse(tokens[2], culture),
					decimal.Parse(tokens[3], culture),
					decimal.Parse(tokens[4], culture),
					decimal.Parse(tokens[5], culture)
				);
			}
			catch (Exception)
			{
				continue;
			}
		}
	}
}