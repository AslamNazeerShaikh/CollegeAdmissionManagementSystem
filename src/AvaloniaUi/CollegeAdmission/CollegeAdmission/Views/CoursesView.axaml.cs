using Avalonia.Controls;
using Avalonia.Interactivity;
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
        // Mirrors the legacy app: the caution animation only plays when the
        // FY syllabus is missing. Kept in-flow, never overlapping.
        if (!selected.HasFy)
        {
            CautionAnim.IsVisible = true;
            CautionAnim.Start();
        }
        else
        {
            CautionAnim.Stop();
            CautionAnim.IsVisible = false;
        }
        PopupOverlay.IsVisible = true;
    }

    private void OnPdfFy(object? sender, RoutedEventArgs e) =>
        MainView.RootOf(this).OpenSyllabusCommand.Execute(selected?.FySyllabusUrl);

    private void OnPdfSy(object? sender, RoutedEventArgs e) =>
        MainView.RootOf(this).OpenSyllabusCommand.Execute(selected?.SySyllabusUrl);

    private void OnPdfTy(object? sender, RoutedEventArgs e) =>
        MainView.RootOf(this).OpenSyllabusCommand.Execute(selected?.TySyllabusUrl);

    private void OnPopupCancel(object? sender, RoutedEventArgs e)
    {
        CautionAnim.Stop();
        CautionAnim.IsVisible = false;
        PopupOverlay.IsVisible = false;
    }

    private void OnPopupRegister(object? sender, RoutedEventArgs e)
    {
        CautionAnim.Stop();
        CautionAnim.IsVisible = false;
        PopupOverlay.IsVisible = false;
        MainView.RootOf(this).ShowRegistration();
    }

    private void OnBackClick(object? sender, RoutedEventArgs e) =>
        MainView.RootOf(this).ShowMain();
}
