using System;
using Avalonia;

namespace CollegeAdmission.Desktop;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            // ponytail: software rendering — GLX segfaults in Mesa gallium (Skia TexSubImage2D via Lottie). Revisit if Mesa/Skia fixed.
            .With(new X11PlatformOptions { RenderingMode = new[] { X11RenderingMode.Software } })
            .WithInterFont()
            .LogToTrace();
}
