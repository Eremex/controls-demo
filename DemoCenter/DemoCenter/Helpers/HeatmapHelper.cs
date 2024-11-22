using System.Reflection;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Media.Imaging;
using Eremex.AvaloniaUI.Charts;

namespace DemoCenter.Helpers;

public static class HeatmapHelper
{
    static double[,] ConvertToDoubles(byte[] data, PixelSize size)
    {
        var result = new double[size.Height, size.Width];
        var x = 0;
        var y = size.Height - 1;
        for (int i = 0; i < data.Length; i++)
        {
            if (x == size.Width)
            {
                x = 0;
                y--;
            }
            result[y, x] = data[i];
            x++;
        }
        return result;
    }
    public static string[] CreateArguments(int size)
    {
        var result = new string[size];
        for (int i = 0; i < size; i++)
            result[i] = i.ToString();
        return result;
    }
    public static HeatmapDataAdapter GetAdapter(string imageName)
    {
        var assembly = Assembly.GetAssembly(typeof(HeatmapHelper));
        var resourceName = assembly!.GetManifestResourceNames().First(s => s.EndsWith($"{imageName}.png"));
        var stream = assembly.GetManifestResourceStream(resourceName)!;
        using var bitmap = WriteableBitmap.Decode(stream);
        var argumentsX = CreateArguments(bitmap.PixelSize.Width);
        var argumentsY = CreateArguments(bitmap.PixelSize.Height);
        var data = new byte[bitmap.PixelSize.Width * bitmap.PixelSize.Height];
        using (var frameBuffer = bitmap.Lock()) 
        { 
            Marshal.Copy(frameBuffer.Address, data, 0, data.Length); 
        }
        var values = ConvertToDoubles(data, bitmap.PixelSize);
        return new HeatmapDataAdapter(argumentsX, argumentsY, values);
    }
}
