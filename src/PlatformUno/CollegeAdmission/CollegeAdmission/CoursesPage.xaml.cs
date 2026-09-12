using CollegeAdmission.ViewModels;

namespace CollegeAdmission;

public sealed partial class CoursesPage : Page
{
    public CoursesViewModel ViewModel { get; } = new();

    public CoursesPage()
    {
        this.InitializeComponent();
        DataContext = ViewModel;
    }

    private void OnBackClick(object sender, RoutedEventArgs e)
    {
        if (Frame.CanGoBack)
            Frame.GoBack();
    }

    private void OnRegisterClick(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(RegistrationPage));
}
