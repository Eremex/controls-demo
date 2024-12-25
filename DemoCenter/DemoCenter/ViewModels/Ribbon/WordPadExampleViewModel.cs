using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData;

namespace DemoCenter.ViewModels;

public partial class WordPadExampleViewModel : PageViewModelBase
{
	[ObservableProperty] private ObservableCollection<string> fonts;
	[ObservableProperty] private ObservableCollection<int> fontSizes;
	[ObservableProperty] private string selectedFont;
	[ObservableProperty] private int selectedSize;
	[ObservableProperty] private ObservableCollection<FontStyleGalleryItem> fontStyles;
	
	public WordPadExampleViewModel()
	{
		fonts = new ObservableCollection<string>();
		fonts.AddRange(FontManager.Current.SystemFonts.Select(x => x.Name).OrderBy(x => x).ToList());
		selectedFont = fonts.FirstOrDefault(f => f == "Arial");
		if (selectedFont == null)
			selectedFont = fonts[0];
		fontSizes = new ObservableCollection<int>
		{
			8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72
		};
		selectedSize = 14;
		fontStyles = new ObservableCollection<FontStyleGalleryItem>
		{
			new FontStyleGalleryItem { Header = "Normal", FontSize = 14 },
			new FontStyleGalleryItem { Header = "Heading", FontSize = 22, Foreground = Brushes.SteelBlue },
			new FontStyleGalleryItem { Header = "Heading 2", FontSize = 18, Foreground = Brushes.SteelBlue },
			new FontStyleGalleryItem { Header = "Title", FontSize = 24 },
			new FontStyleGalleryItem { Header = "Subtitle", FontSize = 14, Foreground = Brushes.DimGray },
			new FontStyleGalleryItem { Header = "Subtle Empha", FontSize = 14, FontStyle = FontStyle.Italic, Foreground = Brushes.DimGray },
			new FontStyleGalleryItem { Header = "Emphasis", FontSize = 14, FontStyle = FontStyle.Italic },
			new FontStyleGalleryItem { Header = "Intense Emphasis", FontSize = 14, FontStyle = FontStyle.Italic, Foreground = Brushes.SteelBlue },
			new FontStyleGalleryItem { Header = "Strong", FontSize = 14, FontWeight = FontWeight.Bold },
			new FontStyleGalleryItem { Header = "Quote", FontSize = 14, FontStyle = FontStyle.Italic },
			new FontStyleGalleryItem { Header = "Intense Refer", FontSize = 16, FontWeight = FontWeight.Bold, Foreground = Brushes.SteelBlue },
			new FontStyleGalleryItem { Header = "Book Title", FontSize = 12, FontWeight = FontWeight.Bold, FontStyle = FontStyle.Italic },
			new FontStyleGalleryItem { Header = "List Paragraph", FontSize = 14 }
		};
	}
}

public class FontStyleGalleryItem : ObservableObject
{
	public string Header { get; set; }
	public double FontSize { get; set; }
	public FontWeight FontWeight { get; set; } = FontWeight.Normal;
	public FontStyle FontStyle { get; set; } = FontStyle.Normal;
	public IBrush Foreground { get; set; }
}