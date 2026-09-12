namespace CollegeAdmission.Models;

// ponytail: static in-code catalog, no DB. Add SQLite cache when offline search matters.
public sealed record Course(
    string Id,
    string Name,
    string Eligibility,
    string Duration,
    string FeePerYear,
    string? FySyllabusUrl = null,
    string? SySyllabusUrl = null,
    string? TySyllabusUrl = null)
{
    public bool HasFy => FySyllabusUrl is not null;
    public bool HasSy => SySyllabusUrl is not null;
    public bool HasTy => TySyllabusUrl is not null;
    public string Subtitle => $"{Eligibility} · {Duration} · ₹{FeePerYear}/yr";
}

public static class CourseCatalog
{
    private const string Base = "https://www.cocsit.org.in/download/syllabus";

    public static IReadOnlyList<Course> All { get; } = new List<Course>
    {
        new("bsccs", "B.Sc. Computer Science", "12th Science", "3/4 Years", "17,900",
            $"{Base}/2024-25/05BScComputerScienceSingleMajorFirstYearNEPSyllabuswef202425.pdf",
            $"{Base}/2025-26/5BScComputerScienceSingalMajorSecondYearsyllabuswef202526.pdf",
            $"{Base}/2026-27/B.Sc_.-III-Year-Computer-Science-Single-Major-Syllabus-2026-27.pdf"),
        new("bca", "BCA", "12th Any Faculty", "3/4 Years", "17,900",
            $"{Base}/2024-25/2.SRTMUN-BCA-Final%2023-10-2024QP%20(2).pdf",
            $"{Base}/2025-26/07BScBCASingalMajorSecondYearsyllabuswef202526.pdf",
            $"{Base}/2026-27/B.C.A.-III-Year-Single-Major-Syllabus-2026-27.pdf"),
        new("bsccse", "B.Sc. Software Engineering", "12th Science", "3/4 Years", "17,900",
            $"{Base}/2024-25/07BScSoftwareEngineeringSingleMajorFirstYearNEPSyllabuswef202425.pdf",
            $"{Base}/2025-26/3BScSoftwareEngineeringSingalMajorSecondYearsyllabuswef202526.pdf",
            $"{Base}/2026-27/B.Sc_.-III-Year-Software-Engineering-Single-Major-Syllabus-2026-27.pdf"),
        new("bscsd", "B.Sc. Software Development", "12th Any Faculty", "3/4 Years", "17,900",
            $"{Base}/2024-25/BScSoftwareDevelopmentSingleMajorFirstYearNEPSyllabuswef202425.pdf",
            $"{Base}/2025-26/12BScSoftwareDevelopmentSingalMajorSecondYearsyllabuswef202526.pdf",
            $"{Base}/2026-27/B.Sc_.-III-Year-Software-Development-Single-Major-Syllabus-2026-27.pdf"),
        new("bscds", "B.Sc. Data Science", "12th Science", "3/4 Years", "17,900",
            $"{Base}/2024-25/BScDataScienceSingleMajorFirstYearNEPSyllabuswef202425.pdf",
            $"{Base}/2025-26/13BScDataScienceSingalMajorSecondYearsyllabuswef202526.pdf",
            $"{Base}/2026-27/B.Sc_.-III-Year-Data-Science-Single-Major-Syllabus-2026-27.pdf"),
        new("bscaiml", "B.Sc. AI & ML", "12th Science", "3/4 Years", "17,900",
            $"{Base}/2025-26/1BScFirstYearArtificialIntelligenceandMachineLearning13092025Major.pdf",
            $"{Base}/2025-26/6BScArtificialIntelligencenMachineLearningSingalMajorSecondYearsyllabuswef202526.pdf",
            $"{Base}/2026-27/B.Sc_.-III-Year-Artificial-Intelligence-and-Machine-Learning-AI-ML-Single-Major-Syllabus-2026-27.pdf"),
        new("bscnt", "B.Sc. Network Technology", "12th Any Faculty", "3/4 Years", "17,900",
            $"{Base}/2024-25/06BScComputerNetworkTechnologySingleMajorFirstYearNEPSyllabuswef202425.pdf",
            $"{Base}/2025-26/4BScComputerNetworkTechnologySingalMajorSecondYearsyllabuswef202526.pdf",
            $"{Base}/2026-27/B.Sc_.-III-Year-Network-Technology-Single-Major-Syllabus-2026-27.pdf"),
        new("bscit", "B.Sc. Information Technology", "12th Any Faculty", "3/4 Years", "17,900",
            $"{Base}/2024-25/08BScInformationTechnologySingleMajorFirstYearNEPSyllabuswef202425.pdf",
            $"{Base}/2025-26/2BScInformationTechnologySingalMajorSecondYearsyllabuswef202526.pdf",
            $"{Base}/2026-27/B.Sc_.-III-Year-Information-Technology-Single-Major-Syllabus-2026-27.pdf"),
        new("bsccm", "B.Sc. Computer Management", "12th Any Faculty", "3/4 Years", "17,900",
            $"{Base}/2024-25/09BScComputerManagementSingleMajorFirstYearNEPSyllabuswef202425.pdf",
            $"{Base}/2025-26/1BScComputerManagementSingalMajorSecondYearsyllabuswef202526.pdf",
            $"{Base}/2026-27/B.Sc_.-III-Year-Computer-Management-Single-Major-Syllabus-2026-26.pdf"),
        new("bvoc", "B.Voc. (PSSD)", "12th Any Faculty", "3 Years", "17,900",
            $"{Base}/2021-22/BVocProgrammingSkillsforSoftwareDevelopmentFirstSecondThirdYear202223to20242025.pdf"),
        new("bscbt", "B.Sc. Biotechnology", "12th Science", "3/4 Years", "17,900",
            $"{Base}/2025-26/B.sc%20Biotechnology%20FirstYear%20NEP%20Syllabus%202024-25.pdf",
            $"{Base}/2025-26/BSc%20Biotechnology%20Second%20Year%20Syllabus%20202526.pdf",
            $"{Base}/2026-27/B.Sc_.-III-Year-Biotechnology-Syllabus-2026-27.pdf"),
        new("bba", "BBA", "12th Any Faculty", "3 Years", "17,900",
            $"{Base}/2024-25/BBA%20FY%20NEP%20Syllbus.pdf",
            null,
            $"{Base}/2026-27/B.B.A.-TY-NEP-Syllabus-Affiliated-College.pdf"),
        new("msccs", "M.Sc. Computer Science", "Any Computer UG", "2 Years", "29,900",
            $"{Base}/2023-24/12MScComputerScienceFirstyearAffiliatedCollege.pdf",
            $"{Base}/2024-25/13MScComputerScienceAffiliatedCollegesSecondYearSyllabuswef202425.pdf"),
        new("mscse", "M.Sc. Software Engineering", "Any Computer UG", "2 Years", "29,900",
            $"{Base}/2023-24/MScFirstYearSoftwareEngineeringsyllabuswef202324.pdf",
            $"{Base}/2024-25/14MScSoftwareEngineeringSecondYearSyllabuswef202425.pdf"),
        new("mscsan", "M.Sc. System Admin. & Networking", "Any Graduate", "2 Years", "29,900",
            $"{Base}/2023-24/10MScSystemAdministrationNetworkingFirstYearAffiliatedCollege.pdf",
            $"{Base}/2024-25/15MScSystemAdministrationandNetworkingAffiliatedCollegesSecondYearSyllabuswef202425.pdf"),
        new("msccm", "M.Sc. Computer Management", "Any Graduate", "2 Years", "29,900",
            $"{Base}/2023-24/11MScComputerManagementFirstyearAffiliatedCollege.pdf",
            $"{Base}/2024-25/12MScComputerManagementAffiliatedCollegesSecondYearSyllabuswef202425.pdf"),
        new("mscds", "M.Sc. Data Science", "Any Graduate", "2 Years", "29,900",
            $"{Base}/2025-26/2MScFirstYearDataScienceSyllabus13092025.pdf",
            $"{Base}/2025-26/MScSecondYearDataSciencesyllabuswef202526.pdf"),
        new("mscca", "M.Sc. Computer Application", "Any Graduate", "2 Years", "29,900",
            $"{Base}/2024-25/MScComputerApplicationAffiliatedCollegeFirstYearNEPSyllabuswef202425.pdf",
            $"{Base}/2024-25/PG_18_M.Sc.%20CA%20(Computer%20Application)%20SY%20NEP-2020_14_05_2025-Affilated%20College.pdf"),
        new("mscbt", "M.Sc. Biotechnology", "B.Sc. Graduate", "2 Years", "29,900",
            $"{Base}/2025-26/MScBiotechnologyFirstyear.pdf",
            $"{Base}/2025-26/MSc_Biotechnology%20Second_Year_Syllabus.pdf"),
        new("mca", "MCA", "Any Graduate + CET", "2 Years", "29,900",
            $"{Base}/2024-25/MCA%20I%20Sem.pdf",
            $"{Base}/2024-25/MCA%20II%20Sem.pdf",
            $"{Base}/2024-25/MCA%20III%20Sem.pdf"),
        new("mba", "MBA (YCMOU)", "Any Graduate", "2 Years", "17,900"),
    };
}
