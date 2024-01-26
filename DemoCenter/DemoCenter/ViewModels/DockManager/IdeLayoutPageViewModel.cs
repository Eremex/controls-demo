using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DemoCenter.ViewModels
{
    public partial class IdeLayoutPageViewModel : PageViewModelBase
    {
        [ObservableProperty] private object focusedSolutionItem;
        
        public IdeLayoutPageViewModel()
        {
            var embeddedFiles = App.EmbeddedResources.Where(x => x.EndsWith(".cs") || x.EndsWith(".axaml"));
            
            var projectNode = new SolutionProjectNode("AvaloniaApplication", string.Empty);
        
            foreach (var s in embeddedFiles)
            {
                var ext = GetExtension(s);
                var nameWithoutExtension = GetFileNameWithoutExtension(s, ext);
                var splitName = nameWithoutExtension.Split(".");

                string currentPath = string.Empty;
                
                for (var i = 0; i < splitName.Length; i++)
                {
                    var last = i == splitName.Length - 1;
                    
                    var name = last ? splitName[i] + ext : splitName[i];
                    var parent =  SolutionNodes.FirstOrDefault(x => x.Path == currentPath) ?? projectNode;
                    
                    currentPath = Path.Join(currentPath, name);
                    var node = SolutionNodes.FirstOrDefault(x => x.Path == currentPath);
                    if (node == null)
                    {
                        node = last ? new SolutionFile(name, currentPath, s) : new SolutionFolder(name, currentPath);
                        node.ParentId = parent.Id;
                        SolutionNodes.Add(node);
                    }
                }
            }

            SolutionNodes.Add(projectNode);
            SolutionNodes.Add(new SolutionDependenciesNode { ParentId = projectNode.Id });

            var focusedItem = SolutionNodes.OfType<SolutionFile>().First();
            FocusedSolutionItem = focusedItem;
            Open(focusedItem);
        }

        public ObservableCollection<SolutionNodeBase> SolutionNodes { get; } = new();
        public ObservableCollection<IdeLayoutDocumentViewModel> Documents { get; } = new();
        
        public void Open(SolutionFile solutionFile)
        {
            var document = Documents.FirstOrDefault(x => x.Uri == solutionFile.Uri);
            if (document == null)
            {
                document = new IdeLayoutDocumentViewModel
                {
                    Header = solutionFile.Filename,
                    Uri = solutionFile.Uri
                };

                document.CloseCommand = new RelayCommand(() => Documents.Remove(document));
                Documents.Add(document);
            }

            document.IsActive = true;
        }
        
        private static string GetExtension(string filename)
        {
            return filename.EndsWith(".axaml.cs") ? ".axaml.cs" : Path.GetExtension(filename);
        }

        private static string GetFileNameWithoutExtension(string filename, string extension)
        {
            return filename.Replace(extension, "");
        }
    }

    public class SolutionProjectNode : SolutionNodeBase
    {
        [Display(Name = "File Name")] public string Filename => $"{Name}.csproj";

        public SolutionProjectNode(string name, string fullPath) : base(name, fullPath) { }
    }

    public class SolutionFolder : SolutionNodeBase
    {
        [Display(Name = "Folder Name")] public string FolderName => Name;

        [Display(Name = "Full Path")] public string FullPath => Path;

        public SolutionFolder(string name, string fullPath) : base(name, fullPath) { }
    }
    
    public enum BuildAction
    {
        Resource,
        [Display(Name="Avalonia XAML")]
        AvaloniaXaml,
        [Display(Name="C# Compiler")]
        CSharpCompiler,
        Content,
        None
    }

    public enum CopyToOutputDirectory
    {
        [Display(Name = "Do not copy")]
        DoNotCopy,
        [Display(Name = "Copy always")]
        CopyAlways,
        [Display(Name = "Copy if newer")]
        CopyIfNewer
    }

    public partial class SolutionFile : SolutionNodeBase
    {
        [ObservableProperty, Display(Name = "Build Action")]
        [property:Category("Advanced")]
        private BuildAction buildAction;
        
        [ObservableProperty, Display(Name = "Copy to Output Directory")]
        [property:Category("Advanced")]
        private CopyToOutputDirectory copyToOutputDirectory;
        
        [ObservableProperty, Display(Name = "Custom Tool")]
        [property:Category("Advanced")]
        private string customTool;
        
        [ObservableProperty, Display(Name = "Custom Tool Namespace")]
        [property:Category("Advanced")]
        private string customToolNamespace;

        public SolutionFile(string name, string fullPath, string uri) : base(name, fullPath)
        {
            if (fullPath.EndsWith(".cs"))
            {
                buildAction = BuildAction.CSharpCompiler;
            }
            else if (fullPath.EndsWith(".axaml"))
            {
                buildAction = BuildAction.AvaloniaXaml;
            }

            Uri = uri;
        }
        
        [Browsable(false)]
        public string Uri { get; }

        [Display(Name = "File Name")]
        public string Filename => Name;
        [Display(Name = "Full Path")]
        public string FullPath => Path;
    }

    public class SolutionDependenciesNode : SolutionNodeBase
    {
        public SolutionDependenciesNode() : base("Dependencies", string.Empty) { }
    }

    public abstract partial class SolutionNodeBase : ObservableObject
    {
        [ObservableProperty]
        [property:Browsable(false)]
        private bool isExpanded;

        protected SolutionNodeBase(string name, string fullPath)
        {
            Name = name;
            Path = fullPath;
            Id = Guid.NewGuid();
        }
        
        [Browsable(false)] public object Id { get; }
        
        [Browsable(false)] public object ParentId { get; set; }

        [Browsable(false)] public string Name { get; }

        [Browsable(false)] public string Path { get; }
    }

    public partial class IdeLayoutDocumentViewModel : ObservableObject
    {
        [ObservableProperty] private string header;
        [ObservableProperty] private string uri;
        [ObservableProperty] private bool isActive;
        [ObservableProperty] private ICommand closeCommand;
    }
}
