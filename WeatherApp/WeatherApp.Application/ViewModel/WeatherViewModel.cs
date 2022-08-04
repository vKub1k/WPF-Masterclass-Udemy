using System.Collections.Generic;
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
                        Value = 21
                    }
                }
            };
        }

        SearchCommand = new SearchCommand(this);
    }
    
    private string query;
    public string Query
    {
        get { return query; }
        set
        {
            query = value;
            OnPropertyChanged("Query");
        }
    }
    
    private CurrentConditions _currentConditions;
    public CurrentConditions CurrentConditions
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
        get { return _selectedCity; }
        set
        {
            _selectedCity = value;
            OnPropertyChanged("SelectedCity");
        }
    }

    public SearchCommand SearchCommand { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public async void MakeQuery()
    {
        var cities = await AccuWeatherHelper.GetCities(Query);
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