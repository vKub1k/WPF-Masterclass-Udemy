using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech;
using System.Speech.Recognition;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

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

        var fontFamilies = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
        ComboBoxFontFamily.ItemsSource = fontFamilies;

        List<double> fontSizes = new List<double>()
        {
            4, 6, 8, 10, 11, 12, 14, 16, 18, 20, 50, 64, 80, 100
        };
        ComboBoxFontSize.ItemsSource = fontSizes;
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
        var isChecked = (sender as ToggleButton).IsChecked ?? false;
        //var textToBold = new TextRange(ContentRichTextBox.Selection.Start, ContentRichTextBox.Selection.End);
        if (isChecked)
        {
            ContentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
        }
        else
        {
            ContentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Regular);
        }
    }

    private void ItalicButton_OnClick(object sender, RoutedEventArgs e)
    {
        var isChecked = (sender as ToggleButton).IsChecked ?? false;
        //var textToBold = new TextRange(ContentRichTextBox.Selection.Start, ContentRichTextBox.Selection.End);
        if (isChecked)
        {
            ContentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
        }
        else
        {
            ContentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
        }
    }

    private void UnderlineButton_OnClick(object sender, RoutedEventArgs e)
    {
        var isChecked = (sender as ToggleButton).IsChecked ?? false;
        //var textToBold = new TextRange(ContentRichTextBox.Selection.Start, ContentRichTextBox.Selection.End);
        if (isChecked)
        {
            ContentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
        }
        else
        {
            TextDecorationCollection textDecorationCollection;
            (ContentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection)
                .TryRemove(TextDecorations.Underline, out textDecorationCollection);
            ContentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, textDecorationCollection);
        }
    }

    private void ComboBoxFontFamily_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ComboBoxFontFamily.SelectedItem != null)
        {
            ContentRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, ComboBoxFontFamily.SelectedItem);
        }
    }

    private void ComboBoxFontSize_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (ComboBoxFontSize.Text != null || ComboBoxFontSize.Text != "")
        {
            ContentRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, ComboBoxFontSize.Text);
        }
    }

    private void ContentRichTextBox_OnSelectionChanged(object sender, RoutedEventArgs e)
    {
        var selectedWeight = ContentRichTextBox.Selection.GetPropertyValue(Inline.FontWeightProperty);
        BoldButton.IsChecked = selectedWeight != DependencyProperty.UnsetValue && selectedWeight.Equals(FontWeights.Bold);
        
        var selectedStyle = ContentRichTextBox.Selection.GetPropertyValue(Inline.FontStyleProperty);
        ItalicButton.IsChecked = selectedStyle != DependencyProperty.UnsetValue && selectedStyle.Equals(FontStyles.Italic);
        
        var selectedDecoration = ContentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
        UnderlineButton.IsChecked = selectedDecoration != DependencyProperty.UnsetValue && selectedDecoration.Equals(TextDecorations.Underline);

        ComboBoxFontFamily.SelectedItem = ContentRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
        ComboBoxFontSize.Text = (ContentRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty)).ToString();
    }
}