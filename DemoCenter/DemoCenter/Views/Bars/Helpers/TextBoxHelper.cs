using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

using System;
using System.Data.Common;
using System.Windows.Input;

namespace DemoCenter.Views.Bars.Helpers;

public class TextBoxHelper : AvaloniaObject
{
    public static readonly AttachedProperty<bool> IsEnabledProperty = AvaloniaProperty.RegisterAttached<TextBoxHelper, TextBox, bool>("IsEnabled");

    public static readonly AttachedProperty<int> LineProperty = AvaloniaProperty.RegisterAttached<TextBoxHelper, TextBox, int>("Line", defaultValue: 1);

    public static readonly AttachedProperty<int> ColumnProperty = AvaloniaProperty.RegisterAttached<TextBoxHelper, TextBox, int>("Column", defaultValue: 1);

    public static bool GetIsEnabled(TextBox textBox)
    {
        return textBox.GetValue(IsEnabledProperty);
    }

    public static void SetIsEnabled(TextBox textBox, bool isEnabled)
    {
        textBox.SetValue(IsEnabledProperty, isEnabled);
    }

    public static int GetLine(TextBox textBox)
    {
        return textBox.GetValue(LineProperty);
    }

    public static void SetLine(TextBox textBox, int line)
    {
        textBox.SetValue(LineProperty, line);
    }

    public static int GetColumn(TextBox textBox)
    {
        return textBox.GetValue(ColumnProperty);
    }

    public static void SetColumn(TextBox textBox, int column)
    {
        textBox.SetValue(ColumnProperty, column);
    }

    static TextBoxHelper()
    {
        IsEnabledProperty.Changed.Subscribe(x => OnIsEnabledChanged(x));
    }

    private static void OnIsEnabledChanged(AvaloniaPropertyChangedEventArgs<bool> e)
    {
        var textBox = (TextBox)e.Sender;
        if (textBox == null)
            return;

        if(e.NewValue.Value)
            textBox.PropertyChanged += TextBox_PropertyChanged;
        else
            textBox.PropertyChanged -= TextBox_PropertyChanged;
    }

    private static void TextBox_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property == TextBox.CaretIndexProperty)
        {
            var textBox = (TextBox)sender;

            SetLine(textBox, GetLineNumber(textBox));
            SetColumn(textBox, GetColumnNumber(textBox));
        }
    }

    private static int GetLineNumber(TextBox textBox)
    {
        int currentIndex = 0;
        int lineNumber = 1;
        while(currentIndex < textBox.CaretIndex)
        {
            if (textBox.Text.Length < currentIndex + textBox.NewLine.Length)
                break;
            if (textBox.Text.Substring(currentIndex, textBox.NewLine.Length) == textBox.NewLine)
            {
                lineNumber++;
                currentIndex += textBox.NewLine.Length;
            }
            else
            {
                currentIndex++;
            }
        }
        return lineNumber;
    }

    private static int GetColumnNumber(TextBox textBox)
    {   
        int currentIndex = textBox.CaretIndex - textBox.NewLine.Length;
        while (currentIndex >= 0)
        {   
            if (textBox.Text.Substring(currentIndex, textBox.NewLine.Length) == textBox.NewLine)
            {
                currentIndex += textBox.NewLine.Length;
                break;
            }
            else
            {
                currentIndex--;
            }
        }
        currentIndex = Math.Max(currentIndex, 0);
        return textBox.CaretIndex - currentIndex + 1;
    }
}