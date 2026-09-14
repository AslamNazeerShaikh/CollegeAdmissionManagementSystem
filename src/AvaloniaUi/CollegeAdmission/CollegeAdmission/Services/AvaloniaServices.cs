using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using CollegeAdmission.Services;
using CollegeAdmission.ViewModels;
using CollegeAdmission.Views;

namespace CollegeAdmission;

public static class AppShell
{
    public static MainViewModel Main { get; } = new MainViewModel(
        new AvaloniaLauncherService(() =>
            TopLevel.GetTopLevel(ShellView)?.Launcher
            ?? throw new InvalidOperationException("No active TopLevel.")),
        new ViewNavigationService());

    public static Control? ShellView { get; set; }
}

public sealed class AvaloniaLauncherService(Func<ILauncher?> resolveLauncher) : ILauncherService
{
    public Task<bool> OpenUrlAsync(string url)
    {
        var launcher = resolveLauncher()
            ?? throw new InvalidOperationException("No active launcher.");
        return launcher.LaunchUriAsync(new Uri(url));
    }
}

public sealed class ViewNavigationService : INavigationService
{
    private static MainViewModel Vm => AppShell.Main;

    public void NavigateToSplash() => Vm.CurrentView = new SplashView();

    public void NavigateToMain() => Vm.CurrentView = new MainMenuView();

    public void NavigateToCourses() => Vm.CurrentView = new CoursesView();

    public void NavigateToRegistration(string? courseName = null)
    {
        if (!string.IsNullOrWhiteSpace(courseName))
            Vm.Registration.CourseName = courseName;
        Vm.CurrentView = new RegistrationView();
    }
}
