using System.Windows;
using System.Windows.Controls;

namespace MyNote.View;

public partial class NoteWindow : Window
{
    public NoteWindow()
    {
        InitializeComponent();
    }

    private void MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void SpeechButton_Click(object sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void ContentRichTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}