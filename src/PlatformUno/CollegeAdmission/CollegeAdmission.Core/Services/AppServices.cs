namespace CollegeAdmission.Services;

// Platform abstractions so ViewModels stay unit-testable without Uno.
public interface ILauncherService
{
    Task<bool> OpenUrlAsync(string url);
}
