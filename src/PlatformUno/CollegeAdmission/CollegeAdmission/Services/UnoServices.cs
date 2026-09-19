using CollegeAdmission.Services;
using Windows.System;

namespace CollegeAdmission;

public sealed class UnoLauncherService : ILauncherService
{
    public async Task<bool> OpenUrlAsync(string url) =>
        await Launcher.LaunchUriAsync(new Uri(url));
}
