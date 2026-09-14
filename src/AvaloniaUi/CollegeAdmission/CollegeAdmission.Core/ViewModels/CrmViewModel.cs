using CollegeAdmission.Models;
using CollegeAdmission.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CollegeAdmission.ViewModels;

public partial class CrmViewModel : ViewModelBase
{
    private readonly ILauncherService launcher;
    private int enquirySeq = 1;

    public CrmViewModel(ILauncherService launcher)
    {
        this.launcher = launcher;
        Applicants = new(CrmSeed.Applicants);
        SelectedApplicant = Applicants.FirstOrDefault();
        Courses = CourseCatalog.All;
        Fills = CrmSeed.Fills.ToList();
        ActionQueue = CrmSeed.Actions.ToList();
    }

    // Sections (TabControl-free: bools drive IsVisible, no converter needed)
    public string[] Sections { get; } = ["Dashboard", "Pipeline", "Applications", "Courses", "Fees"];

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ShowDashboard))]
    [NotifyPropertyChangedFor(nameof(ShowPipeline))]
    [NotifyPropertyChangedFor(nameof(ShowApplications))]
    [NotifyPropertyChangedFor(nameof(ShowCourses))]
    [NotifyPropertyChangedFor(nameof(ShowFees))]
    private string selectedSection = "Pipeline";

    public bool ShowDashboard => SelectedSection == "Dashboard";
    public bool ShowPipeline => SelectedSection == "Pipeline";
    public bool ShowApplications => SelectedSection == "Applications";
    public bool ShowCourses => SelectedSection == "Courses";
    public bool ShowFees => SelectedSection == "Fees";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FilteredApplicants))]
    private string searchText = "";

    public System.Collections.ObjectModel.ObservableCollection<Applicant> Applicants { get; }

    [ObservableProperty]
    private Applicant? selectedApplicant;

    public IReadOnlyList<Course> Courses { get; }
    public IReadOnlyList<CourseFill> Fills { get; }
    public IReadOnlyList<CrmAction> ActionQueue { get; }

    public IEnumerable<Applicant> FilteredApplicants =>
        string.IsNullOrWhiteSpace(SearchText) ? Applicants :
        Applicants.Where(a =>
            a.FullName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
            a.Course.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
            a.Phone.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

    public IEnumerable<Applicant> FeeDueList => Applicants.Where(a => !a.FeePaid && a.Stage >= CrmStage.Offered);
    public int FeeDueCount => FeeDueList.Count();

    // Lanes
    public IEnumerable<Applicant> LaneEnquiry => Applicants.Where(a => a.Stage == CrmStage.Enquiry);
    public IEnumerable<Applicant> LaneApplied => Applicants.Where(a => a.Stage == CrmStage.Applied);
    public IEnumerable<Applicant> LaneVerified => Applicants.Where(a => a.Stage == CrmStage.Verified);
    public IEnumerable<Applicant> LaneMerit => Applicants.Where(a => a.Stage == CrmStage.Merit);
    public IEnumerable<Applicant> LaneOffered => Applicants.Where(a => a.Stage == CrmStage.Offered);
    public IEnumerable<Applicant> LaneFeePaid => Applicants.Where(a => a.Stage == CrmStage.FeePaid);
    public IEnumerable<Applicant> LaneEnrolled => Applicants.Where(a => a.Stage == CrmStage.Enrolled);

    // Stats
    public int TotalApplicants => Applicants.Count;
    public int EnrolledCount => Applicants.Count(a => a.Stage == CrmStage.Enrolled);
    public decimal FeeDueTotal => Applicants.Where(a => !a.FeePaid).Sum(a => a.FeeDue);
    public string FeeDueLabel => $"₹{FeeDueTotal:N0}";
    public int DocsPendingCount => Applicants.Count(a => a.DocsPending && a.Stage != CrmStage.Enrolled);
    public int CountEnquiry => LaneEnquiry.Count();
    public int CountApplied => LaneApplied.Count();
    public int CountVerified => LaneVerified.Count();
    public int CountMerit => LaneMerit.Count();
    public int CountOffered => LaneOffered.Count();
    public int CountFeePaid => LaneFeePaid.Count();
    public int CountEnrolled => LaneEnrolled.Count();

    private void RefreshDerived()
    {
        OnPropertyChanged(nameof(FilteredApplicants));
        OnPropertyChanged(nameof(FeeDueList));
        OnPropertyChanged(nameof(FeeDueCount));
        OnPropertyChanged(nameof(LaneEnquiry));
        OnPropertyChanged(nameof(LaneApplied));
        OnPropertyChanged(nameof(LaneVerified));
        OnPropertyChanged(nameof(LaneMerit));
        OnPropertyChanged(nameof(LaneOffered));
        OnPropertyChanged(nameof(LaneFeePaid));
        OnPropertyChanged(nameof(LaneEnrolled));
        OnPropertyChanged(nameof(TotalApplicants));
        OnPropertyChanged(nameof(EnrolledCount));
        OnPropertyChanged(nameof(FeeDueTotal));
        OnPropertyChanged(nameof(FeeDueLabel));
        OnPropertyChanged(nameof(DocsPendingCount));
        OnPropertyChanged(nameof(CountEnquiry));
        OnPropertyChanged(nameof(CountApplied));
        OnPropertyChanged(nameof(CountVerified));
        OnPropertyChanged(nameof(CountMerit));
        OnPropertyChanged(nameof(CountOffered));
        OnPropertyChanged(nameof(CountFeePaid));
        OnPropertyChanged(nameof(CountEnrolled));
    }

    private void Replace(Applicant oldItem, Applicant next)
    {
        var i = Applicants.IndexOf(oldItem);
        if (i < 0) return;
        Applicants[i] = next;
        if (SelectedApplicant == oldItem) SelectedApplicant = next;
        RefreshDerived();
    }

    [RelayCommand]
    private void SelectSection(string section) => SelectedSection = section;

    [RelayCommand]
    private void SelectApplicant(Applicant? applicant)
    {
        if (applicant is not null) SelectedApplicant = applicant;
    }

    [RelayCommand]
    private void AdvanceStage(Applicant? applicant)
    {
        applicant ??= SelectedApplicant;
        if (applicant is null || applicant.Stage == CrmStage.Enrolled) return;
        Replace(applicant, applicant with { Stage = applicant.Stage + 1, DaysInStage = 0 });
    }

    [RelayCommand]
    private void CollectFee(Applicant? applicant)
    {
        applicant ??= SelectedApplicant;
        if (applicant is null || applicant.FeePaid) return;
        var next = applicant with { FeePaid = true, FeeDue = 0 };
        if (next.Stage == CrmStage.Offered) next = next with { Stage = CrmStage.FeePaid };
        Replace(applicant, next);
    }

    [RelayCommand]
    private void AddEnquiry()
    {
        var n = enquirySeq++;
        var item = new Applicant($"walkin-{DateTime.Now:HHmmss}-{n}", $"Walk-in Enquiry {n}", "WE",
            "BCA - Computer Application", "bca", "98XXXXXXXX", 0,
            CrmStage.Enquiry, 0, 4, false, 17900, 0, "Desk", "Walk-in");
        Applicants.Insert(0, item);
        SelectedApplicant = item;
        SelectedSection = "Applications";
        RefreshDerived();
    }

    [RelayCommand]
    private Task OpenSyllabusAsync(string? url)
    {
        if (string.IsNullOrWhiteSpace(url)) return Task.CompletedTask;
        return launcher.OpenUrlAsync(url);
    }
}

internal static class CrmSeed
{
    internal static readonly Applicant[] Applicants =
    [
        new("a01", "Sneha Deshmukh", "SD", "BCA - Computer Application", "bca", "9822011456", 91.2, CrmStage.Merit, 3, 4, false, 17900, 2, "Patil", "Website"),
        new("a02", "Rahul Shinde", "RS", "B.Sc. Computer Science", "bsccs", "9764452301", 84.6, CrmStage.Verified, 2, 4, false, 0, 4, "Jadhav", "Referral"),
        new("a03", "Pooja Kulkarni", "PK", "B.Sc. Data Science", "bscds-x", "9881123470", 88.0, CrmStage.Offered, 4, 4, false, 17900, 6, "Patil", "Expo"),
        new("a04", "Amit Pawar", "AP", "BBA - Business Admin", "bba", "9730012890", 62.4, CrmStage.Applied, 1, 4, false, 0, 1, "Desk", "Walk-in"),
        new("a05", "Kiran Jadhav", "KJ", "M.Sc. Computer Science", "msccs", "9422377812", 78.9, CrmStage.FeePaid, 4, 4, true, 0, 1, "Jadhav", "Website"),
        new("a06", "Divya Nair", "DN", "B.Sc. AI & ML", "bscaiml-x", "9810034567", 93.5, CrmStage.Merit, 4, 4, false, 0, 3, "Patil", "Website"),
        new("a07", "Sagar More", "SM", "B.Sc. Network Technology", "bscnt", "9657781234", 58.2, CrmStage.Enquiry, 0, 4, false, 0, 0, "Desk", "Phone"),
        new("a08", "Anjali Bhosale", "AB", "B.Sc. Biotechnology", "bscbt", "9890123678", 81.3, CrmStage.Verified, 3, 4, false, 0, 5, "Jadhav", "School visit"),
        new("a09", "Vikram Reddy", "VR", "MBA - Business Admin", "mba", "9705011223", 70.0, CrmStage.Offered, 4, 4, false, 29900, 9, "Patil", "Referral"),
        new("a10", "Neha Joshi", "NJ", "B.Sc. Software Engineering", "bscse", "9822456789", 86.8, CrmStage.Applied, 2, 4, false, 0, 2, "Desk", "Website"),
        new("a11", "Imran Shaikh", "IS", "BCA - Computer Application", "bca", "9765012987", 74.5, CrmStage.Enrolled, 4, 4, true, 0, 0, "Jadhav", "Walk-in"),
        new("a12", "Priya Patil", "PP", "B.Sc. Information Technology", "bscit-x", "9881402563", 89.4, CrmStage.Merit, 3, 4, false, 0, 2, "Patil", "Expo"),
        new("a13", "Rohit Kale", "RK", "B.Sc. Computer Management", "bsccm-x", "9730458123", 66.1, CrmStage.Enquiry, 0, 4, false, 0, 1, "Desk", "Phone"),
        new("a14", "Sana Sheikh", "SS", "M.Sc. Biotechnology", "mscbt", "9422001456", 79.7, CrmStage.Verified, 4, 4, false, 0, 3, "Jadhav", "Website"),
        new("a15", "Akash Thorat", "AT", "B.Sc. Software Development", "bscsd-x", "9657001892", 72.8, CrmStage.Applied, 1, 4, false, 0, 4, "Desk", "School visit"),
        new("a16", "Meera Iyer", "MI", "B.Sc. Computer Science", "bsccs", "9811012233", 95.0, CrmStage.Offered, 4, 4, false, 17900, 1, "Patil", "Website"),
        new("a17", "Nikhil Gaikwad", "NG", "B.Sc. Network Technology", "bscnt", "9890334455", 60.5, CrmStage.FeePaid, 4, 4, true, 0, 2, "Desk", "Referral"),
        new("a18", "Farah Khan", "FK", "BBA - Business Admin", "bba", "9764009988", 76.3, CrmStage.Enrolled, 4, 4, true, 0, 0, "Jadhav", "Walk-in"),
        new("a19", "Omkar Desai", "OD", "BCA - Computer Application", "bca", "9730022455", 69.9, CrmStage.Enquiry, 1, 4, false, 0, 0, "Desk", "Phone"),
        new("a20", "Rutuja Mane", "RM", "M.Sc. Computer Management", "msccm", "9422013678", 82.6, CrmStage.Applied, 2, 4, false, 0, 3, "Patil", "Website"),
        new("a21", "Aditya Nair", "AN", "B.Sc. AI & ML", "bscaiml-x", "9881133901", 90.1, CrmStage.Merit, 4, 4, false, 0, 1, "Jadhav", "Expo"),
    ];

    internal static readonly CourseFill[] Fills =
    [
        new("bca", "BCA", 96, 120),
        new("bsccs", "B.Sc. CS", 88, 120),
        new("bscse", "B.Sc. SE", 61, 80),
        new("bscnt", "B.Sc. NT", 44, 60),
        new("bba", "BBA", 52, 60),
        new("msccs", "M.Sc. CS", 30, 40),
    ];

    internal static readonly CrmAction[] Actions =
    [
        new("Fee overdue — Vikram Reddy (MBA)", "₹29,900 · 9 days in Offered · call today", "Fee"),
        new("Fee overdue — Pooja Kulkarni (Data Science)", "₹17,900 · 6 days in Offered · send reminder", "Fee"),
        new("Docs missing — Rahul Shinde (B.Sc. CS)", "2/4 · TC + caste certificate pending", "Docs"),
        new("Docs missing — Sneha Deshmukh (BCA)", "3/4 · photo ID pending · merit 91.2%", "Docs"),
        new("Merit list — 4 applicants ready", "Divya 93.5 · Aditya 90.1 · Priya 89.4 · publish?", "Merit"),
        new("New enquiries — 3 unassigned", "Sagar, Rohit, Omkar · assign counsellor", "New"),
    ];
}
