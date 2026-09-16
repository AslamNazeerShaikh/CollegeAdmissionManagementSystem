import 'package:flutter/material.dart';
import 'crm_theme.dart';
import 'crm_models.dart';
import 'crm_state.dart';
import 'crm_button.dart';

// Right-hand applicant rail (340dp on desktop, stacked below on narrow).
class DetailRail extends StatelessWidget {
  final CrmState state;
  const DetailRail(this.state, {super.key});

  @override
  Widget build(BuildContext context) {
    final a = state.selectedApplicant;
    return Container(
      decoration: const BoxDecoration(
        color: CrmColors.surface,
        border: Border(left: BorderSide(color: CrmColors.line)),
      ),
      padding: const EdgeInsets.all(16),
      child: a == null
          ? const Text('Select an applicant',
              style: TextStyle(color: CrmColors.muted))
          : ListView(
              children: [
                const Text('APPLICANT',
                    style: TextStyle(fontSize: 10, color: CrmColors.muted)),
                const SizedBox(height: 10),
                Container(
                  width: 36,
                  height: 36,
                  alignment: Alignment.centerLeft,
                  child: Container(
                    width: 36,
                    height: 36,
                    alignment: Alignment.center,
                    decoration: const BoxDecoration(
                      color: CrmColors.accentSoft,
                      shape: BoxShape.circle,
                    ),
                    child: Text(a.initials,
                        style: const TextStyle(
                            fontSize: 14,
                            fontWeight: FontWeight.w700,
                            color: CrmColors.accent)),
                  ),
                ),
                const SizedBox(height: 10),
                Text(a.fullName,
                    style: const TextStyle(
                        fontSize: 18,
                        fontWeight: FontWeight.w700,
                        color: CrmColors.ink)),
                const SizedBox(height: 2),
                Text(a.course,
                    style: const TextStyle(fontSize: 13, color: CrmColors.ink2)),
                const SizedBox(height: 2),
                Text(a.phone,
                    style: const TextStyle(fontSize: 13, color: CrmColors.ink)),
                const SizedBox(height: 8),
                Row(
                  children: [
                    Expanded(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          const Text('MERIT',
                              style: TextStyle(fontSize: 10, color: CrmColors.muted)),
                          Text(a.meritLabel,
                              style: const TextStyle(
                                  fontSize: 26,
                                  fontWeight: FontWeight.w700,
                                  color: CrmColors.accent,
                                  fontFeatures: [
                                    FontFeature.tabularFigures()
                                  ])),
                        ],
                      ),
                    ),
                    Expanded(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          const Text('STAGE',
                              style: TextStyle(fontSize: 10, color: CrmColors.muted)),
                          Text(a.stage.label,
                              style: const TextStyle(
                                  fontSize: 14,
                                  fontWeight: FontWeight.w600,
                                  color: CrmColors.ink)),
                          Text(a.docsLabel,
                              style: const TextStyle(
                                  fontSize: 12, color: CrmColors.ink2)),
                        ],
                      ),
                    ),
                  ],
                ),
                const SizedBox(height: 8),
                Row(
                  children: [
                    Expanded(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          const Text('FEE',
                              style: TextStyle(fontSize: 10, color: CrmColors.muted)),
                          Text(a.feeLabel,
                              style: const TextStyle(
                                  fontSize: 14,
                                  fontWeight: FontWeight.w600,
                                  color: CrmColors.warn)),
                        ],
                      ),
                    ),
                    Expanded(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          const Text('COUNSELLOR',
                              style: TextStyle(fontSize: 10, color: CrmColors.muted)),
                          Text(a.counsellor,
                              style: const TextStyle(
                                  fontSize: 13, color: CrmColors.ink)),
                        ],
                      ),
                    ),
                  ],
                ),
                const SizedBox(height: 12),
                SizedBox(
                    width: double.infinity,
                    child: CrmButton('Advance stage \u2192',
                        onPressed: () => state.advanceStage())),
                const SizedBox(height: 8),
                SizedBox(
                    width: double.infinity,
                    child: CrmButton('Collect fee',
                        onPressed: () => state.collectFee(),
                        kind: CrmButtonKind.success)),
                const SizedBox(height: 8),
                SizedBox(
                    width: double.infinity,
                    child: CrmButton('New application',
                        onPressed: () => state.addEnquiry(),
                        kind: CrmButtonKind.outline)),
                const SizedBox(height: 8),
                const Divider(color: CrmColors.lineSoft),
                const Text('ACTIVITY',
                    style: TextStyle(fontSize: 10, color: CrmColors.muted)),
                const SizedBox(height: 4),
                const Text(
                    '\u00B7 Enquiry logged\n\u00B7 Docs uploaded for verification\n\u00B7 Merit auto-computed from marks',
                    style: TextStyle(
                        fontSize: 12, color: CrmColors.ink2, height: 1.6)),
              ],
            ),
    );
  }
}
