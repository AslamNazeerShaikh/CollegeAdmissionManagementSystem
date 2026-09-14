using CollegeAdmission.Models;
using CollegeAdmission.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CollegeAdmission.ViewModels;

public sealed partial class CoursesViewModel : ObservableObject
{
    private readonly ILauncherService launcher;

    public CoursesViewModel(ILauncherService launcher) => this.launcher = launcher;

    [ObservableProperty]
    private Course? selectedCourse;

    public IReadOnlyList<Course> Courses => CourseCatalog.All;

    [RelayCommand]
    private Task OpenSyllabusAsync(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return Task.CompletedTask;
        return launcher.OpenUrlAsync(url);
    }
}
