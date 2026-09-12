using CollegeAdmission.Models;
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
}
