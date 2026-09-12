using CollegeAdmission.Models;
using Windows.System;

namespace CollegeAdmission.ViewModels;

public sealed partial class CoursesViewModel : ObservableObject
{
    [ObservableProperty]
    private Course? selectedCourse;

    public IReadOnlyList<Course> Courses => CourseCatalog.All;

    [RelayCommand]
    private async Task OpenSyllabusAsync(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return;
        await Launcher.LaunchUriAsync(new Uri(url));
    }
}
