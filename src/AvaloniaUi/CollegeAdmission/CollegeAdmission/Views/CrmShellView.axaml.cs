using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using CollegeAdmission.Models;
using CollegeAdmission.ViewModels;

namespace CollegeAdmission.Views;

public partial class CrmShellView : UserControl
{
    // Matches Flutter shellLayoutForWidth: single pane below 760dp, split
    // views above. Desktop (MinWidth 800) resizes freely through both.
    private const double NarrowThreshold = 760;
    private bool wasNarrow;

    public CrmShellView()
    {
        InitializeComponent();
        NavPanel.AddHandler(Button.ClickEvent, OnNavClick);
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        AppShell.ShellView = this;
        // First paint used stale defaults (drawer toggle/cards invisible)
        // until the first Bounds change fired — apply immediately instead.
        ApplyLayout();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == BoundsProperty)
            ApplyLayout();
    }

    private CrmViewModel Vm => (CrmViewModel)DataContext!;

    private void OnNavToggle(object? sender, RoutedEventArgs e) =>
        NavSplit.IsPaneOpen = !NavSplit.IsPaneOpen;

    private void OnNavClick(object? sender, RoutedEventArgs e)
    {
        // Close the overlay drawer after picking a section on phones.
        if (Bounds.Width < NarrowThreshold)
            NavSplit.IsPaneOpen = false;
    }

    private void ApplyLayout()
    {
        var narrow = Bounds.Width < NarrowThreshold;

        // Nav: inline pane on desktop, overlay drawer on phones.
        NavSplit.DisplayMode = narrow ? SplitViewDisplayMode.Overlay : SplitViewDisplayMode.Inline;
        if (narrow != wasNarrow)
        {
            NavSplit.IsPaneOpen = !narrow;
            wasNarrow = narrow;
        }
        NavToggle.IsVisible = narrow;

        // Top bar owns its height (Auto row): search + 2 CTAs only, so it
        // can never clip or paint over the content row. Narrow stacks the
        // buttons under the search instead of squeezing them aside.
        ShellGrid.RowDefinitions = new RowDefinitions("Auto,*");
        TopGrid.ColumnDefinitions = new ColumnDefinitions(
            narrow ? "Auto,*,*" : "Auto,*,Auto,Auto");
        TopGrid.RowDefinitions = new RowDefinitions(narrow ? "Auto,Auto" : "Auto");
        Grid.SetColumn(SearchBox, 1);
        Grid.SetRow(SearchBox, 0);
        Grid.SetColumnSpan(SearchBox, narrow ? 2 : 1);
        Grid.SetRow(EnquiryButton, narrow ? 1 : 0);
        Grid.SetColumn(EnquiryButton, narrow ? 1 : 2);
        Grid.SetRow(ApplicationButton, narrow ? 1 : 0);
        Grid.SetColumn(ApplicationButton, narrow ? 2 : 3);
        EnquiryButton.Margin = narrow ? new Thickness(0, 8, 8, 0) : new Thickness(0, 0, 8, 0);
        ApplicationButton.Margin = narrow ? new Thickness(0, 8, 0, 0) : new Thickness(0);

        // Rail: right column on desktop, stacked below content on phones.
        BodyGrid.ColumnDefinitions = new ColumnDefinitions(narrow ? "*" : "*,320");
        BodyGrid.RowDefinitions = new RowDefinitions(narrow ? "*,Auto" : "*");
        Grid.SetRow(RailBorder, narrow ? 1 : 0);
        Grid.SetColumn(RailBorder, narrow ? 0 : 1);
        RailBorder.MaxHeight = narrow ? 360 : double.PositiveInfinity;
        RailBorder.BorderThickness = new Thickness(0, narrow ? 1 : 0, 0, 0);

        // Pipeline header: side-by-side on desktop, stacked on phones.
        PipeHeader.ColumnDefinitions = new ColumnDefinitions(narrow ? "*" : "*,Auto");
        Grid.SetRow(PipeHint, narrow ? 1 : 0);
        Grid.SetColumn(PipeHint, narrow ? 0 : 1);

        // Applications: 6-column table on desktop, stacked cards on phones.
        AppsHeader.IsVisible = !narrow;
        AppsRows.IsVisible = !narrow;
        AppsCards.IsVisible = narrow;

        ShellGrid.InvalidateMeasure();
    }

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
        Vm.AddApplicationCommand.Execute(Vm.SelectedApplicant?.CourseId);

    private void OnSyllabusClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: string url } && !string.IsNullOrWhiteSpace(url))
            Vm.OpenSyllabusCommand.Execute(url);
    }

    private void OnApplyClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button { Tag: string courseId })
            Vm.AddApplicationCommand.Execute(courseId);
    }
}
