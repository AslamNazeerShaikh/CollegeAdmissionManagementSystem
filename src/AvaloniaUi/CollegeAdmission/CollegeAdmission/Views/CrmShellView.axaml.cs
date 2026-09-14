using Avalonia.Controls;
using Avalonia.Interactivity;
using CollegeAdmission.Models;
using CollegeAdmission.ViewModels;

namespace CollegeAdmission.Views;

public partial class CrmShellView : UserControl
{
    public CrmShellView()
    {
        InitializeComponent();
    }

    private CrmViewModel Vm => (CrmViewModel)DataContext!;

    private static MainViewModel RootOf(Control view) => MainView.RootOf(view);

    private void OnApplicantClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: Applicant applicant })
            Vm.SelectApplicantCommand.Execute(applicant);
    }

    private void OnAdvanceClick(object? sender, RoutedEventArgs e) =>
        Vm.AdvanceStageCommand.Execute(Vm.SelectedApplicant);

    private void OnCollectClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: Applicant applicant })
            Vm.CollectFeeCommand.Execute(applicant);
    }

    private void OnRailCollectClick(object? sender, RoutedEventArgs e) =>
        Vm.CollectFeeCommand.Execute(Vm.SelectedApplicant);

    private void OnRailRegisterClick(object? sender, RoutedEventArgs e) =>
        RootOf(this).ShowRegistration(Vm.SelectedApplicant?.Course);

    private void OnSyllabusClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: string url } && !string.IsNullOrWhiteSpace(url))
            Vm.OpenSyllabusCommand.Execute(url);
    }

    private void OnApplyClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: string course })
            RootOf(this).ShowRegistration(course);
    }

    private void OnClassicApp(object? sender, RoutedEventArgs e) => RootOf(this).ShowMain();
    private void OnClassicCourses(object? sender, RoutedEventArgs e) => RootOf(this).ShowCourses();
    private void OnClassicRegister(object? sender, RoutedEventArgs e) => RootOf(this).ShowRegistration();
}
