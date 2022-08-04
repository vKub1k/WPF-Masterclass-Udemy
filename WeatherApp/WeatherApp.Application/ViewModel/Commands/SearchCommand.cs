using System;
using System.Windows.Input;

namespace WeatherApp.Application.ViewModel.Commands;

public class SearchCommand : ICommand
{
    public WeatherViewModel ViewModel { get; set; }

    public SearchCommand(WeatherViewModel viewModel)
    {
        ViewModel = viewModel;
    }
    
    public bool CanExecute(object? parameter)
    {
        var query = parameter as string;
        
        return !string.IsNullOrWhiteSpace(query);
    }

    public void Execute(object? parameter)
    {
        ViewModel.MakeQuery();
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}