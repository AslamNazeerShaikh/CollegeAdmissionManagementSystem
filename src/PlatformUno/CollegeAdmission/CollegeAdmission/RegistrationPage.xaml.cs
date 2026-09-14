using CollegeAdmission.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace CollegeAdmission;

public sealed partial class RegistrationPage : Page
{
    public RegistrationViewModel ViewModel { get; } = new();

    public RegistrationPage()
    {
        this.InitializeComponent();
        DataContext = ViewModel;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        if (e.Parameter is string courseName && !string.IsNullOrWhiteSpace(courseName))
            ViewModel.CourseName = courseName;
    }

    private void OnNextClick(object sender, RoutedEventArgs e) =>
        ConfirmOverlay.Visibility = Visibility.Visible;

    private void OnConfirmDismiss(object sender, RoutedEventArgs e) =>
        ConfirmOverlay.Visibility = Visibility.Collapsed;

    private async void OnConfirmRegister(object sender, RoutedEventArgs e)
    {
        ConfirmOverlay.Visibility = Visibility.Collapsed;
        await ViewModel.SubmitCommand.ExecuteAsync(null);
    }

    private void OnCancelClick(object sender, RoutedEventArgs e) =>
        CancelOverlay.Visibility = Visibility.Visible;

    private void OnCancelDismiss(object sender, RoutedEventArgs e) =>
        CancelOverlay.Visibility = Visibility.Collapsed;

    private void OnCancelContinue(object sender, RoutedEventArgs e) =>
        App.Navigation.NavigateToCourses();
}
