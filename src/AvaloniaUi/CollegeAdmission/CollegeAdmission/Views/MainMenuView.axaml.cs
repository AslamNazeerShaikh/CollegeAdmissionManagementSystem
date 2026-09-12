using Avalonia.Controls;
using Avalonia.Interactivity;

namespace CollegeAdmission.Views;

public partial class MainMenuView : UserControl
{
    public MainMenuView()
    {
        InitializeComponent();
        ExitMessage.Text = "College of Computer Science\n& Information Technology\n\nOnline Admission Management System\n\nThis App is made possible by the Android open source project and other open source software. Made with ❤ in COCSIT. Thanks for using this app.\n\nAre sure you want to exit ?";
    }

    private void OnRegistrationClick(object? sender, RoutedEventArgs e) =>
        MainView.RootOf(this).ShowCourses();

    private void OnWalkthroughClick(object? sender, RoutedEventArgs e) =>
        MainView.RootOf(this).ShowSplash();

    private void OnBackClick(object? sender, RoutedEventArgs e) =>
        ExitOverlay.IsVisible = true;

    private void OnExitAccept(object? sender, RoutedEventArgs e) =>
        Environment.Exit(0);

    private void OnExitCancel(object? sender, RoutedEventArgs e) =>
        ExitOverlay.IsVisible = false;
}
