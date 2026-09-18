// ponytail: shell contract tests — landscape panes, merged titlebar,
// no horizontal scrollers in our own code (search-field internals
// excluded: EditableText scrolls horizontally by design).
import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';

import 'package:college_admission/main.dart';

Future<void> _boot(WidgetTester tester, Size size) async {
  tester.view.physicalSize = size;
  tester.view.devicePixelRatio = 1.0;
  addTearDown(tester.view.reset);
  await tester.pumpWidget(const CrmApp());
  await tester.pumpAndSettle();
}

bool _hasHorizontalOurs(WidgetTester tester) => find
    .byWidgetPredicate((w) =>
        (w is SingleChildScrollView &&
            w.scrollDirection == Axis.horizontal) ||
        (w is ListView && w.scrollDirection == Axis.horizontal))
    .evaluate()
    .isNotEmpty;

void main() {
  testWidgets('wide boots merged titlebar + triple landscape panes',
      (WidgetTester tester) async {
    await _boot(tester, const Size(1440, 900));
    expect(find.byKey(const Key('crmTitlebar')), findsOneWidget);
    // Nav, content, and rail all visible at once — landscape.
    expect(find.text('College Admissions'), findsOneWidget);
    expect(find.text('Admission pipeline \u2014 merit board'),
        findsOneWidget);
    expect(find.text('APPLICANT'), findsOneWidget);
    expect(find.byType(Drawer), findsNothing);
    expect(find.text('+ New enquiry'), findsOneWidget);
    expect(_hasHorizontalOurs(tester), isFalse);
  });

  testWidgets('medium window keeps content + rail, nav in drawer',
      (WidgetTester tester) async {
    await _boot(tester, const Size(900, 700));
    expect(find.text('Admission pipeline \u2014 merit board'),
        findsOneWidget);
    expect(find.text('APPLICANT'), findsOneWidget);
    expect(find.text('College Admissions'), findsNothing);
    await tester.tap(find.text('\u2630'));
    await tester.pumpAndSettle();
    expect(find.text('College Admissions'), findsOneWidget);
    expect(_hasHorizontalOurs(tester), isFalse);
  });

  testWidgets('compact window collapses creation into + menu',
      (WidgetTester tester) async {
    await _boot(tester, const Size(500, 800));
    expect(find.text('+ New enquiry'), findsNothing);
    expect(find.text('+'), findsOneWidget);
    expect(_hasHorizontalOurs(tester), isFalse);
  });

  testWidgets('applications uses cards on narrow, table on wide',
      (WidgetTester tester) async {
    await _boot(tester, const Size(500, 800));
    await tester.tap(find.text('\u2630'));
    await tester.pumpAndSettle();
    await tester.tap(find.text('Applications'));
    await tester.pumpAndSettle();
    expect(find.text('NAME'), findsNothing);
    expect(find.text('Sneha Deshmukh'), findsWidgets);

    await _boot(tester, const Size(1600, 900));
    await tester.tap(find.text('Applications'));
    await tester.pumpAndSettle();
    expect(find.text('NAME'), findsOneWidget);
    expect(_hasHorizontalOurs(tester), isFalse);
  });

  testWidgets('every section pumps clean at desktop size',
      (WidgetTester tester) async {
    await _boot(tester, const Size(1440, 900));
    for (final section in [
      'Dashboard',
      'Pipeline',
      'Applications',
      'Courses',
      'Fees'
    ]) {
      await tester.tap(find.text(section));
      await tester.pumpAndSettle();
      expect(tester.takeException(), isNull);
      expect(_hasHorizontalOurs(tester), isFalse);
    }
  });
}
