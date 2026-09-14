namespace CollegeAdmission.Services;

// Platform abstractions so ViewModels stay unit-testable without Avalonia.
public interface ILauncherService
{
    Task<bool> OpenUrlAsync(string url);
}

public interface INavigationService
{
    void NavigateToSplash();
    void NavigateToMain();
    void NavigateToCourses();
    void NavigateToRegistration(string? courseName = null);
}
