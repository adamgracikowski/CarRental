using System.Diagnostics.CodeAnalysis;

namespace CarRental.Comparer.Web.Extensions;

public sealed class SlugStringComparer : IEqualityComparer<string>
{
	public bool Equals(string? x, string? y)
	{
		return  x != null && y != null && ToSlug(x) == ToSlug(y);
	}

	public int GetHashCode([DisallowNull] string obj)
	{
		return ToSlug(obj).GetHashCode();
	}

	private static string ToSlug(string value)
	{
		return value.Replace(' ', '-').ToLower();
	}
}