import 'package:flutter/material.dart';
import '../crm_theme.dart';
import '../crm_models.dart';
import '../crm_state.dart';
import '../layout_breakpoints.dart';

// Dense register. Table on wide panes, stacked cards on narrow ones —
// the columns never squeeze and nothing ever scrolls horizontally.
class ApplicationsSection extends StatelessWidget {
  final CrmState state;
  const ApplicationsSection(this.state, {super.key});

  @override
  Widget build(BuildContext context) {
    final rows = state.filtered;
    return Padding(
      padding: const EdgeInsets.all(16),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Text('Applications \u2014 dense register',
              style: TextStyle(fontSize: 20, fontWeight: FontWeight.w700, color: CrmColors.ink)),
          const SizedBox(height: 12),
          Expanded(
            child: LayoutBuilder(
              builder: (context, constraints) => applicationsUseTable(
                      constraints.maxWidth)
                  ? _table(rows)
                  : _cards(rows),
            ),
          ),
        ],
      ),
    );
  }

  Widget _table(List<Applicant> rows) {
    return Column(
      children: [
        Container(
          padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 8),
          decoration: BoxDecoration(
            color: CrmColors.surface,
            borderRadius: BorderRadius.circular(8),
            border: Border.all(color: CrmColors.line),
          ),
          child: const Row(
            children: [
              Expanded(flex: 2, child: _H('NAME')),
              Expanded(flex: 2, child: _H('COURSE')),
              Expanded(child: _H('STAGE')),
              Expanded(child: _H('MERIT')),
              Expanded(child: _H('DOCS')),
              Expanded(child: _H('FEE')),
            ],
          ),
        ),
        const SizedBox(height: 8),
        if (rows.isEmpty)
          const Expanded(
            child: Center(
              child: Text('No applicants match your search',
                  style: TextStyle(fontSize: 13, color: CrmColors.muted)),
            ),
          )
        else
          Expanded(
            child: ListView.builder(
              itemCount: rows.length,
              itemBuilder: (_, i) {
                final a = rows[i];
                final selected = state.selectedApplicant == a;
                return GestureDetector(
                  onTap: () => state.selectApplicant(a),
                  child: Container(
                    margin: const EdgeInsets.only(bottom: 4),
                    padding: const EdgeInsets.symmetric(
                        horizontal: 12, vertical: 14),
                    decoration: BoxDecoration(
                      color: CrmColors.surface,
                      borderRadius: BorderRadius.circular(6),
                      border: Border.all(
                          color: selected ? CrmColors.accent : CrmColors.line,
                          width: selected ? 2 : 1),
                    ),
                    child: Row(
                      children: [
                        Expanded(
                            flex: 2,
                            child: Text(a.fullName,
                                overflow: TextOverflow.ellipsis,
                                style: const TextStyle(
                                    fontSize: 13,
                                    fontWeight: FontWeight.w600,
                                    color: CrmColors.ink))),
                        Expanded(
                            flex: 2,
                            child: Text(a.course,
                                overflow: TextOverflow.ellipsis,
                                style: const TextStyle(
                                    fontSize: 12, color: CrmColors.ink2))),
                        Expanded(
                            child: Text(a.stage.label,
                                overflow: TextOverflow.ellipsis,
                                style: const TextStyle(
                                    fontSize: 12, color: CrmColors.accent))),
                        Expanded(
                            child: Text(a.meritLabel,
                                overflow: TextOverflow.ellipsis,
                                style: const TextStyle(
                                    fontSize: 13, color: CrmColors.ink))),
                        Expanded(
                            child: Text(a.docsLabel,
                                overflow: TextOverflow.ellipsis,
                                style: const TextStyle(
                                    fontSize: 12, color: CrmColors.ink2))),
                        Expanded(
                            child: Text(a.feeLabel,
                                overflow: TextOverflow.ellipsis,
                                style: const TextStyle(
                                    fontSize: 12,
                                    fontWeight: FontWeight.w600,
                                    color: CrmColors.warn))),
                      ],
                    ),
                  ),
                );
              },
            ),
          ),
      ],
    );
  }

  Widget _cards(List<Applicant> rows) {
    if (rows.isEmpty) {
      return const Center(
        child: Text('No applicants match your search',
            style: TextStyle(fontSize: 13, color: CrmColors.muted)),
      );
    }
    return ListView.builder(
      itemCount: rows.length,
      itemBuilder: (_, i) {
        final a = rows[i];
        final selected = state.selectedApplicant == a;
        return GestureDetector(
          onTap: () => state.selectApplicant(a),
          child: Container(
            margin: const EdgeInsets.only(bottom: 8),
            padding: const EdgeInsets.all(12),
            decoration: BoxDecoration(
              color: CrmColors.surface,
              borderRadius: BorderRadius.circular(8),
              border: Border.all(
                  color: selected ? CrmColors.accent : CrmColors.line,
                  width: selected ? 2 : 1),
            ),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Row(
                  children: [
                    Expanded(
                      child: Text(a.fullName,
                          overflow: TextOverflow.ellipsis,
                          style: const TextStyle(
                              fontSize: 14,
                              fontWeight: FontWeight.w600,
                              color: CrmColors.ink)),
                    ),
                    const SizedBox(width: 8),
                    Text(a.meritLabel,
                        style: const TextStyle(
                            fontSize: 14,
                            fontWeight: FontWeight.w700,
                            color: CrmColors.accent)),
                  ],
                ),
                const SizedBox(height: 2),
                Text(a.course,
                    overflow: TextOverflow.ellipsis,
                    maxLines: 2,
                    style:
                        const TextStyle(fontSize: 12, color: CrmColors.muted)),
                const SizedBox(height: 6),
                Wrap(
                  spacing: 8,
                  runSpacing: 4,
                  children: [
                    _chip(a.stage.label),
                    _chip(a.docsLabel),
                    _chip(a.feeLabel),
                  ],
                ),
              ],
            ),
          ),
        );
      },
    );
  }

  Widget _chip(String text) {
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 9, vertical: 4),
      decoration: BoxDecoration(
        color: CrmColors.paperBg,
        borderRadius: BorderRadius.circular(999),
        border: Border.all(color: CrmColors.lineSoft),
      ),
      child: Text(text,
          style: const TextStyle(fontSize: 11, color: CrmColors.ink2)),
    );
  }
}

class _H extends StatelessWidget {
  final String text;
  const _H(this.text);
  @override
  Widget build(BuildContext context) {
    return Text(text, style: const TextStyle(fontSize: 10, color: CrmColors.muted));
  }
}
