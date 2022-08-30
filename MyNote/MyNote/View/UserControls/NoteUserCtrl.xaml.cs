using System.Windows;
using System.Windows.Controls;
using MyNote.Model;

namespace MyNote.View.UserControls;

public partial class NoteUserCtrl : UserControl
{
    
    #region propdp

    public Note Note
    {
        get { return (Note)GetValue(NoteProperty); }
        set { SetValue(NoteProperty, value); }
    }

    public static readonly DependencyProperty NoteProperty =
        DependencyProperty.Register("Note", typeof(Note), typeof(NoteUserCtrl), new PropertyMetadata(null, SetValues));

    private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var noteUserCtrl = d as NoteUserCtrl;

        if (noteUserCtrl != null) noteUserCtrl.DataContext = noteUserCtrl.Note;
    }

    #endregion
    
    public NoteUserCtrl()
    {
        InitializeComponent();
    }
}