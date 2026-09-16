import 'package:flutter/material.dart';
import '../crm_theme.dart';
import '../crm_models.dart';
import '../crm_state.dart';

class DashboardSection extends StatelessWidget {
  final CrmState state;
  const DashboardSection(this.state, {super.key});

  @override
  Widget build(BuildContext context) {
    return ListView(
      padding: const EdgeInsets.all(16),
      children: [
        const Text('Dashboard', style: TextStyle(fontSize: 26, fontWeight: FontWeight.w600, color: CrmColors.ink)),
        const SizedBox(height: 2),
        Text('${state.total} applicants \u00B7 2026\u201327 cycle',
            style: const TextStyle(fontSize: 13, color: CrmColors.muted)),
        const SizedBox(height: 12),
        Wrap(
          spacing: 12,
          runSpacing: 12,
          children: [
            _stat('Total applicants', '${state.total}', '2026\u201327 cycle', CrmColors.ink),
            _stat('New enquiries', '${state.lane(CrmStage.enquiry).length}', 'need counsellor', CrmColors.info),
            _stat('Enrolled', '${state.enrolledCount}', 'admissions confirmed', CrmColors.ink),
            _stat('Fees due', state.feeDueLabel, '${state.feeDueList.length} applicants', CrmColors.ink),
            _stat('Docs pending', '${state.docsPendingCount}', 'awaiting verification', CrmColors.bad),
          ],
        ),
        const SizedBox(height: 16),
        const Text('Needs action today',
            style: TextStyle(fontSize: 18, fontWeight: FontWeight.w600, color: CrmColors.ink)),
        const SizedBox(height: 8),
        ...state.actionQueue.map(_actionCard),
        const SizedBox(height: 16),
        const Text('Intake funnel',
            style: TextStyle(fontSize: 14, fontWeight: FontWeight.w600, color: CrmColors.ink)),
        const SizedBox(height: 8),
        Container(
          padding: const EdgeInsets.all(12),
          decoration: BoxDecoration(
            color: CrmColors.surface,
            borderRadius: BorderRadius.circular(8),
            border: Border.all(color: CrmColors.line),
          ),
          child: Wrap(
            spacing: 16,
            runSpacing: 8,
            children: [
              _funnel('${state.lane(CrmStage.enquiry).length}', 'Enquiry', CrmColors.ink),
              _funnel('${state.lane(CrmStage.applied).length}', 'Applied', CrmColors.ink),
              _funnel('${state.lane(CrmStage.verified).length}', 'Verified', CrmColors.ink),
              _funnel('${state.lane(CrmStage.merit).length}', 'Merit', CrmColors.accent),
              _funnel('${state.lane(CrmStage.offered).length}', 'Offered', CrmColors.ink),
              _funnel('${state.lane(CrmStage.feePaid).length}', 'Fee paid', CrmColors.warn),
              _funnel('${state.lane(CrmStage.enrolled).length}', 'Enrolled', CrmColors.ok),
            ],
          ),
        ),
        const SizedBox(height: 16),
        const Text('Course fill %',
            style: TextStyle(fontSize: 14, fontWeight: FontWeight.w600, color: CrmColors.ink)),
        const SizedBox(height: 8),
        ...state.fills.map((f) => Container(
              margin: const EdgeInsets.only(bottom: 8),
              padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 10),
              decoration: BoxDecoration(
                color: CrmColors.surface,
                borderRadius: BorderRadius.circular(8),
                border: Border.all(color: CrmColors.line),
              ),
              child: Row(
                children: [
                  SizedBox(
                    width: 140,
                    child: Text(f.name,
                        style: const TextStyle(
                            fontSize: 13, fontWeight: FontWeight.w600, color: CrmColors.ink)),
                  ),
                  Expanded(
                    child: Padding(
                      padding: const EdgeInsets.symmetric(horizontal: 12),
                      child: LinearProgressIndicator(
                        value: f.total <= 0 ? 0 : f.filled / f.total,
                        minHeight: 8,
                        backgroundColor: CrmColors.lineSoft,
                        valueColor: const AlwaysStoppedAnimation(CrmColors.accent),
                        borderRadius: BorderRadius.circular(4),
                      ),
                    ),
                  ),
                  SizedBox(
                    width: 70,
                    child: Text(f.pctLabel,
                        textAlign: TextAlign.right,
                        style: const TextStyle(fontSize: 12, color: CrmColors.ink2)),
                  ),
                ],
              ),
            )),
      ],
    );
  }

  Widget _stat(String label, String value, String sub, Color valueColor) {
    return Container(
      width: 160,
      padding: const EdgeInsets.symmetric(horizontal: 18, vertical: 16),
      decoration: BoxDecoration(
        color: CrmColors.surface,
        borderRadius: BorderRadius.circular(12),
        border: Border.all(color: CrmColors.line),
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(label, style: const TextStyle(fontSize: 13, color: CrmColors.muted)),
          const SizedBox(height: 2),
          Text(value,
              style: TextStyle(
                  fontSize: 26,
                  fontWeight: FontWeight.w600,
                  color: valueColor,
                  fontFeatures: const [FontFeature.tabularFigures()])),
          const SizedBox(height: 2),
          Text(sub, style: const TextStyle(fontSize: 13, color: CrmColors.muted)),
        ],
      ),
    );
  }

  Widget _actionCard(CrmAction a) {
    return Container(
      margin: const EdgeInsets.only(bottom: 8),
      padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 10),
      decoration: BoxDecoration(
        color: CrmColors.surface,
        borderRadius: BorderRadius.circular(8),
        border: Border.all(color: CrmColors.line),
      ),
      child: Row(
        children: [
          Container(
            padding: const EdgeInsets.symmetric(horizontal: 9, vertical: 4),
            margin: const EdgeInsets.only(right: 12),
            decoration: BoxDecoration(
              color: CrmColors.warnBg,
              borderRadius: BorderRadius.circular(999),
            ),
            child: Text(a.kind,
                style: const TextStyle(
                    fontSize: 11, fontWeight: FontWeight.w600, color: CrmColors.warn)),
          ),
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(a.title,
                    style: const TextStyle(
                        fontSize: 13, fontWeight: FontWeight.w600, color: CrmColors.ink)),
                const SizedBox(height: 2),
                Text(a.detail,
                    style: const TextStyle(fontSize: 12, color: CrmColors.ink2)),
              ],
            ),
          ),
        ],
      ),
    );
  }

  Widget _funnel(String value, String label, Color color) {
    return SizedBox(
      width: 96,
      child: Column(
        children: [
          Text(value,
              style: TextStyle(
                  fontSize: 20,
                  fontWeight: FontWeight.w700,
                  color: color,
                  fontFeatures: const [FontFeature.tabularFigures()])),
          Text(label, style: const TextStyle(fontSize: 11, color: CrmColors.muted)),
        ],
      ),
    );
  }
}
