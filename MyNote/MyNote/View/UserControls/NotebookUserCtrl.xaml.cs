using System.Windows;
using System.Windows.Controls;
using MyNote.Model;

namespace MyNote.View.UserControls;

public partial class NotebookUserCtrl : UserControl
{
    #region propdp

    public Notebook Notebook
    {
        get { return (Notebook)GetValue(NotebookProperty); }
        set { SetValue(NotebookProperty, value); }
    }

    public static readonly DependencyProperty NotebookProperty =
        DependencyProperty.Register("Notebook", typeof(Notebook), typeof(NotebookUserCtrl), new PropertyMetadata(null, SetValues));

    private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var notebookUserCtrl = d as NotebookUserCtrl;

        if (notebookUserCtrl != null) notebookUserCtrl.DataContext = notebookUserCtrl.Notebook;
    }

    #endregion
    
    public NotebookUserCtrl()
    {
        InitializeComponent();
    }
}