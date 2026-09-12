using CollegeAdmission.ViewModels;

namespace CollegeAdmission;

public sealed partial class RegistrationPage : Page
{
    public RegistrationViewModel ViewModel { get; } = new();

    public RegistrationPage()
    {
        this.InitializeComponent();
        DataContext = ViewModel;
    }

    private void OnBackClick(object sender, RoutedEventArgs e)
    {
        if (Frame.CanGoBack)
            Frame.GoBack();
    }

    private async void OnRegisterClick(object sender, RoutedEventArgs e)
    {
        var dialog = new ContentDialog
        {
            Title = "Confirm registration",
            Content = "By going ahead you will get registered. Continue?",
            PrimaryButtonText = "Register",
            CloseButtonText = "Dismiss",
            XamlRoot = this.XamlRoot,
        };
        if (await dialog.ShowAsync() == ContentDialogResult.Primary)
            await ViewModel.SubmitCommand.ExecuteAsync(null);
    }
}
