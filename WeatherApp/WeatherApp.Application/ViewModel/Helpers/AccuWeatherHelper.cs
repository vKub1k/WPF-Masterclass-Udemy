using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherApp.Application.Model;

namespace WeatherApp.Application.ViewModel.Helpers;

public class AccuWeatherHelper
{
    private const string BASE_URL = "http://dataservice.accuweather.com/";
    private const string API_KEY = "AAVRi78CRoXHo3YGPVRmX7DXPZ7n8pGV";
    private const string AUTOCOMPLETE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
    private const string CURRENTCONDITIONS_ENDPOINT = "currentconditions/v1/{0}?apikey={1}";

    public static async Task<List<City>?> GetCities(string query)
    {
        List<City>? cities;
        string url = BASE_URL + string.Format(AUTOCOMPLETE_ENDPOINT, API_KEY, query);

        using (HttpClient client = new HttpClient())
        {
            var response = await client.GetAsync(url);

            string json = await response.Content.ReadAsStringAsync();

            cities = JsonConvert.DeserializeObject<List<City>>(json);
        }

        return cities;
    }
    
    public static async Task<CurrentConditions?> GetCurrentConditions(string cityKey)
    {
        CurrentConditions? currentConditions;
        
        string url = BASE_URL + string.Format(CURRENTCONDITIONS_ENDPOINT, cityKey, API_KEY);
        
        using (HttpClient client = new HttpClient())
        {
            var response = await client.GetAsync(url);

            string json = await response.Content.ReadAsStringAsync();

            currentConditions = (JsonConvert.DeserializeObject<List<CurrentConditions>>(json) ?? new List<CurrentConditions>()).FirstOrDefault();
        }
        
        return currentConditions;
    }
}