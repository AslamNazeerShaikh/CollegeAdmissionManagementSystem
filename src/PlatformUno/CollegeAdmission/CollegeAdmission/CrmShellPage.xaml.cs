using System.ComponentModel;
using CollegeAdmission.Models;
using CollegeAdmission.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace CollegeAdmission;

public sealed partial class CrmShellPage : Page
{
    // Matches Flutter shellLayoutForWidth: single pane below 760px.
    private const double NarrowThreshold = 760;
    private bool wasNarrow = true;

    internal CrmViewModel Vm => (CrmViewModel)DataContext!;

    public CrmShellPage()
    {
        DataContext = new CrmViewModel(App.Launcher);
        this.InitializeComponent();
        Vm.PropertyChanged += OnVmChanged;
        Loaded += (_, _) => { PaintActive(); ApplyLayout(); };
        SizeChanged += (_, _) => ApplyLayout();
    }

    private void OnVmChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(CrmViewModel.SelectedSection)
            or nameof(CrmViewModel.SelectedStage)
            or nameof(CrmViewModel.CountEnquiry)
            or nameof(CrmViewModel.CountApplied)
            or nameof(CrmViewModel.CountVerified)
            or nameof(CrmViewModel.CountMerit)
            or nameof(CrmViewModel.CountOffered)
            or nameof(CrmViewModel.CountFeePaid)
            or nameof(CrmViewModel.CountEnrolled))
            PaintActive();
    }

    // Active highlight is painted explicitly (same as Avalonia): WinUI has
    // no class bindings, and theme ToggleButton chrome would tint the CRM
    // surfaces with the Material purple instead of the green reference.
    private void PaintActive()
    {
        PaintNav(NavDashboard, Vm.ShowDashboard);
        PaintNav(NavPipeline, Vm.ShowPipeline);
        PaintNav(NavApplications, Vm.ShowApplications);
        PaintNav(NavCourses, Vm.ShowCourses);
        PaintNav(NavFees, Vm.ShowFees);
        PaintTab(TabEnquiry, $"Enquiry · {Vm.CountEnquiry}", Vm.SelectedStage == CrmStage.Enquiry);
        PaintTab(TabApplied, $"Applied · {Vm.CountApplied}", Vm.SelectedStage == CrmStage.Applied);
        PaintTab(TabVerified, $"Verified · {Vm.CountVerified}", Vm.SelectedStage == CrmStage.Verified);
        PaintTab(TabMerit, $"★ Merit · {Vm.CountMerit}", Vm.SelectedStage == CrmStage.Merit);
        PaintTab(TabOffered, $"Offered · {Vm.CountOffered}", Vm.SelectedStage == CrmStage.Offered);
        PaintTab(TabFeePaid, $"Fee paid · {Vm.CountFeePaid}", Vm.SelectedStage == CrmStage.FeePaid);
        PaintTab(TabEnrolled, $"Enrolled · {Vm.CountEnrolled}", Vm.SelectedStage == CrmStage.Enrolled);
    }

    // Buttons inside an x:Load-unloaded section are null until that
    // section materializes; the post-navigation repaint covers them then.
    private static void PaintNav(Button? b, bool active)
    {
        if (b is null) return;
        b.Background = (Brush)Application.Current.Resources[active ? "CrmPaperBg" : "CrmSurface"];
        b.Foreground = (Brush)Application.Current.Resources["CrmInk"];
        b.FontWeight = active ? Microsoft.UI.Text.FontWeights.SemiBold : Microsoft.UI.Text.FontWeights.Normal;
    }

    private static void PaintTab(Button? b, string label, bool active)
    {
        if (b is null) return;
        var res = Application.Current.Resources;
        b.Content = label;
        b.Background = (Brush)res[active ? "CrmAccentSoft" : "CrmSurface"];
        b.Foreground = (Brush)res[active ? "CrmAccent" : "CrmInk2"];
        b.BorderBrush = (Brush)res[active ? "CrmAccent" : "CrmLine"];
        b.BorderThickness = new Thickness(1);
        b.FontWeight = active ? Microsoft.UI.Text.FontWeights.SemiBold : Microsoft.UI.Text.FontWeights.Normal;
    }

    private void OnNavToggle(object sender, RoutedEventArgs e) =>
        NavSplit.IsPaneOpen = !NavSplit.IsPaneOpen;

    private void OnNavSection(object sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: string section })
            Vm.SelectSectionCommand.Execute(section);
        // Close the overlay drawer after picking a section when narrow.
        if (ActualWidth < NarrowThreshold)
            NavSplit.IsPaneOpen = false;
        // x:Load just materialized a new section: repaint + re-apply layout
        // afterwards so hidden-while-unloaded elements get their mode.
        DispatcherQueue.TryEnqueue(() => { PaintActive(); ApplyLayout(); });
    }

    private void OnStageTab(object sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: string stage })
            Vm.SelectStageCommand.Execute(stage);
    }

    private void ApplyLayout()
    {
        var narrow = ActualWidth < NarrowThreshold;

        NavSplit.DisplayMode = narrow
            ? SplitViewDisplayMode.Overlay
            : SplitViewDisplayMode.Inline;
        if (narrow != wasNarrow)
        {
            NavSplit.IsPaneOpen = !narrow;
            wasNarrow = narrow;
        }
        NavToggle.Visibility = narrow ? Visibility.Visible : Visibility.Collapsed;

        // Top bar stacks the buttons under the search when narrow.
        TopGrid.ColumnDefinitions.Clear();
        foreach (var def in narrow ? new[] { "Auto,*,*" } : new[] { "Auto,*,Auto,Auto" })
            foreach (var w in def.Split(','))
                TopGrid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = w == "*" ? new GridLength(1, GridUnitType.Star) : GridLength.Auto
                });
        TopGrid.RowDefinitions.Clear();
        TopGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        if (narrow)
            TopGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        Grid.SetColumn(SearchBox, 1);
        Grid.SetRow(SearchBox, 0);
        Grid.SetColumnSpan(SearchBox, narrow ? 2 : 1);
        Grid.SetRow(EnquiryButton, narrow ? 1 : 0);
        Grid.SetColumn(EnquiryButton, narrow ? 1 : 2);
        Grid.SetRow(ApplicationButton, narrow ? 1 : 0);
        Grid.SetColumn(ApplicationButton, narrow ? 2 : 3);
        EnquiryButton.Margin = narrow ? new Thickness(0, 8, 8, 0) : new Thickness(0, 0, 8, 0);
        ApplicationButton.Margin = narrow ? new Thickness(0, 8, 0, 0) : new Thickness(0);

        // Rail: right column on desktop, stacked below content when narrow.
        BodyGrid.ColumnDefinitions.Clear();
        BodyGrid.ColumnDefinitions.Add(new ColumnDefinition
        {
            Width = new GridLength(1, GridUnitType.Star)
        });
        if (!narrow)
            BodyGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(320) });
        BodyGrid.RowDefinitions.Clear();
        BodyGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
        if (narrow)
            BodyGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        Grid.SetRow(RailBorder, narrow ? 1 : 0);
        Grid.SetColumn(RailBorder, narrow ? 0 : 1);
        RailBorder.MaxHeight = narrow ? 360 : double.PositiveInfinity;
        RailBorder.BorderThickness = narrow
            ? new Thickness(0, 1, 0, 0)
            : new Thickness(1, 0, 0, 0);

        // Pipeline header: side-by-side on desktop, stacked when narrow.
        // Null while the Pipeline section is x:Load-unloaded (same rule as
        // the apps table below).
        if (PipeHeader is not null && PipeHint is not null)
        {
            PipeHeader.ColumnDefinitions.Clear();
            PipeHeader.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            });
            if (!narrow)
                PipeHeader.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            PipeHeader.RowDefinitions.Clear();
            PipeHeader.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            if (narrow)
                PipeHeader.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            Grid.SetRow(PipeHint, narrow ? 1 : 0);
            Grid.SetColumn(PipeHint, narrow ? 0 : 1);
        }

        // Applications: 6-column table on desktop, stacked cards when narrow.
        // Null while the Applications section is x:Load-unloaded; the
        // post-navigation ApplyLayout pass covers them once materialized.
        if (AppsHeader is not null)
            AppsHeader.Visibility = narrow ? Visibility.Collapsed : Visibility.Visible;
        if (AppsRows is not null)
            AppsRows.Visibility = narrow ? Visibility.Collapsed : Visibility.Visible;
        if (AppsCards is not null)
            AppsCards.Visibility = narrow ? Visibility.Visible : Visibility.Collapsed;
    }

    private void OnApplicantClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: Applicant applicant })
            Vm.SelectApplicantCommand.Execute(applicant);
    }

    private void OnAdvanceClick(object sender, RoutedEventArgs e) =>
        Vm.AdvanceStageCommand.Execute(Vm.SelectedApplicant);

    private void OnCollectClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: Applicant applicant })
            Vm.CollectFeeCommand.Execute(applicant);
    }

    private void OnRailCollectClick(object sender, RoutedEventArgs e) =>
        Vm.CollectFeeCommand.Execute(Vm.SelectedApplicant);

    private void OnRailRegisterClick(object sender, RoutedEventArgs e) =>
        Vm.AddApplicationCommand.Execute(Vm.SelectedApplicant?.CourseId);

    private void OnSyllabusClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: string url } && !string.IsNullOrWhiteSpace(url))
            Vm.OpenSyllabusCommand.Execute(url);
    }

    private void OnApplyClick(object sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: string courseId })
            Vm.AddApplicationCommand.Execute(courseId);
    }
}
