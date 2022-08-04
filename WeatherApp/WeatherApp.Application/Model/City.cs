namespace WeatherApp.Application.Model;

public class Localization
{
    public string ID { get; set; }
    public string LocalizedName { get; set; }
}

public class City
{
    public int Version { get; set; }
    public string Key { get; set; }
    public string Type { get; set; }
    public int Rank { get; set; }
    public string LocalizedName { get; set; }
    public Localization Country { get; set; }
    public Localization AdministrativeArea { get; set; }
}