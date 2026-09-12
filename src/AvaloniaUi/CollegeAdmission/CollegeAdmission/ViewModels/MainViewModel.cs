using Avalonia.Platform.Storage;
using CollegeAdmission.Models;
using CollegeAdmission.Views;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CollegeAdmission.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private object? currentView;

    public RegistrationViewModel Registration { get; } = new();

    public IReadOnlyList<Course> Courses => CourseCatalog.All;

    public void ShowSplash() => CurrentView = new SplashView();
    public void ShowMain() => CurrentView = new MainMenuView();
    public void ShowCourses() => CurrentView = new CoursesView();

    public void ShowRegistration(string? courseName = null)
    {
        if (!string.IsNullOrWhiteSpace(courseName))
            Registration.CourseName = courseName;
        CurrentView = new RegistrationView();
    }

    public Task OpenSyllabusAsync(string? url, ILauncher launcher)
    {
        if (string.IsNullOrWhiteSpace(url))
            return Task.CompletedTask;
        return launcher.LaunchUriAsync(new Uri(url));
    }
}
