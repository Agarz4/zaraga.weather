using Newtonsoft.Json;
using System.Text;

namespace zaraga.api;

public class Client
{
    private readonly string _baseUrl;
    private readonly int _timeout; // seconds
    private readonly Dictionary<string, string>? _baseHeaders;

    /// <summary>
    /// HTTP API Rest Client
    /// </summary>
    /// <param name="baseUrl">Base URL to invoke the endpoints</param>
    /// <param name="timeout">Seconds to set the timeout in the requests</param>
    /// <param name="baseHeaders">Headers to send in every request</param>
    public Client(string baseUrl, int timeout = 30, Dictionary<string, string>? baseHeaders = null)
    {
        _baseUrl = baseUrl;
        _timeout = timeout;
        _baseHeaders = baseHeaders;
    }

    /// <summary>
    /// Make a HTTP GET request to the given endpoint and return the response as a Type.
    /// </summary>
    /// <typeparam name="T">Structure of the Object returned</typeparam>
    /// <param name="endpoint">Endpoint URL after baseUrl</param>
    /// <param name="defaultResponse">Default value returned in case of empty response or error</param>
    /// <returns>Returns an object with the structure defined in <paramref name="T"/></returns>
    public async Task<T?> Get<T>(string endpoint, T? defaultResponse)
    {
        string response = await Execute(HttpMethod.Get, endpoint);
        if (string.IsNullOrWhiteSpace(response))
        {
            return defaultResponse;
        }
        return JsonConvert.DeserializeObject<T>(response) ?? defaultResponse;
    }

    /// <summary>
    /// Make a HTTP POST request to the given endpoint and return the response as a Type.
    /// </summary>
    /// <typeparam name="T">Structure of the Object returned</typeparam>
    /// <param name="endpoint">Endpoint URL after baseUrl</param>
    /// <param name="defaultResponse">Default value returned in case of empty response or error</param>
    /// <param name="body">Data to send into the request, if is null it is going to be skipped</param>
    /// <returns>Returns an object with the structure defined in <paramref name="T"/></returns>
    public async Task<T?> Post<T>(string endpoint, T? defaultResponse, object body)
    {
        string response = await Execute(HttpMethod.Post, endpoint, body);
        if (string.IsNullOrWhiteSpace(response))
        {
            return defaultResponse;
        }
        return JsonConvert.DeserializeObject<T>(response) ?? defaultResponse;
    }


    /// <summary>
    /// Make a HTTP PUT request to the given endpoint and return the response as a Type.
    /// </summary>
    /// <typeparam name="T">Structure of the Object returned</typeparam>
    /// <param name="endpoint">Endpoint URL after baseUrl</param>
    /// <param name="defaultResponse">Default value returned in case of empty response or error</param>
    /// <param name="body">Data to send into the request, if is null it is going to be skipped</param>
    /// <returns>Returns an object with the structure defined in <paramref name="T"/></returns>
    public async Task<T?> Put<T>(string endpoint, T? defaultResponse, object body)
    {
        string response = await Execute(HttpMethod.Put, endpoint, body);
        if (string.IsNullOrWhiteSpace(response))
        {
            return defaultResponse;
        }
        return JsonConvert.DeserializeObject<T>(response) ?? defaultResponse;
    }

    /// <summary>
    /// Make a HTTP PATCH request to the given endpoint and return the response as a Type.
    /// </summary>
    /// <typeparam name="T">Structure of the Object returned</typeparam>
    /// <param name="endpoint">Endpoint URL after baseUrl</param>
    /// <param name="defaultResponse">Default value returned in case of empty response or error</param>
    /// <param name="body">Data to send into the request, if is null it is going to be skipped</param>
    /// <returns>Returns an object with the structure defined in <paramref name="T"/></returns>
    public async Task<T?> Patch<T>(string endpoint, T? defaultResponse, object body)
    {
        string response = await Execute(HttpMethod.Patch
            , endpoint, body);
        if (string.IsNullOrWhiteSpace(response))
        {
            return defaultResponse;
        }
        return JsonConvert.DeserializeObject<T>(response) ?? defaultResponse;
    }

    /// <summary>
    /// Make a HTTP DELETE request to the given endpoint and return the response as a Type.
    /// </summary>
    /// <typeparam name="T">Structure of the Object returned</typeparam>
    /// <param name="endpoint">Endpoint URL after baseUrl</param>
    /// <param name="defaultResponse">Default value returned in case of empty response or error</param>
    /// <param name="body">Data to send into the request, if is null it is going to be skipped</param>
    /// <returns>Returns an object with the structure defined in <paramref name="T"/></returns>
    public async Task<T?> Delete<T>(string endpoint, T? defaultResponse)
    {
        string response = await Execute(HttpMethod.Delete, endpoint);
        if (string.IsNullOrWhiteSpace(response))
        {
            return defaultResponse;
        }
        return JsonConvert.DeserializeObject<T>(response) ?? defaultResponse;
    }


    /// <summary>
    /// Execute the HTTP request with the given method and endpoint.
    /// </summary>
    /// <param name="method">Method of the request</param>
    /// <param name="endpoint">Endpoint URL after baseUrl</param>
    /// <param name="body">Data to send into the request, if is null it is going to be skipped</param>
    /// <returns></returns>
    private async Task<string> Execute(HttpMethod method, string endpoint, object? body = null, Dictionary<string, string>? requestHeaders = null)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(_baseUrl);
        client.Timeout = TimeSpan.FromSeconds(_timeout);
        //Add base headers to the request
        if (_baseHeaders != null)
        {
            foreach (var header in _baseHeaders)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }
        //Add request headers 
        if (requestHeaders != null)
        {
            foreach (var header in requestHeaders)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        var request = new HttpRequestMessage(method, endpoint);
        if (body != null)
        {
            var json = JsonConvert.SerializeObject(body);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return content;
    }
}
