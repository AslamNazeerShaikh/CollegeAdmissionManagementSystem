using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using CollegeAdmission.Models;

namespace CollegeAdmission.Views;

public partial class CoursesView : UserControl
{
    private Course? selected;

    public CoursesView()
    {
        InitializeComponent();
    }

    private void OnCourseClick(object? sender, RoutedEventArgs e)
    {
        selected = (Course)((Button)sender!).Tag!;
        PopupTitle.Text = selected.CardTitle;
        PopupSubtitle.Text = selected.PopupSubtitle;
        FyButton.IsEnabled = selected.HasFy;
        SyButton.IsEnabled = selected.HasSy;
        TyButton.IsEnabled = selected.HasTy;
        PopupOverlay.IsVisible = true;
    }

    private ILauncher Launcher => TopLevel.GetTopLevel(this)!.Launcher;

    private void OnPdfFy(object? sender, RoutedEventArgs e) =>
        MainView.RootOf(this).OpenSyllabusAsync(selected?.FySyllabusUrl, Launcher).ConfigureAwait(false);

    private void OnPdfSy(object? sender, RoutedEventArgs e) =>
        MainView.RootOf(this).OpenSyllabusAsync(selected?.SySyllabusUrl, Launcher).ConfigureAwait(false);

    private void OnPdfTy(object? sender, RoutedEventArgs e) =>
        MainView.RootOf(this).OpenSyllabusAsync(selected?.TySyllabusUrl, Launcher).ConfigureAwait(false);

    private void OnPopupCancel(object? sender, RoutedEventArgs e) =>
        PopupOverlay.IsVisible = false;

    private void OnPopupRegister(object? sender, RoutedEventArgs e)
    {
        PopupOverlay.IsVisible = false;
        MainView.RootOf(this).ShowRegistration();
    }

    private void OnBackClick(object? sender, RoutedEventArgs e) =>
        MainView.RootOf(this).ShowMain();
}
