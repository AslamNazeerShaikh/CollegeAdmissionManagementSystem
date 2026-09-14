using CollegeAdmission.Models;
using CollegeAdmission.Services;
using CollegeAdmission.ViewModels;

namespace CollegeAdmission.Tests;

public class CatalogTests
{
    [Test]
    public void Catalog_CoversAllLegacyCourses()
    {
        var names = CourseCatalog.All.Select(c => c.Name).ToList();
        foreach (var expected in new[]
        {
            "B.Sc. Computer Science", "B.Sc. Software Engineering", "B.Sc. Network Technology",
            "M.Sc. Computer Science", "M.Sc. Software Engineering", "M.Sc. System Admin. & Networking",
            "M.Sc. Computer Management", "BCA", "B.Sc. Biotechnology", "M.Sc. Biotechnology",
            "BBA", "MBA (YCMOU)",
        })
            Assert.That(names, Does.Contain(expected), $"missing {expected}");
    }

    [Test]
    public void Registration_RequiresKeyFields()
    {
        var vm = new RegistrationViewModel();
        Assert.That(vm.Validate(out _), Is.False);

        vm.FirstName = "Aslam";
        vm.LastName = "Shaikh";
        vm.BirthDay = "01"; vm.BirthMonth = "05"; vm.BirthYear = "1998";
        vm.PrimaryPhone = "9876543210";
        vm.CourseName = "BCA";
        Assert.That(vm.Validate(out var errors), Is.True, string.Join("; ", errors.Select(e => e.ErrorMessage)));
    }

    [Test]
    public async Task OpenSyllabus_SkipsEmpty_OpensValid()
    {
        var fake = new FakeLauncher();
        var vm = new CoursesViewModel(fake);

        await vm.OpenSyllabusCommand.ExecuteAsync(null);
        await vm.OpenSyllabusCommand.ExecuteAsync(" ");
        Assert.That(fake.Opened, Is.Empty);

        await vm.OpenSyllabusCommand.ExecuteAsync("https://example.com/x.pdf");
        Assert.That(fake.Opened, Is.EqualTo(new[] { "https://example.com/x.pdf" }));
    }

    private sealed class FakeLauncher : ILauncherService
    {
        public List<string> Opened { get; } = new();

        public Task<bool> OpenUrlAsync(string url)
        {
            Opened.Add(url);
            return Task.FromResult(true);
        }
    }
}
