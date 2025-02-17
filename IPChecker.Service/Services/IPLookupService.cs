using System.Net.Http;
using System.Threading.Tasks;
using IPChecker.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

public class IPLookupService : IIPLookupService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl;
    private readonly string _apiKey;

    public IPLookupService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiBaseUrl = configuration["GeoLocationApi:BaseUrl"];
        _apiKey = configuration["GeoLocationApi:ApiKey"];
    }

    public async Task<string> GetCountryCodeByIPAsync(string ip)
    {
        try
        {
            var response = await _httpClient.GetStringAsync($"{_apiBaseUrl}/{ip}/json/?key={_apiKey}");
            dynamic result = JsonConvert.DeserializeObject(response);
            return result?.country ?? "Unknown"; 
        }
        catch
        {
            return "Error"; 
        }
    }
}
