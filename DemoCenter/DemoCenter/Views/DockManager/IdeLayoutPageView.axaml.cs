using Avalonia.Controls;
using Avalonia.Media;
using DemoCenter.ViewModels;
using Eremex.AvaloniaUI.Controls.TreeList;
using System.Reflection;
using System.Text.RegularExpressions;
using Avalonia;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using AvaloniaEdit;
using AvaloniaEdit.Highlighting;
using DemoCenter.Helpers;
using DynamicData;

namespace DemoCenter.Views
{
    public partial class IdeLayoutPageView : UserControl
    {
        public IdeLayoutPageView()
        {
            InitializeComponent();
        }

        private void TreeListControlBase_OnNodeClick(object sender, TreeListNodeClickEventArgs e)
        {
            if(e.IsLeftButtonPressed && e.ClickCount == 2 && e.Node.Content is SolutionFile solutionFile)
            {
                (DataContext as IdeLayoutPageViewModel)?.Open(solutionFile);
            }
        }

        private void TreeListControl_OnCustomColumnSort(object sender, TreeListCustomColumnSortEventArgs e)
        {
            if(e.Node1.Content is SolutionNodeBase v1 && e.Node2.Content is SolutionNodeBase v2)
            {
                e.Result = SolutionNodeComparer.Instance.Compare(v1, v2);
            }
        }

        private sealed class SolutionNodeComparer : IComparer<SolutionNodeBase>
        {

            public static readonly SolutionNodeComparer Instance = new();

            public int Compare(SolutionNodeBase x, SolutionNodeBase y)
            {
                if(x is SolutionDependenciesNode) return -1;
                if(y is SolutionDependenciesNode) return 1;

                if(x is SolutionFile && y is SolutionFile)
                {
                    return string.Compare(x.Path, y.Path, StringComparison.Ordinal);
                }

                if(x is SolutionFolder && y is SolutionFolder)
                {
                    return string.Compare(x.Path, y.Path, StringComparison.Ordinal);
                }

                if(x is SolutionFolder) return -1;
                if(y is SolutionFolder) return 1;

                return 0;
            }
        }
    }

    public class SolutionItemImageSelector : ITreeListNodeImageSelector
    {
        public IImage OpenedFolderImage { get; set; }
        public IImage ClosedFolderImage { get; set; }
        public IImage FileImage { get; set; }
        public IImage CSharpFileImage { get; set; }
        public IImage ProjectImage { get; set; }
        public IImage DependenciesImage { get; set; }

        public IImage SelectImage(TreeListNode node)
        {
            return node?.Content switch
            {
                SolutionFolder => node.IsExpanded ? OpenedFolderImage : ClosedFolderImage,
                SolutionFile file => file.Name.EndsWith(".cs") ? CSharpFileImage : FileImage,
                SolutionDependenciesNode => DependenciesImage,
                SolutionProjectNode => ProjectImage,
                _ => null
            };
        }
    }

    public class IdeLayoutDocumentContentTemplate : MarkupExtension, IDataTemplate
    {
        public Control Build(object param)
        {
            var viewModel = (IdeLayoutDocumentViewModel)param;
            var uriString = viewModel.Uri;

            TextEditor codeViewEditor = new();
            codeViewEditor.Tag = uriString;
            codeViewEditor.ActualThemeVariantChanged += CodeViewEditorOnActualThemeVariantChanged;
            codeViewEditor.DetachedFromVisualTree += CodeViewEditorOnDetachedFromVisualTree;
            UpdateCodeViewEditor(codeViewEditor);

            return codeViewEditor;
        }

        private void UpdateCodeViewEditor(TextEditor codeViewEditor)
        {
            string uriString = (string)codeViewEditor.Tag;
            if(uriString == null)
                return;
            using var stream = Assembly.GetAssembly(typeof(App))?.GetManifestResourceStream(uriString);
            if(uriString.EndsWith(".cs"))
                codeViewEditor.SyntaxHighlighting =
                    new ThemedSyntaxHighlighter("CSharp-Highlight").HighlightingDefinition;
            else if(uriString.EndsWith(".axaml"))
                codeViewEditor.SyntaxHighlighting =
                    new ThemedSyntaxHighlighter("Axaml-Highlight").HighlightingDefinition;
            else
                codeViewEditor.SyntaxHighlighting = null;
            codeViewEditor.Load(stream);
        }

        private void CodeViewEditorOnDetachedFromVisualTree(object sender, VisualTreeAttachmentEventArgs e)
        {
            if(sender is TextEditor te)
            {
                te.ActualThemeVariantChanged -= CodeViewEditorOnActualThemeVariantChanged;
                te.DetachedFromVisualTree -= CodeViewEditorOnDetachedFromVisualTree;
            }
        }

        private void CodeViewEditorOnActualThemeVariantChanged(object sender, EventArgs e)
        {
            UpdateCodeViewEditor((TextEditor)sender);
        }

        public bool Match(object data)
        {
            return data is IdeLayoutDocumentViewModel;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
