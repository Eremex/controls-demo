using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData;

namespace DemoCenter.ViewModels.Ribbon;

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
		var fs = new[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
		fontSizes = new ObservableCollection<int>(fs);
		selectedSize = 14;
		fontStyles = new ObservableCollection<FontStyleGalleryItem>();
		fontStyles.Add(new NormalGalleryItem() { Header = "Normal" });
		fontStyles.Add(new HeadingGalleryItem() { Header = "Heading" });
		fontStyles.Add(new Heading2GalleryItem() { Header = "Heading 2" });
		fontStyles.Add(new TitleGalleryItem() { Header = "Title" });
		fontStyles.Add(new SubtitleGalleryItem() { Header = "Subtitle" });
		fontStyles.Add(new SubtleEmphaGalleryItem() { Header = "Subtle Empha" });
		fontStyles.Add(new EmphasisGalleryItem() { Header = "Emphasis" });
		fontStyles.Add(new IntenseEmphasisGalleryItem() { Header = "Intense Emphasis" });
		fontStyles.Add(new StrongGalleryItem() { Header = "Strong" });
		fontStyles.Add(new QuoteGalleryItem() { Header = "Quote" });
		fontStyles.Add(new IntenseReferGalleryItem() { Header = "Intense Refer" });
		fontStyles.Add(new BookTitleGalleryItem() { Header = "Book Title" });
		fontStyles.Add(new ListParagraphGalleryItem() { Header = "List Paragraph" });
	}
}

public partial class FontStyleGalleryItem : ObservableObject
{
	[ObservableProperty] private string header;
}

public class NormalGalleryItem : FontStyleGalleryItem { }

public class HeadingGalleryItem : FontStyleGalleryItem { }

public class Heading2GalleryItem : FontStyleGalleryItem { }

public class TitleGalleryItem : FontStyleGalleryItem { }

public class SubtitleGalleryItem : FontStyleGalleryItem { }

public class SubtleEmphaGalleryItem : FontStyleGalleryItem { }

public class EmphasisGalleryItem : FontStyleGalleryItem { }

public class IntenseEmphasisGalleryItem : FontStyleGalleryItem { }

public class StrongGalleryItem : FontStyleGalleryItem { }

public class QuoteGalleryItem : FontStyleGalleryItem { }

public class IntenseReferGalleryItem : FontStyleGalleryItem { }

public class BookTitleGalleryItem : FontStyleGalleryItem { }

public class ListParagraphGalleryItem : FontStyleGalleryItem { }