using System;
using System.Windows.Input;

namespace MyNote.ViewModel.Commands;

public class EditCommand : ICommand
{
    public bool CanExecute(object? parameter)
    {
        return true;
    }
    public NotesVM ViewModel { get; set; }

    public EditCommand(NotesVM vm)
    {
        ViewModel = vm;
    }

    public void Execute(object? parameter)
    {
        ViewModel.StartEditing();
    }

    public event EventHandler? CanExecuteChanged;
}