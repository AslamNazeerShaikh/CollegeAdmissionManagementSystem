using CollegeAdmission.Models;
using CollegeAdmission.Services;
using CollegeAdmission.ViewModels;

namespace CollegeAdmission.Tests;

public class CatalogTests
{
    [Test]
    public void Catalog_CoversAllLegacyCourses()
    {
        var titles = CourseCatalog.All.Select(c => c.CardTitle.Replace("\n", " ")).ToList();
        foreach (var expected in new[]
        {
            "B.Sc. Computer Science", "B.Sc. Software Engineering", "B.Sc. Network Technology",
            "M.Sc. Computer Science", "M.Sc. Software Engineering", "M.Sc. System Admin. & N/W",
            "M.Sc. Computer Management", "BCA - Bachelor of Computer Application",
            "B.Sc. Biotechnology", "M.Sc. Biotechnology",
            "BBA - Bachelor of Business Administration", "MBA - Master of Business Administration",
        })
            Assert.That(titles, Does.Contain(expected), $"missing {expected}");
    }

    [Test]
    public void Seed_CoversEveryStage()
    {
        var vm = new CrmViewModel(new FakeLauncher());
        Assert.That(vm.TotalApplicants, Is.EqualTo(21));
        Assert.Multiple(() =>
        {
            Assert.That(vm.CountEnquiry, Is.EqualTo(3));
            Assert.That(vm.CountApplied, Is.EqualTo(4));
            Assert.That(vm.CountVerified, Is.EqualTo(3));
            Assert.That(vm.CountMerit, Is.EqualTo(4));
            Assert.That(vm.CountOffered, Is.EqualTo(3));
            Assert.That(vm.CountFeePaid, Is.EqualTo(2));
            Assert.That(vm.CountEnrolled, Is.EqualTo(2));
        });
        vm.SelectStageCommand.Execute("Merit");
        Assert.That(vm.LaneForStage.Count(), Is.EqualTo(4));
    }

    [Test]
    public void AdvanceAndCollect_MoveApplicant()
    {
        var vm = new CrmViewModel(new FakeLauncher());
        var enquiry = vm.LaneEnquiry.First();
        vm.AdvanceStageCommand.Execute(enquiry);
        Assert.That(vm.LaneApplied, Does.Contain(enquiry with { Stage = CrmStage.Applied, DaysInStage = 0 }));

        var offered = vm.LaneOffered.First(a => !a.FeePaid);
        vm.CollectFeeCommand.Execute(offered);
        Assert.That(vm.LaneFeePaid.Any(a => a.Id == offered.Id && a.FeePaid), Is.True);
    }

    [Test]
    public async Task OpenSyllabus_SkipsEmpty_OpensValid()
    {
        var fake = new FakeLauncher();
        var vm = new CrmViewModel(fake);

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
