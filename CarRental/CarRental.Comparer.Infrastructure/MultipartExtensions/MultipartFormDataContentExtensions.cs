using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Reflection;

namespace CarRental.Comparer.Infrastructure.MultipartExtensions;

public static class MultipartFormDataContentExtensions
{
	public static MultipartFormDataContent ToMultipartFormDataContent<T>(this T obj)
	{
		var multipartContent = new MultipartFormDataContent();

		foreach (var property in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
		{
			var value = property.GetValue(obj);

			if (value == null)
				continue;

			if (value is IFormFile file)
			{
				var streamContent = new StreamContent(file.OpenReadStream());
				streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
				multipartContent.Add(streamContent, property.Name, file.FileName);
			}
			else if (value is Stream stream)
			{
				var streamContent = new StreamContent(stream);
				multipartContent.Add(streamContent, property.Name);
			}
			else
			{
				multipartContent.Add(new StringContent(value.ToString()!), property.Name);
			}
		}

		return multipartContent;
	}
}