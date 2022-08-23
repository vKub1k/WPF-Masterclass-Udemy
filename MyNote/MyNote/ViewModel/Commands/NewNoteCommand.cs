using System;
using System.Windows.Input;
using MyNote.Model;

namespace MyNote.ViewModel.Commands;

public class NewNoteCommand : ICommand
{
    public NotesVM Vm { get; set; }

    public NewNoteCommand(NotesVM vm)
    {
        Vm = vm;
    }
    
    public bool CanExecute(object? parameter)
    {
        return (parameter as Notebook) != null;
    }

    public void Execute(object? parameter)
    {
        if (parameter is Notebook selectedNotebook)
            Vm.CreateNote(selectedNotebook.Id);
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}