using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using CollegeAdmission.Services;
using CollegeAdmission.ViewModels;

namespace CollegeAdmission;

public static class AppShell
{
    public static CrmViewModel Crm { get; } = new CrmViewModel(
        new AvaloniaLauncherService(() =>
            TopLevel.GetTopLevel(ShellView)?.Launcher
            ?? throw new InvalidOperationException("No active TopLevel.")));

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
