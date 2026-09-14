using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CollegeAdmission.ViewModels;

public sealed partial class RegistrationViewModel : ObservableObject
{
    [Required] public string FirstName { get; set; } = "";
    public string MiddleName { get; set; } = "";
    [Required] public string LastName { get; set; } = "";
    public string Marks10 { get; set; } = "";
    public string Marks12 { get; set; } = "";
    public string MarksUG { get; set; } = "";
    public string PassYear10 { get; set; } = "";
    public string PassYear12 { get; set; } = "";
    public string PassYearUG { get; set; } = "";
    [Required] public string BirthDay { get; set; } = "";
    [Required] public string BirthMonth { get; set; } = "";
    [Required] public string BirthYear { get; set; } = "";
    public string BloodGroup { get; set; } = "";
    public string Nationality { get; set; } = "";
    public string Gender { get; set; } = "";
    [Required, Phone] public string PrimaryPhone { get; set; } = "";
    public string SecondaryPhone { get; set; } = "";
    [Required] public string CourseName { get; set; } = "";
    public string PermanentAddress { get; set; } = "";
    public string CurrentAddress { get; set; } = "";

    [ObservableProperty]
    private string statusMessage = "";

    public bool Validate(out List<ValidationResult> errors)
    {
        errors = new();
        var ctx = new ValidationContext(this);
        return Validator.TryValidateObject(this, ctx, errors, validateAllProperties: true);
    }

    private static string DraftPath(string name)
    {
        var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CollegeAdmission");
        Directory.CreateDirectory(dir);
        return Path.Combine(dir, name);
    }

    [RelayCommand]
    private async Task SaveDraftAsync()
    {
        var json = JsonSerializer.Serialize(this.ToData(), new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(DraftPath("draft.json"), json);
        StatusMessage = "Draft saved on this device.";
    }

    [RelayCommand]
    private async Task SubmitAsync()
    {
        if (!Validate(out var errors))
        {
            StatusMessage = string.Join("; ", errors.Select(e => e.ErrorMessage).Distinct());
            return;
        }
        var json = JsonSerializer.Serialize(this.ToData(), new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(DraftPath($"registration-{DateTime.Now:yyyyMMdd-HHmmss}.json"), json);
        StatusMessage = "Registered. Details saved on this device — backend sync pending (Phase 5).";
    }

    public Dictionary<string, string> ToData() => new()
    {
        ["firstName"] = FirstName, ["middleName"] = MiddleName, ["lastName"] = LastName,
        ["marks10"] = Marks10, ["marks12"] = Marks12, ["marksUG"] = MarksUG,
        ["passYear10"] = PassYear10, ["passYear12"] = PassYear12, ["passYearUG"] = PassYearUG,
        ["birthDay"] = BirthDay, ["birthMonth"] = BirthMonth, ["birthYear"] = BirthYear,
        ["bloodGroup"] = BloodGroup, ["nationality"] = Nationality, ["gender"] = Gender,
        ["primaryPhone"] = PrimaryPhone, ["secondaryPhone"] = SecondaryPhone,
        ["courseName"] = CourseName, ["permanentAddress"] = PermanentAddress, ["currentAddress"] = CurrentAddress,
    };
}
