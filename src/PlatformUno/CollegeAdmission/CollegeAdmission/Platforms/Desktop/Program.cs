using Uno.UI.Hosting;

namespace CollegeAdmission;

internal class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        // XWayland reports an unreliable Xft.dpi (e.g. 192 on a scale-1
        // session), doubling the whole UI versus Flutter/GTK siblings.
        // Uno docs recommend UNO_DISPLAY_SCALE_OVERRIDE for X11-on-Wayland;
        // only default it when the user didn't set one explicitly.
        Environment.SetEnvironmentVariable(
            "UNO_DISPLAY_SCALE_OVERRIDE",
            Environment.GetEnvironmentVariable("UNO_DISPLAY_SCALE_OVERRIDE") ?? "1.0");
        App.InitializeLogging();

        var host = UnoPlatformHostBuilder.Create()
            .App(() => new App())
            .UseX11()
            .UseLinuxFrameBuffer()
            .UseMacOS()
            .UseWin32()
            .Build();

        host.Run();
    }
}
