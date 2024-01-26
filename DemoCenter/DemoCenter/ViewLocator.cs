using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using Eremex.AvaloniaUI.Controls.Common;

namespace DemoCenter;

public class ViewLocator : IDataTemplate
{
    public Control Build(object data)
    {
        if (data is null)
            return null;

        var name = data.GetType().FullName!.Replace("ViewModel", "View");
        var type = Type.GetType(name);

        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }
        
        return new TextBlock { Text = name + " not found!", Foreground = new SolidColorBrush(Colors.Red), 
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center, 
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center};
    }

    public bool Match(object data)
    {
        return data is ViewModelBase;
    }
}