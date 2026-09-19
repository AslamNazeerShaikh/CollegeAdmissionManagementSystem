namespace CollegeAdmission.Models;

// ponytail: immutable records; stage moves = replace item in collection.
public enum CrmStage
{
    Enquiry,
    Applied,
    Verified,
    Merit,
    Offered,
    FeePaid,
    Enrolled
}

public sealed record Applicant(
    string Id,
    string FullName,
    string Initials,
    string Course,
    string CourseId,
    string Phone,
    double MeritPct,
    CrmStage Stage,
    int DocsOk,
    int DocsTotal,
    bool FeePaid,
    decimal FeeDue,
    int DaysInStage,
    string Counsellor,
    string Source)
{
    public string DocsLabel => $"{DocsOk}/{DocsTotal} docs";
    public string FeeLabel => FeePaid ? "Paid" : FeeDue > 0 ? $"Due ₹{FeeDue:N0}" : "—";
    public string StageName => Stage switch
    {
        CrmStage.Enquiry => "Enquiry",
        CrmStage.Applied => "Applied",
        CrmStage.Verified => "Verified",
        CrmStage.Merit => "Merit",
        CrmStage.Offered => "Offered",
        CrmStage.FeePaid => "Fee paid",
        CrmStage.Enrolled => "Enrolled",
        _ => Stage.ToString()
    };
    public bool DocsPending => DocsOk < DocsTotal;
}

public sealed record CrmAction(string Title, string Detail, string Kind);

public sealed record CourseFill(string CourseId, string Name, int Filled, int Total)
{
    public double Pct => Total <= 0 ? 0 : (double)Filled / Total * 100;
    public string PctLabel => $"{Pct:N0}% full";
}
