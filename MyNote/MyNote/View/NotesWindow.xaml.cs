using System;
using System.Linq;
using System.Speech;
using System.Speech.Recognition;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MyNote.View;

public partial class NoteWindow : Window
{
    private SpeechRecognitionEngine _recognizer;
    public NoteWindow()
    {
        InitializeComponent();

        var currentCulture = (from r in SpeechRecognitionEngine.InstalledRecognizers()
            where r.Culture.Equals(Thread.CurrentThread.CurrentCulture)
            select r).FirstOrDefault();
        _recognizer = new SpeechRecognitionEngine();

        GrammarBuilder builder = new GrammarBuilder();
        builder.AppendDictation();
        Grammar grammar = new Grammar(builder);

        _recognizer.LoadGrammar(grammar);
        _recognizer.SetInputToDefaultAudioDevice();
        _recognizer.SpeechRecognized += RecognizerEvent;
    }

    private void RecognizerEvent(object? sender, SpeechRecognizedEventArgs e)
    {
        string recognizedText = e.Result.Text;
        ContentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(recognizedText)));
    }

    private void MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private bool isRecognizing = false;
    private void SpeechButton_Click(object sender, RoutedEventArgs e)
    {
        if (!isRecognizing)
        {
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            isRecognizing = true;
        }
        else
        {
            _recognizer.RecognizeAsyncStop();
            isRecognizing = false;
        }
    }

    private void ContentRichTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        int amountOfCharacters = (new TextRange(
            ContentRichTextBox.Document.ContentStart,
            ContentRichTextBox.Document.ContentEnd)
            .Text.Length);

        StatusTextBlock.Text = $"Document length: {amountOfCharacters}";
    }

    private void BoldButton_Click(object sender, RoutedEventArgs e)
    {
        var textToBold = new TextRange(ContentRichTextBox.Selection.Start, ContentRichTextBox.Selection.End);
        
        ContentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
    }
}