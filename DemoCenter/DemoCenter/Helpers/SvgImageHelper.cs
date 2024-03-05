using System.Reflection;
using System.Xml;
using Avalonia.Markup.Xaml;
using Avalonia.Svg.Skia;
using AvaloniaEdit.Highlighting;
using AvaloniaEdit.Highlighting.Xshd;

namespace DemoCenter.Helpers; 

public static class SvgImageHelper
{
    public static SvgImage CreateSvgImage(string path)
    {
        var svgSource = SvgSource.Load(path, null);
        return new SvgImage { Source = svgSource };
    }
}