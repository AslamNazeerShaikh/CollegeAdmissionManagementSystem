// ponytail: pure width math, no widget pumping needed.
import 'package:flutter_test/flutter_test.dart';

import 'package:college_admission/layout_breakpoints.dart';

void main() {
  test('macOS minimum window lands in triple landscape layout', () {
    // MainFlutterWindow.swift enforces 1100x700.
    expect(shellLayoutForWidth(1100), ShellLayout.triple);
    expect(shellLayoutForWidth(1280), ShellLayout.triple);
    expect(shellLayoutForWidth(2560), ShellLayout.triple);
  });

  test('resized windows degrade without horizontal scrolling', () {
    expect(shellLayoutForWidth(1099), ShellLayout.contentRail);
    expect(shellLayoutForWidth(760), ShellLayout.contentRail);
    expect(shellLayoutForWidth(759), ShellLayout.single);
    expect(shellLayoutForWidth(320), ShellLayout.single);
  });

  test('applications register switches table to cards', () {
    expect(applicationsUseTable(640), isTrue);
    expect(applicationsUseTable(639), isFalse);
    // Triple-pane content width at the 1100 minimum is ~530 after
    // 248 nav + 320 rail, so the register uses cards there too.
    expect(applicationsUseTable(1100 - 248 - 320), isFalse);
  });
}
