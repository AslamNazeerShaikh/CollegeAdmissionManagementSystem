namespace CollegeAdmission;

public sealed partial class SplashPage : Page
{
    // Cancelled when leaving the page: resizing/closing during the 3s delay
    // must not fire a navigate on a dead page.
    private CancellationTokenSource? splashCts;

    public SplashPage()
    {
        this.InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        splashCts?.Cancel();
        splashCts = new CancellationTokenSource();
        try
        {
            await Task.Delay(3000, splashCts.Token);
            App.Navigation.NavigateToMain();
        }
        catch (TaskCanceledException) { }
    }

    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
    {
        splashCts?.Cancel();
        base.OnNavigatingFrom(e);
    }
}
