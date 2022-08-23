using System;
using System.Windows.Input;

namespace MyNote.ViewModel.Commands;

public class RegisterCommand : ICommand
{
    public LoginVM Vm { get; set; }

    public RegisterCommand(LoginVM vm)
    {
        Vm = vm;
    }
    
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        //TODO
    }

    public event EventHandler? CanExecuteChanged;
}