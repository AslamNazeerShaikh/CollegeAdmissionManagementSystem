using Avalonia;
using Avalonia.Controls;
using Avalonia.VisualTree;
using CollegeAdmission.ViewModels;

namespace CollegeAdmission.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    public MainViewModel Root => (MainViewModel)DataContext!;

    public static MainViewModel RootOf(Control view) =>
        (MainViewModel)view.GetVisualAncestors().OfType<MainView>().First().DataContext!;

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        if (Root.CurrentView is null)
            Root.ShowSplash();
    }
}
