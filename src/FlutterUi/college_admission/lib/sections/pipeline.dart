import 'package:flutter/material.dart';
import '../crm_theme.dart';
import '../crm_models.dart';
import '../crm_state.dart';

// Pipeline as stage tabs + applicant list. No horizontal scrolling:
// tabs wrap, the list scrolls vertically, detail lives in the rail.
class PipelineSection extends StatefulWidget {
  final CrmState state;
  const PipelineSection(this.state, {super.key});

  @override
  State<PipelineSection> createState() => _PipelineSectionState();
}

class _PipelineSectionState extends State<PipelineSection> {
  late CrmStage tab;

  @override
  void initState() {
    super.initState();
    tab = widget.state.selectedApplicant?.stage ?? CrmStage.enquiry;
  }

  @override
  Widget build(BuildContext context) {
    final state = widget.state;
    final items = state.lane(tab);
    return Padding(
      padding: const EdgeInsets.all(16),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Text('Admission pipeline \u2014 merit board',
              style: TextStyle(
                  fontSize: 20,
                  fontWeight: FontWeight.w700,
                  color: CrmColors.ink)),
          const SizedBox(height: 2),
          const Text('pick a stage \u00B7 tap a card \u2192 detail rail \u00B7 Advance moves stage',
              style: TextStyle(fontSize: 11, color: CrmColors.muted)),
          const SizedBox(height: 12),
          Wrap(
            spacing: 8,
            runSpacing: 8,
            children: CrmStage.values.map((s) {
              final active = s == tab;
              final n = state.lane(s).length;
              return Material(
                color: active ? CrmColors.accentSoft : CrmColors.surface,
                borderRadius: BorderRadius.circular(8),
                child: InkWell(
                  borderRadius: BorderRadius.circular(8),
                  onTap: () => setState(() => tab = s),
                  child: Container(
                    constraints: const BoxConstraints(minHeight: 40),
                    padding: const EdgeInsets.symmetric(
                        horizontal: 14, vertical: 8),
                    alignment: Alignment.center,
                    decoration: BoxDecoration(
                      border: Border.all(
                          color: active ? CrmColors.accent : CrmColors.line),
                      borderRadius: BorderRadius.circular(8),
                    ),
                    child: Text('${s.label} \u00B7 $n',
                        style: TextStyle(
                            fontSize: 13,
                            fontWeight: active
                                ? FontWeight.w600
                                : FontWeight.normal,
                            color: active
                                ? CrmColors.accent
                                : CrmColors.ink2)),
                  ),
                ),
              );
            }).toList(),
          ),
          const SizedBox(height: 12),
          Expanded(
            child: items.isEmpty
                ? Center(
                    child: Text('No applicants in ${tab.label}',
                        style: const TextStyle(
                            fontSize: 13, color: CrmColors.muted)),
                  )
                : ListView.builder(
                    itemCount: items.length,
                    itemBuilder: (_, i) => _row(state, items[i]),
                  ),
          ),
        ],
      ),
    );
  }

  Widget _row(CrmState state, Applicant a) {
    final selected = state.selectedApplicant == a;
    return GestureDetector(
      onTap: () => state.selectApplicant(a),
      child: Container(
        margin: const EdgeInsets.only(bottom: 4),
        padding:
            const EdgeInsets.symmetric(horizontal: 12, vertical: 12),
        decoration: BoxDecoration(
          color: CrmColors.surface,
          borderRadius: BorderRadius.circular(8),
          border: Border.all(
              color: selected ? CrmColors.accent : CrmColors.line,
              width: selected ? 2 : 1),
        ),
        child: Row(
          children: [
            Container(
              width: 36,
              height: 36,
              alignment: Alignment.center,
              decoration: const BoxDecoration(
                color: CrmColors.accentSoft,
                shape: BoxShape.circle,
              ),
              child: Text(a.initials,
                  style: const TextStyle(
                      fontSize: 13,
                      fontWeight: FontWeight.w700,
                      color: CrmColors.accent)),
            ),
            const SizedBox(width: 12),
            Expanded(
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(a.fullName,
                      style: const TextStyle(
                          fontSize: 14,
                          fontWeight: FontWeight.w600,
                          color: CrmColors.ink)),
                  const SizedBox(height: 2),
                  Text('${a.course} \u00B7 ${a.phone}',
                      overflow: TextOverflow.ellipsis,
                      maxLines: 2,
                      style: const TextStyle(
                          fontSize: 12, color: CrmColors.muted)),
                ],
              ),
            ),
            Column(
              crossAxisAlignment: CrossAxisAlignment.end,
              children: [
                Text(a.meritLabel,
                    style: const TextStyle(
                        fontSize: 14,
                        fontWeight: FontWeight.w700,
                        color: CrmColors.accent,
                        fontFeatures: [FontFeature.tabularFigures()])),
                const SizedBox(height: 2),
                Text('${a.docsLabel} \u00B7 ${a.feeLabel}',
                    style: const TextStyle(
                        fontSize: 11, color: CrmColors.ink2)),
              ],
            ),
            const SizedBox(width: 4),
            const Text('\u203A',
                style: TextStyle(fontSize: 20, color: CrmColors.muted)),
          ],
        ),
      ),
    );
  }
}
