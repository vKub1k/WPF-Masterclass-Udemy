using System;
using System.Windows.Input;

namespace MyNote.ViewModel.Commands;

public class NewNotebookCommand : ICommand
{
    public NotesVM Vm { get; set; }

    public NewNotebookCommand(NotesVM vm)
    {
        Vm = vm;
    }
    
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
       Vm.CreateNotebook();
    }

    public event EventHandler? CanExecuteChanged;
}