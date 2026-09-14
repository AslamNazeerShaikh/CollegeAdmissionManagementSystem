using CollegeAdmission.Models;
using CollegeAdmission.ViewModels;
using Microsoft.UI.Xaml.Controls;

namespace CollegeAdmission;

public sealed partial class CoursesPage : Page
{
    public CoursesViewModel ViewModel { get; } = new(App.Launcher);

    private Course? selected;

    public CoursesPage()
    {
        this.InitializeComponent();
        DataContext = ViewModel;
    }

    private async void OnCourseClick(object sender, ItemClickEventArgs e)
    {
        selected = (Course)e.ClickedItem;
        ViewModel.SelectedCourse = selected;
        PopupTitle.Text = selected.CardTitle;
        PopupSubtitle.Text = selected.PopupSubtitle;
        FyButton.IsEnabled = selected.HasFy;
        FyButton.Visibility = selected.HasFy ? Visibility.Visible : Visibility.Collapsed;
        SyButton.IsEnabled = selected.HasSy;
        SyButton.Visibility = selected.HasSy ? Visibility.Visible : Visibility.Collapsed;
        TyButton.IsEnabled = selected.HasTy;
        TyButton.Visibility = selected.HasTy ? Visibility.Visible : Visibility.Collapsed;
        // Mirrors the legacy app: the caution animation only plays when the
        // FY syllabus is missing (e.g. MBA). Kept in-flow, never overlapping.
        if (!selected.HasFy)
        {
            CautionAnim.Visibility = Visibility.Visible;
            await CautionAnim.PlayAsync(0, 1, true);
        }
        else
        {
            CautionAnim.Visibility = Visibility.Collapsed;
        }
        PopupOverlay.Visibility = Visibility.Visible;
    }

    private void OnPdfFy(object sender, RoutedEventArgs e) =>
        ViewModel.OpenSyllabusCommand.Execute(selected?.FySyllabusUrl);

    private void OnPdfSy(object sender, RoutedEventArgs e) =>
        ViewModel.OpenSyllabusCommand.Execute(selected?.SySyllabusUrl);

    private void OnPdfTy(object sender, RoutedEventArgs e) =>
        ViewModel.OpenSyllabusCommand.Execute(selected?.TySyllabusUrl);

    private void OnPopupCancel(object sender, RoutedEventArgs e)
    {
        CautionAnim.Visibility = Visibility.Collapsed;
        PopupOverlay.Visibility = Visibility.Collapsed;
    }

    private void OnPopupRegister(object sender, RoutedEventArgs e)
    {
        CautionAnim.Visibility = Visibility.Collapsed;
        PopupOverlay.Visibility = Visibility.Collapsed;
        App.Navigation.NavigateToRegistration(selected?.Name);
    }

    private void OnBackClick(object sender, RoutedEventArgs e)
    {
        if (Frame.CanGoBack)
            Frame.GoBack();
        else
            App.Navigation.NavigateToMain();
    }
}
