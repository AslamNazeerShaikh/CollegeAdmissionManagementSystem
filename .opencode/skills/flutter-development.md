---
name: flutter-development
description: Flutter/Dart cross-platform practices for the CollegeAdmission CRM port (ChangeNotifier state, CrmColors tokens, desktop shell, Linux builds)
version: 1.0.0
author: opencode
tags:
  - flutter
  - dart
  - linux-desktop
  - changenotifier
  - crm
  - reference-ui
capabilities:
  - idiomatic-dart
  - changenotifier-state
  - design-token-theming
  - desktop-responsive-shell
  - widget-testing
  - linux-builds
references:
  - "https://docs.flutter.dev/"
  - "https://docs.flutter.dev/platform-integration/linux/building"
  - "https://github.com/flutter/flutter/tree/main/packages/flutter_tools"
examples:
  - name: "Immutable Models + Seed Data"
    description: "Plain data classes with copyWith; stage moves replace the item in the list"
    code: |
      // lib/crm_models.dart
      class Applicant {
        final String id, fullName, course;
        final CrmStage stage;
        final bool feePaid;
        final double feeDue;
        const Applicant({required this.id, required this.fullName, /* ... */});
        String get feeLabel => feePaid ? 'Paid' : feeDue > 0 ? 'Due \u20B9${feeDue.toStringAsFixed(0)}' : '\u2014';
        Applicant copyWith({CrmStage? stage, bool? feePaid, double? feeDue}) => Applicant(/* ... */);
      }

  - name: "ChangeNotifier Store (no packages)"
    description: "Sections, search, selection and commands; ListenableBuilder in the shell"
    code: |
      // lib/crm_state.dart
      class CrmState extends ChangeNotifier {
        String selectedSection = 'Pipeline';
        String searchText = '';
        Applicant? selectedApplicant;
        void selectSection(String s) { selectedSection = s; notifyListeners(); }
        void advanceStage([Applicant? a]) {
          a ??= selectedApplicant;
          if (a == null || a.stage == CrmStage.enrolled) return;
          _replace(a, a.copyWith(stage: CrmStage.values[a.stage.index + 1]));
        }
      }
      // lib/main.dart
      ListenableBuilder(listenable: state, builder: (_, __) => /* shell */);

  - name: "Design Tokens Verbatim"
    description: "CrmColors mirrors Avalonia CrmTokens.axaml; widgets never hard-code hex"
    code: |
      // lib/crm_theme.dart
      abstract final class CrmColors {
        static const paperBg = Color(0xFFF7F6F3);
        static const accent = Color(0xFF147A5A);
        // ...
      }

  - name: "Desktop Shell Breakpoint"
    description: "3-column shell on wide screens, drawer + stacked rail below 900px"
    code: |
      LayoutBuilder(builder: (context, c) {
        final narrow = c.maxWidth < 900;
        return Scaffold(
          drawer: narrow ? Drawer(child: _nav()) : null,
          body: narrow
              ? ListView(children: [SizedBox(height: 600, child: _section()), DetailRail(state)])
              : Row(children: [SizedBox(width: 300, child: _nav()), Expanded(child: _section()), SizedBox(width: 340, child: DetailRail(state))]),
        );
      });

  - name: "Widget Smoke Test"
    description: "Pin desktop surface size; wide text needs Ahem-font room"
    code: |
      // test/widget_test.dart
      tester.view.physicalSize = const Size(1440, 900);
      tester.view.devicePixelRatio = 1.0;
      addTearDown(tester.view.reset);
      await tester.pumpWidget(const CrmApp());
      expect(find.text('Admission pipeline \u2014 merit board'), findsOneWidget);

  - name: "Linux Release Build"
    description: "System deps once, then standard release build"
    code: |
      sudo dnf install -y clang cmake ninja-build gtk3-devel
      cd src/FlutterUi/college_admission
      flutter build linux --release
      # output: build/linux/x64/release/bundle/
