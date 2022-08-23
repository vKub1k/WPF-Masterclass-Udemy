using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using MyNote.Model;
using MyNote.ViewModel.Commands;
using MyNote.ViewModel.Helpers;

namespace MyNote.ViewModel;

public class NotesVM : INotifyPropertyChanged
{
    public ObservableCollection<Notebook> Notebooks { get; set; }
    public ObservableCollection<Note> Notes { get; set; }

    private Notebook _selectedNotebook;
    public Notebook SelectedNotebook
    {
        get => _selectedNotebook;
        set
        {
            _selectedNotebook = value;
            OnPropertyChanged("SelectedNotebook");
            GetNotes();
        }
    }
    
    
    public  NewNotebookCommand NewNotebookCommand { get; set; }
    public  NewNoteCommand NewNoteCommand { get; set; }

    public NotesVM()
    {
        NewNotebookCommand = new NewNotebookCommand(this);
        NewNoteCommand = new NewNoteCommand(this);

        Notebooks = new ObservableCollection<Notebook>();
        Notes = new ObservableCollection<Note>();
        
        GetNotebooks();
    }

    public void CreateNote(int notebookId)
    {
        Note newNote = new Note()
        {
            NoteboookId = notebookId,
            CreatedTime = DateTime.Now,
            UpdatedTime = DateTime.Now,
            Title = $"Note {DateTime.Now:hh:mm:ss}"
        };

        DatabaseHelper.Insert<Note>(newNote);
        GetNotes();
    }
    public void CreateNotebook()
    {
        Notebook newNotebook = new Notebook
        {
            Name = "New notebook"
        };

        DatabaseHelper.Insert(newNotebook);
        GetNotebooks();
    }

    private void GetNotebooks()
    {
        var notebooksInDb = DatabaseHelper.Read<Notebook>();
        Notebooks.Clear();

        foreach (Notebook notebook in notebooksInDb)
        {
            Notebooks.Add(notebook);
        }
    }

    private void GetNotes()
    {
        var notesInDb = DatabaseHelper.Read<Note>()
            .Where(n => n.NoteboookId == SelectedNotebook.Id)
            .ToList();
        Notes.Clear();
        
        foreach (Note note in notesInDb)
        {
            Notes.Add(note);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

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