using Avalonia;
using Avalonia.Controls;

namespace CollegeAdmission.Views;

public partial class MainWindow : Window
{
    private const double Ratio = 9.0 / 16.0;
    private bool _lock;
    private double _lastW = 720, _lastH = 1280;

    public MainWindow()
    {
        InitializeComponent();
        Opened += (_, _) => FitToScreen();
        SizeChanged += (_, e) => EnforceRatio(e.NewSize);
    }

    // ponytail: client-side ratio coercion, Wayland compositors may still override maximized/fullscreen sizes
    private void FitToScreen()
    {
        var screen = Screens.ScreenFromVisual(this) ?? Screens.Primary;
        if (screen is null) return;
        var avail = screen.WorkingArea.Size.ToSize(screen.Scaling);
        var w = System.Math.Min(Width, avail.Width * 0.9);
        var h = w / Ratio;
        if (h > avail.Height * 0.9) { h = avail.Height * 0.9; w = h * Ratio; }
        _lock = true;
        Width = w; Height = h;
        _lastW = w; _lastH = h;
        _lock = false;
    }

    private void EnforceRatio(Size s)
    {
        if (_lock || WindowState != WindowState.Normal || s.Width <= 0 || s.Height <= 0) return;
        _lock = true;
        if (s.Width != _lastW) { Width = s.Width; Height = s.Width / Ratio; }
        else if (s.Height != _lastH) { Height = s.Height; Width = s.Height * Ratio; }
        _lastW = Width; _lastH = Height;
        _lock = false;
    }
}