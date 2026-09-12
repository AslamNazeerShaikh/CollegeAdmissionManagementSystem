namespace CollegeAdmission;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
    }

    private void OnCoursesClick(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(CoursesPage));

    private void OnRegisterClick(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(RegistrationPage));

    private async void OnExitClick(object sender, RoutedEventArgs e)
    {
        var dialog = new ContentDialog
        {
            Title = "Exit",
            Content = "Thanks for using this app. Are you sure you want to exit?",
            PrimaryButtonText = "Exit",
            CloseButtonText = "Stay",
            XamlRoot = this.XamlRoot,
        };
        if (await dialog.ShowAsync() == ContentDialogResult.Primary)
            Application.Current.Exit();
    }
}
