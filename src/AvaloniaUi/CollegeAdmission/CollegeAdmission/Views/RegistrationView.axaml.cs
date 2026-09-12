using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CollegeAdmission.ViewModels;

namespace CollegeAdmission.Views;

public partial class RegistrationView : UserControl
{
    public RegistrationView()
    {
        InitializeComponent();
    }

    private RegistrationViewModel Vm => (RegistrationViewModel)DataContext!;

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        DataContext = MainView.RootOf(this).Registration;
    }

    private void OnNextClick(object? sender, RoutedEventArgs e) =>
        ConfirmOverlay.IsVisible = true;

    private void OnConfirmDismiss(object? sender, RoutedEventArgs e) =>
        ConfirmOverlay.IsVisible = false;

    private async void OnConfirmRegister(object? sender, RoutedEventArgs e)
    {
        ConfirmOverlay.IsVisible = false;
        await Vm.SubmitCommand.ExecuteAsync(null);
    }

    private void OnCancelClick(object? sender, RoutedEventArgs e) =>
        CancelOverlay.IsVisible = true;

    private void OnCancelDismiss(object? sender, RoutedEventArgs e) =>
        CancelOverlay.IsVisible = false;

    private void OnCancelContinue(object? sender, RoutedEventArgs e) =>
        MainView.RootOf(this).ShowCourses();
}
