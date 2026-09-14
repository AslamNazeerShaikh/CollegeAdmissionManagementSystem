namespace CollegeAdmission;

public sealed partial class SplashPage : Page
{
    private bool navigated;

    public SplashPage()
    {
        this.InitializeComponent();
    }

    protected override async void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        try
        {
            await Task.Delay(3000);
            if (!navigated)
            {
                navigated = true;
                App.Navigation.NavigateToMain();
            }
        }
        catch (TaskCanceledException) { }
    }
}
