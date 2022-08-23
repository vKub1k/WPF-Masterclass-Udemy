using System;
using System.Windows.Input;

namespace MyNote.ViewModel.Commands;

public class LoginCommand : ICommand
{
    public LoginVM Vm { get; set; }

    public LoginCommand(LoginVM vm)
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