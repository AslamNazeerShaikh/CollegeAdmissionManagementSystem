// ponytail: one smoke test - shell boots onto the Pipeline board.
import 'dart:ui';

import 'package:flutter_test/flutter_test.dart';

import 'package:college_admission/main.dart';

void main() {
  testWidgets('CRM shell boots to pipeline board', (WidgetTester tester) async {
    tester.view.physicalSize = const Size(1440, 900);
    tester.view.devicePixelRatio = 1.0;
    addTearDown(tester.view.reset);
    await tester.pumpWidget(const CrmApp());
    expect(find.text('Admission pipeline \u2014 merit board'), findsOneWidget);
    expect(find.text('College Admissions'), findsOneWidget);
  });

  testWidgets('tapping a nav section switches content', (WidgetTester tester) async {
    tester.view.physicalSize = const Size(1440, 900);
    tester.view.devicePixelRatio = 1.0;
    addTearDown(tester.view.reset);
    await tester.pumpWidget(const CrmApp());
    // Regression: nav onTap used a context above MaterialApp (Navigator.of crash).
    await tester.tap(find.text('Dashboard'));
    await tester.pumpAndSettle();
    expect(find.text('Needs action today'), findsOneWidget);
  });
}
