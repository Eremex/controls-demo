using Avalonia.Controls.Primitives;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DemoCenter.ViewModels;

public partial class MemoEditorPageViewModel : PageViewModelBase
{
    [ObservableProperty] private ScrollBarVisibility horizontalScrollbarVisibility = ScrollBarVisibility.Auto;
    [ObservableProperty] private ScrollBarVisibility verticalScrollbarVisibility = ScrollBarVisibility.Auto;

    [ObservableProperty] private ScrollBarVisibility[] scrollbarVisibilityItems = new[]
    {
        ScrollBarVisibility.Auto, ScrollBarVisibility.Visible, ScrollBarVisibility.Hidden, ScrollBarVisibility.Disabled
    };

    [ObservableProperty] private string text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, " + Environment.NewLine +
                                               "quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum " + Environment.NewLine +
                                               "dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. " + Environment.NewLine +
                                               "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore " + Environment.NewLine +
                                               "veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem " + Environment.NewLine +
                                               "quia voluptas sit aspernatur aut odit aut fugit, " + Environment.NewLine +
                                               "sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. " + Environment.NewLine +
                                               "Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, " + Environment.NewLine +
                                               "consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et " + Environment.NewLine +
                                               "dolore magnam aliquam quaerat voluptatem. Ut enim ad " + Environment.NewLine +
                                               "minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid " + Environment.NewLine +
                                               "ex ea commodi consequatur? Quis autem vel eum iure " + Environment.NewLine +
                                               "reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum " + Environment.NewLine +
                                               "qui dolorem eum fugiat quo voluptas nulla pariatur? " + Environment.NewLine +
                                               "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis " + Environment.NewLine +
                                               "praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias " + Environment.NewLine +
                                               "excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui " + Environment.NewLine +
                                               "officia deserunt mollitia animi, id est laborum et dolorum fuga. " + Environment.NewLine +
                                               "Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum " + Environment.NewLine +
                                               "soluta nobis est eligendi optio cumque nihil impedit quo minus id " + Environment.NewLine +
                                               "quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor " + Environment.NewLine +
                                               "repellendus. Temporibus autem quibusdam et aut officiis debitis aut " + Environment.NewLine +
                                               "rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae " + Environment.NewLine +
                                               "non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, " + Environment.NewLine +
                                               "ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat.";
}