import 'package:flutter/material.dart';
import '../crm_theme.dart';
import '../crm_button.dart';
import '../crm_state.dart';

class FeesSection extends StatelessWidget {
  final CrmState state;
  const FeesSection(this.state, {super.key});

  @override
  Widget build(BuildContext context) {
    final due = state.feeDueList;
    return Padding(
      padding: const EdgeInsets.all(16),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Row(
            children: [
              const Expanded(
                child: Text('Fees \u2014 collection queue',
                    style: TextStyle(
                        fontSize: 20, fontWeight: FontWeight.w700, color: CrmColors.ink)),
              ),
              const Text('Outstanding: ',
                  style: TextStyle(fontSize: 12, color: CrmColors.muted)),
              Text(state.feeDueLabel,
                  style: const TextStyle(
                      fontSize: 18, fontWeight: FontWeight.w700, color: CrmColors.warn)),
            ],
          ),
          const SizedBox(height: 12),
          Expanded(
            child: ListView.builder(
              itemCount: due.length,
              itemBuilder: (_, i) {
                final a = due[i];
                return Container(
                  margin: const EdgeInsets.only(bottom: 8),
                  padding: const EdgeInsets.all(12),
                  decoration: BoxDecoration(
                    color: CrmColors.surface,
                    borderRadius: BorderRadius.circular(8),
                    border: Border.all(color: CrmColors.line),
                  ),
                  child: Row(
                    children: [
                      Expanded(
                        child: Column(
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            Text(a.fullName,
                                style: const TextStyle(
                                    fontSize: 13,
                                    fontWeight: FontWeight.w600,
                                    color: CrmColors.ink)),
                            const SizedBox(height: 2),
                            Text(a.course,
                                overflow: TextOverflow.ellipsis,
                                maxLines: 2,
                                style: const TextStyle(
                                    fontSize: 12, color: CrmColors.muted)),
                          ],
                        ),
                      ),
                      Text(a.feeLabel,
                          style: const TextStyle(
                              fontSize: 14,
                              fontWeight: FontWeight.w700,
                              color: CrmColors.warn)),
                      const SizedBox(width: 12),
                      CrmButton('Collect',
                          onPressed: () => state.collectFee(a),
                          kind: CrmButtonKind.success),
                    ],
                  ),
                );
              },
            ),
          ),
        ],
      ),
    );
  }
}
