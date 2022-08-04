using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WeatherApp.Application.Model;
using WeatherApp.Application.ViewModel.Commands;
using WeatherApp.Application.ViewModel.Helpers;

namespace WeatherApp.Application.ViewModel;

public class WeatherViewModel : INotifyPropertyChanged
{
    public WeatherViewModel()
    {
        if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
        {
            SelectedCity = new City
            {
                LocalizedName = "Szczecin"
            };

            CurrentConditions = new CurrentConditions
            {
                WeatherText = "Partly cloudy",
                Temperature = new Temperature
                {
                    Metric = new MeasurementSystem
                    {
                        Value = "21"
                    }
                }
            };
        }

        SearchCommand = new SearchCommand(this);

        Cities = new ObservableCollection<City>();
    }

    private async void GetCurrentConditions()
    {
        Query = string.Empty;
        Cities.Clear();
        CurrentConditions =  await AccuWeatherHelper.GetCurrentConditions(SelectedCity.Key);
    }
    
    private string _query;
    public string Query
    {
        get => _query;
        set
        {
            _query = value;
            OnPropertyChanged("Query");
        }
    }

    public ObservableCollection<City> Cities { get; set; }
    
    private CurrentConditions? _currentConditions;
    public CurrentConditions? CurrentConditions
    {
        get { return _currentConditions; }
        set
        {
            _currentConditions = value;
            OnPropertyChanged("CurrentConditions");
        }
    }
    
    private City _selectedCity;
    public City SelectedCity
    {
        get => _selectedCity;
        set
        {
            _selectedCity = value;
            
            OnPropertyChanged("SelectedCity");

            GetCurrentConditions();
        }
    }

    public SearchCommand SearchCommand { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public async void MakeQuery()
    {
        var cities = await AccuWeatherHelper.GetCities(Query);
        

        if (cities == null) return;
        Cities.Clear();
        foreach (var item in cities)
        {
            Cities.Add(item);
        }
    }
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}