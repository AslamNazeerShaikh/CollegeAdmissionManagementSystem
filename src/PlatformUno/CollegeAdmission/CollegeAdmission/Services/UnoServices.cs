using CollegeAdmission.Services;
using Microsoft.UI.Xaml.Controls;
using Windows.System;

namespace CollegeAdmission;

public sealed class UnoLauncherService : ILauncherService
{
    public async Task<bool> OpenUrlAsync(string url) =>
        await Launcher.LaunchUriAsync(new Uri(url));
}

public sealed class FrameNavigationService : INavigationService
{
    private Frame? Frame => App.RootFrame;

    public void NavigateToSplash() => Frame?.Navigate(typeof(SplashPage));

    public void NavigateToMain() => Frame?.Navigate(typeof(MainMenuPage));

    public void NavigateToCourses() => Frame?.Navigate(typeof(CoursesPage));

    public void NavigateToRegistration(string? courseName = null) =>
        Frame?.Navigate(typeof(RegistrationPage), courseName ?? string.Empty);

    public void GoBack()
    {
        if (Frame?.CanGoBack == true)
            Frame.GoBack();
    }
}
