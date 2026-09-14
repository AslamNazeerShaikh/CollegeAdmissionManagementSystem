namespace CollegeAdmission.Models;

// ponytail: static in-code catalog mirroring the legacy 12-card list. SQLite when search matters.
public sealed record Course(
    string Id,
    string CardTitle,
    string CardSubtitle,
    string PopupSubtitle,
    string? FySyllabusUrl = null,
    string? SySyllabusUrl = null,
    string? TySyllabusUrl = null)
{
    public bool HasFy => FySyllabusUrl is not null;
    public bool HasSy => SySyllabusUrl is not null;
    public bool HasTy => TySyllabusUrl is not null;
}

public static class CourseCatalog
{
    private const string Base = "https://www.cocsit.org.in/download/syllabus";
    private const string UgDetails = "Eligibility : 12th Science\nDuration : 3 Years\nSemesters : Total 6\nFees/Year : 17,900.0 Rs.";
    private const string UgAnyDetails = "Eligibility : Any 12th\nDuration : 3 Years\nSemesters : Total 6\nFees/Year : 17,900.0 Rs.";
    private const string PgDetails = "Eligibility : Any Computer UG\nDuration : 2 Years\nSemesters : Total 4\nFees/Year : 29,900.0 Rs.";
    private const string PgAnyDetails = "Eligibility : Any UG\nDuration : 2 Years\nSemesters : Total 4\nFees/Year : 29,900.0 Rs.";

    public static IReadOnlyList<Course> All { get; } = new List<Course>
    {
        new("bsccs", "B.Sc. Computer Science", "Eligibility : 12th Science\nDuration : 3 Years", UgDetails,
            $"{Base}/2024-25/05BScComputerScienceSingleMajorFirstYearNEPSyllabuswef202425.pdf",
            $"{Base}/2025-26/5BScComputerScienceSingalMajorSecondYearsyllabuswef202526.pdf",
            $"{Base}/2026-27/B.Sc_.-III-Year-Computer-Science-Single-Major-Syllabus-2026-27.pdf"),
        new("bscse", "B.Sc. Software Engineering", "Eligibility : 12th Science\nDuration : 3 Years", UgDetails,
            $"{Base}/2024-25/07BScSoftwareEngineeringSingleMajorFirstYearNEPSyllabuswef202425.pdf",
            $"{Base}/2025-26/3BScSoftwareEngineeringSingalMajorSecondYearsyllabuswef202526.pdf",
            $"{Base}/2026-27/B.Sc_.-III-Year-Software-Engineering-Single-Major-Syllabus-2026-27.pdf"),
        new("bscnt", "B.Sc. Network Technology", "Eligibility : 12th Science\nDuration : 3 Years", UgDetails,
            $"{Base}/2024-25/06BScComputerNetworkTechnologySingleMajorFirstYearNEPSyllabuswef202425.pdf",
            $"{Base}/2025-26/4BScComputerNetworkTechnologySingalMajorSecondYearsyllabuswef202526.pdf",
            $"{Base}/2026-27/B.Sc_.-III-Year-Network-Technology-Single-Major-Syllabus-2026-27.pdf"),
        new("msccs", "M.Sc. Computer Science", "Eligibility : Any Computer UG\nDuration : 2 Years", PgDetails,
            $"{Base}/2023-24/12MScComputerScienceFirstyearAffiliatedCollege.pdf",
            $"{Base}/2024-25/13MScComputerScienceAffiliatedCollegesSecondYearSyllabuswef202425.pdf"),
        new("mscse", "M.Sc. Software Engineering", "Eligibility : Any Computer UG\nDuration : 2 Years", PgDetails,
            $"{Base}/2023-24/MScFirstYearSoftwareEngineeringsyllabuswef202324.pdf",
            $"{Base}/2024-25/14MScSoftwareEngineeringSecondYearSyllabuswef202425.pdf"),
        new("mscsa", "M.Sc. System Admin. & N/W", "Eligibility : Any UG\nDuration : 2 Years", PgAnyDetails,
            $"{Base}/2023-24/10MScSystemAdministrationNetworkingFirstYearAffiliatedCollege.pdf",
            $"{Base}/2024-25/15MScSystemAdministrationandNetworkingAffiliatedCollegesSecondYearSyllabuswef202425.pdf"),
        new("msccm", "M.Sc. Computer Management", "Eligibility : Any UG\nDuration : 2 Years", PgAnyDetails,
            $"{Base}/2023-24/11MScComputerManagementFirstyearAffiliatedCollege.pdf",
            $"{Base}/2024-25/12MScComputerManagementAffiliatedCollegesSecondYearSyllabuswef202425.pdf"),
        new("bca", "BCA - Bachelor of\nComputer Application", "Eligibility : Any 12th\nDuration : 3 Years", UgAnyDetails,
            $"{Base}/2024-25/2.SRTMUN-BCA-Final%2023-10-2024QP%20(2).pdf",
            $"{Base}/2025-26/07BScBCASingalMajorSecondYearsyllabuswef202526.pdf",
            $"{Base}/2026-27/B.C.A.-III-Year-Single-Major-Syllabus-2026-27.pdf"),
        new("bscbt", "B.Sc. Biotechnology", "Eligibility : 12th Science\nDuration : 3 Years", UgDetails,
            $"{Base}/2025-26/B.sc%20Biotechnology%20FirstYear%20NEP%20Syllabus%202024-25.pdf",
            $"{Base}/2025-26/BSc%20Biotechnology%20Second%20Year%20Syllabus%20202526.pdf",
            $"{Base}/2026-27/B.Sc_.-III-Year-Biotechnology-Syllabus-2026-27.pdf"),
        new("mscbt", "M.Sc. Biotechnology", "Eligibility : Any UG\nDuration : 2 Years", PgAnyDetails,
            $"{Base}/2025-26/MScBiotechnologyFirstyear.pdf",
            $"{Base}/2025-26/MSc_Biotechnology%20Second_Year_Syllabus.pdf"),
        new("bba", "BBA - Bachelor of\nBusiness Administration", "Eligibility : Any 12th\nDuration : 3 Years", UgAnyDetails,
            $"{Base}/2024-25/BBA%20FY%20NEP%20Syllbus.pdf",
            null,
            $"{Base}/2026-27/B.B.A.-TY-NEP-Syllabus-Affiliated-College.pdf"),
        new("mba", "MBA - Master of\nBusiness Administration", "Eligibility : Any UG\nDuration : 2 Years",
            "Eligibility : Any UG\nDuration : 2 Years\nSemesters : Total 4\nFees/Year : 17,900.0 Rs."),
    };
}
