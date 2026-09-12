using Avalonia;
using Avalonia.Controls;

namespace CollegeAdmission.Views;

public partial class SplashView : UserControl
{
    private bool detached;

    public SplashView()
    {
        InitializeComponent();
    }

    protected override async void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        try
        {
            await Task.Delay(3000);
            if (!detached)
                MainView.RootOf(this).ShowMain();
        }
        catch (TaskCanceledException) { }
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        detached = true;
    }
}
