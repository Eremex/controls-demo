using Avalonia.Controls;
using Avalonia.VisualTree;
using DemoCenter.ViewModels;

namespace DemoCenter.Views
{
    public partial class HyperlinkEditorPageView : UserControl
    {

        HyperlinkEditorPageViewModel ViewModel { get; set; }
        public HyperlinkEditorPageView()
        {
            InitializeComponent();
        }

        protected override void OnDataContextChanged(EventArgs e)
        {
            base.OnDataContextChanged(e);
            UnsubscribeEvents(ViewModel);
            ViewModel = DataContext as HyperlinkEditorPageViewModel;
            SubscribeEvents(ViewModel);
        }

        private void SubscribeEvents(HyperlinkEditorPageViewModel viewModel)
        {
            if (viewModel == null)
                return;
            viewModel.SelectDemo += OnViewModelSelectDemo;
        }

        private void UnsubscribeEvents(HyperlinkEditorPageViewModel viewModel)
        {
            if (viewModel == null)
                return;
            viewModel.SelectDemo -= OnViewModelSelectDemo;
        }

        private void OnViewModelSelectDemo(string moduleName)
        {
            if(this.GetVisualAncestors().FirstOrDefault(x => x is MainView) is MainView view)
                (view.DataContext as MainViewModel)?.SelectProduct(moduleName);
        }
    }
}
