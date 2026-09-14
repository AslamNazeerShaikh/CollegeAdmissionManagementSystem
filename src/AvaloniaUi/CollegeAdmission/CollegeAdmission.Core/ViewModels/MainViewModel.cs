using CollegeAdmission.Models;
using CollegeAdmission.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CollegeAdmission.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly ILauncherService launcher;
    private readonly INavigationService navigation;

    public MainViewModel(ILauncherService launcher, INavigationService navigation)
    {
        this.launcher = launcher;
        this.navigation = navigation;
    }

    [ObservableProperty]
    private object? currentView;

    public RegistrationViewModel Registration { get; } = new();

    public IReadOnlyList<Course> Courses => CourseCatalog.All;

    public void ShowSplash() => navigation.NavigateToSplash();

    public void ShowMain() => navigation.NavigateToMain();

    public void ShowCourses() => navigation.NavigateToCourses();

    public void ShowRegistration(string? courseName = null) =>
        navigation.NavigateToRegistration(courseName);

    [RelayCommand]
    private Task OpenSyllabusAsync(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return Task.CompletedTask;
        return launcher.OpenUrlAsync(url);
    }
}
