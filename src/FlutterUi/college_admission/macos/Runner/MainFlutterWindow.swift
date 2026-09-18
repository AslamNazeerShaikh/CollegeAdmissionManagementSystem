import Cocoa
import FlutterMacOS

class MainFlutterWindow: NSWindow {
  override func awakeFromNib() {
    let flutterViewController = FlutterViewController()
    let windowFrame = self.frame
    self.contentViewController = flutterViewController
    self.setFrame(windowFrame, display: true)

    // Landscape desktop shell: resizable, 1100x700 minimum keeps the
    // triple-pane layout (nav + content + rail) with zero horizontal
    // scrolling. Titlebar merges into the Flutter UI — the Dart
    // titlebar (crmTitlebar) draws in its place with traffic-light
    // clearance, so there is exactly one bar, not two.
    self.titleVisibility = .hidden
    self.titlebarAppearsTransparent = true
    self.styleMask.insert(.fullSizeContentView)
    self.isMovableByWindowBackground = true
    self.minSize = NSSize(width: 1100, height: 700)
    if self.frame.size.width < 1280 || self.frame.size.height < 800 {
      self.setContentSize(NSSize(width: 1280, height: 800))
    }
    self.center()

    RegisterGeneratedPlugins(registry: flutterViewController)

    super.awakeFromNib()
  }
}
