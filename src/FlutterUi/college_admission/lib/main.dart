import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'crm_theme.dart';
import 'crm_state.dart';
import 'crm_button.dart';
import 'layout_breakpoints.dart';
import 'sections/dashboard.dart';
import 'sections/pipeline.dart';
import 'sections/applications.dart';
import 'sections/courses.dart';
import 'sections/fees.dart';
import 'detail_rail.dart';

void main() => runApp(const CrmApp());

class CrmApp extends StatefulWidget {
  const CrmApp({super.key});
  @override
  State<CrmApp> createState() => _CrmAppState();
}

class _CrmAppState extends State<CrmApp> {
  final state = CrmState();
  final searchCtrl = TextEditingController();

  @override
  void dispose() {
    searchCtrl.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'College Admissions CRM',
      theme: crmTheme(),
      debugShowCheckedModeBanner: false,
      home: ListenableBuilder(
        listenable: state,
        builder: (context, _) => LayoutBuilder(
          builder: (context, constraints) {
            final layout =
                shellLayoutForWidth(constraints.maxWidth);
            return Scaffold(
              // Merged macOS titlebar: the native title is hidden +
              // transparent (see MainFlutterWindow.swift), so this bar
              // IS the titlebar. No Scaffold.appBar anywhere — one bar
              // total, zero horizontal scrolling inside it.
              body: Column(
                children: [
                  _titleBar(layout != ShellLayout.triple),
                  Expanded(child: _body(layout)),
                ],
              ),
              drawer: layout == ShellLayout.triple
                  ? null
                  : Drawer(
                      child: Builder(
                          builder: (drawerCtx) => _nav(drawerCtx))),
            );
          },
        ),
      ),
    );
  }

  Widget _body(ShellLayout layout) {
    return switch (layout) {
      // Landscape default: three panes, each scrolls vertically on
      // its own. Content flexes; side panes are fixed + narrow.
      ShellLayout.triple => Row(
          children: [
            SizedBox(width: 248, child: _nav()),
            Expanded(child: _section()),
            SizedBox(width: 320, child: DetailRail(state)),
          ],
        ),
      // Resized narrower: nav docks into the drawer, content + rail
      // share the row. Still landscape, still no horizontal scroll.
      ShellLayout.contentRail => Row(
          children: [
            Expanded(child: _section()),
            SizedBox(width: 320, child: DetailRail(state)),
          ],
        ),
      // Compact (phone/portrait): stack content over rail, both
      // full-width, page scrolls vertically.
      ShellLayout.single => ListView(
          children: [
            SizedBox(height: 600, child: _section()),
            SizedBox(height: 560, child: DetailRail(state)),
          ],
        ),
    };
  }

  // Titlebar carries traffic-light clearance on macOS, search, and
  // creation only. Stats live on the Dashboard cards — duplicating
  // them here forced a horizontal stats strip. Below 760dp the two
  // creation buttons collapse into one "+" menu so the bar itself
  // never scrolls or overflows horizontally.
  Widget _titleBar(bool showMenu) {
    // ponytail: no window_manager dep for drag-to-move; traffic lights
    // + transparency come from MainFlutterWindow.swift, drag stays
    // native-only until someone asks for it.
    final isMac = defaultTargetPlatform == TargetPlatform.macOS;
    return Container(
      key: const Key('crmTitlebar'),
      height: 52,
      decoration: const BoxDecoration(
        color: CrmColors.surface,
        border: Border(bottom: BorderSide(color: CrmColors.line)),
      ),
      padding: EdgeInsets.only(
          left: isMac ? 78 : 8, right: 12, top: 6, bottom: 6),
      child: LayoutBuilder(
        builder: (context, constraints) {
          final roomy = constraints.maxWidth >= 760;
          return Row(
            children: [
              if (showMenu)
                Builder(
                  builder: (ctx) => IconButton(
                    icon: const Text('\u2630',
                        style:
                            TextStyle(fontSize: 20, color: CrmColors.ink)),
                    onPressed: () => Scaffold.of(ctx).openDrawer(),
                  ),
                ),
              Expanded(
                child: Container(
                  constraints: const BoxConstraints(maxWidth: 420),
                  margin: const EdgeInsets.only(right: 12),
                  child: _searchField(),
                ),
              ),
              if (roomy)
                const Flexible(
                  child: Text(
                    'College Admissions \u00B7 2026\u201327',
                    overflow: TextOverflow.ellipsis,
                    style: TextStyle(
                        fontSize: 12, color: CrmColors.muted),
                  ),
                ),
              if (roomy) const SizedBox(width: 12),
              if (roomy) ...[
                CrmButton('+ New enquiry', onPressed: state.addEnquiry),
                const SizedBox(width: 8),
                CrmButton('+ New application',
                    onPressed: () => state.addApplication(),
                    kind: CrmButtonKind.outline),
              ] else
                PopupMenuButton<String>(
                  icon: const Text('+',
                      style: TextStyle(
                          fontSize: 20,
                          fontWeight: FontWeight.w600,
                          color: CrmColors.accent)),
                  onSelected: (v) {
                    if (v == 'enquiry') {
                      state.addEnquiry();
                    } else {
                      state.addApplication();
                    }
                  },
                  itemBuilder: (_) => const [
                    PopupMenuItem(
                        value: 'enquiry', child: Text('New enquiry')),
                    PopupMenuItem(
                        value: 'application',
                        child: Text('New application')),
                  ],
                ),
            ],
          );
        },
      ),
    );
  }

  Widget _searchField() {
    return TextField(
      controller: searchCtrl,
      onChanged: state.setSearch,
      style: const TextStyle(fontSize: 13),
      decoration: InputDecoration(
        hintText: 'Search name, course, phone\u2026',
        hintStyle: const TextStyle(fontSize: 13, color: CrmColors.muted),
        isDense: true,
        filled: true,
        fillColor: CrmColors.surface,
        enabledBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(8),
          borderSide: const BorderSide(color: CrmColors.line),
        ),
        focusedBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(8),
          borderSide: const BorderSide(color: CrmColors.accent, width: 1.5),
        ),
        contentPadding:
            const EdgeInsets.symmetric(horizontal: 12, vertical: 12),
      ),
    );
  }

  // drawerCtx is only passed for the drawer copy so tapping a section
  // closes it; the docked copy passes nothing. Never use the State's
  // own context for Navigator — it sits above MaterialApp.
  Widget _nav([BuildContext? drawerCtx]) {
    return _NavBody(
      state: state,
      onSelect: (s) {
        state.selectSection(s);
        if (drawerCtx != null && Navigator.of(drawerCtx).canPop()) {
          Navigator.of(drawerCtx).pop();
        }
      },
    );
  }

  Widget _section() {
    final child = switch (state.selectedSection) {
      'Dashboard' => DashboardSection(state),
      'Pipeline' => PipelineSection(state),
      'Applications' => ApplicationsSection(state),
      'Courses' => CoursesSection(state),
      'Fees' => FeesSection(state),
      _ => PipelineSection(state),
    };
    // ReferenceUi motion: 150-220ms ease-out, opacity only.
    return AnimatedSwitcher(
      duration: const Duration(milliseconds: 180),
      switchInCurve: Curves.easeOut,
      switchOutCurve: Curves.easeOut,
      child: KeyedSubtree(
          key: ValueKey(state.selectedSection), child: child),
    );
  }
}

// Static docked nav (triple layout) needs no context at all.
class _NavBody extends StatelessWidget {
  final CrmState state;
  final ValueChanged<String> onSelect;
  const _NavBody({required this.state, required this.onSelect});

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: const BoxDecoration(
        color: CrmColors.surface,
        border: Border(right: BorderSide(color: CrmColors.line)),
      ),
      padding: const EdgeInsets.all(16),
      child: ListView(
        children: [
          const Row(
            children: [
              _Logo(),
              SizedBox(width: 10),
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text('College Admissions',
                        overflow: TextOverflow.ellipsis,
                        style: TextStyle(
                            fontSize: 15,
                            fontWeight: FontWeight.w600,
                            color: CrmColors.ink)),
                    Text('CRM',
                        style: TextStyle(fontSize: 12, color: CrmColors.muted)),
                  ],
                ),
              ),
            ],
          ),
          const SizedBox(height: 2),
          const Text('2026\u201327 admission cycle',
              style: TextStyle(fontSize: 11, color: CrmColors.muted)),
          const SizedBox(height: 12),
          ...CrmState.sections.map((s) {
            final active = state.show(s);
            return Container(
              margin: const EdgeInsets.only(bottom: 2),
              child: Material(
                color: active ? CrmColors.paperBg : Colors.transparent,
                borderRadius: BorderRadius.circular(8),
                child: InkWell(
                  borderRadius: BorderRadius.circular(8),
                  onTap: () => onSelect(s),
                  child: Padding(
                    padding: const EdgeInsets.symmetric(
                        horizontal: 10, vertical: 10),
                    child: Text(s,
                        style: TextStyle(
                            fontSize: 14,
                            fontWeight: active
                                ? FontWeight.w600
                                : FontWeight.normal,
                            color: CrmColors.ink)),
                  ),
                ),
              ),
            );
          }),
          const Divider(color: CrmColors.lineSoft, height: 25),
          const Padding(
            padding: EdgeInsets.only(left: 10, bottom: 4),
            child: Text('MANAGEMENT',
                style: TextStyle(fontSize: 11, color: CrmColors.muted)),
          ),
          Container(
            padding: const EdgeInsets.all(10),
            decoration: BoxDecoration(
              color: CrmColors.paperBg,
              borderRadius: BorderRadius.circular(8),
            ),
            child: const Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text('Counsellor desk \u00B7 Latur',
                    style: TextStyle(
                        fontSize: 12,
                        fontWeight: FontWeight.w600,
                        color: CrmColors.ink)),
                SizedBox(height: 2),
                Text('click card \u2192 rail \u00B7 Advance moves stage',
                    style: TextStyle(fontSize: 11, color: CrmColors.muted)),
              ],
            ),
          ),
        ],
      ),
    );
  }
}

class _Logo extends StatelessWidget {
  const _Logo();
  @override
  Widget build(BuildContext context) {
    return Container(
      width: 36,
      height: 36,
      alignment: Alignment.center,
      decoration: BoxDecoration(
        color: CrmColors.accent,
        borderRadius: BorderRadius.circular(10),
      ),
      child: const Text('\u2726',
          style: TextStyle(fontSize: 18, color: Colors.white)),
    );
  }
}
