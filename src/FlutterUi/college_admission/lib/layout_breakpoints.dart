// Landscape-first breakpoints for the desktop shell.
//
// Pure width math — no widgets, no platform checks — so unit tests can
// pin the layout contract without pumping anything.
//
// Contract:
// - triple (>=1100): nav + content + detail rail side by side. This is
//   the macOS default: MainFlutterWindow.swift enforces a 1100x700
//   minimum, so the macOS app always lands here, always landscape.
// - contentRail (>=760): content + rail, nav behind the hamburger.
// - single (<760): one pane, nav via drawer, rail as a bottom sheet.
// - applicationsUseTable: the register renders as a table only when the
//   content pane is wide enough; otherwise stacked cards. Either way
//   nothing ever scrolls horizontally.
enum ShellLayout { triple, contentRail, single }

ShellLayout shellLayoutForWidth(double width) {
  if (width >= 1100) return ShellLayout.triple;
  if (width >= 760) return ShellLayout.contentRail;
  return ShellLayout.single;
}

bool applicationsUseTable(double contentWidth) => contentWidth >= 640;
