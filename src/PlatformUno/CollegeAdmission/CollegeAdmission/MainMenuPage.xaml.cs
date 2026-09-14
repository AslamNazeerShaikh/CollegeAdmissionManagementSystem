using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CollegeAdmission;

public sealed partial class MainMenuPage : Page
{
    public MainMenuPage()
    {
        this.InitializeComponent();
        ExitMessage.Text = "College of Computer Science\n& Information Technology\n\nOnline Admission Management System\n\nThis App is made possible by the Android open source project and other open source software. Made with ❤ in COCSIT. Thanks for using this app.\n\nAre sure you want to exit ?";
    }

    private void OnRegistrationClick(object sender, RoutedEventArgs e) =>
        App.Navigation.NavigateToCourses();

    private void OnWalkthroughClick(object sender, RoutedEventArgs e) =>
        App.Navigation.NavigateToSplash();

    private void OnBackClick(object sender, RoutedEventArgs e) =>
        ExitOverlay.Visibility = Visibility.Visible;

    private void OnExitAccept(object sender, RoutedEventArgs e) =>
        Application.Current.Exit();

    private void OnExitCancel(object sender, RoutedEventArgs e) =>
        ExitOverlay.Visibility = Visibility.Collapsed;
}
