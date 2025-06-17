using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace GlicareApp.CrossCuting.Extensions
{
    public static class HttpClientExtension
    {
        private readonly static MediaTypeHeaderValue ContentType =
          new("application/json");

        private readonly static JsonSerializerOptions JsonOptions = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public static Task<HttpResponseMessage> PostAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonSerializer.Serialize(data, JsonOptions);

            var content = new StringContent(dataAsString);

            content.Headers.ContentType = ContentType;

            return httpClient.PostAsync(url, content);
        }
    }
}
