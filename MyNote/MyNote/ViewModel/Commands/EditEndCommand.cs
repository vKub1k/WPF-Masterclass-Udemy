using System;
using System.Windows.Input;
using MyNote.Model;

namespace MyNote.ViewModel.Commands;

public class EditEndCommand : ICommand
{
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public NotesVM ViewModel { get; set; }

    public EditEndCommand(NotesVM vm)
    {
        ViewModel = vm;
    }

    public void Execute(object? parameter)
    {
        Notebook notebook = parameter as Notebook;
        if (notebook != null)
        {
            ViewModel.EndEditing(notebook);
        }
    }

    public event EventHandler? CanExecuteChanged;
}