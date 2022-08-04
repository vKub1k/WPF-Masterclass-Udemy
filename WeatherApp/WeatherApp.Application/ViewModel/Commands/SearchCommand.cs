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
        return true;
    }

    public void Execute(object? parameter)
    {
        ViewModel.MakeQuery();
    }

    public event EventHandler? CanExecuteChanged;
}