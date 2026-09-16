import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart';
import '../crm_theme.dart';
import '../crm_button.dart';
import '../crm_state.dart';

class CoursesSection extends StatelessWidget {
  final CrmState state;
  const CoursesSection(this.state, {super.key});

  @override
  Widget build(BuildContext context) {
    return ListView(
      padding: const EdgeInsets.all(16),
      children: [
        const Text('Course catalog \u2014 seats, fees, syllabi',
            style: TextStyle(fontSize: 20, fontWeight: FontWeight.w700, color: CrmColors.ink)),
        const SizedBox(height: 12),
        ...state.courses.map((c) => Container(
              margin: const EdgeInsets.only(bottom: 8),
              padding: const EdgeInsets.all(12),
              decoration: BoxDecoration(
                color: CrmColors.surface,
                borderRadius: BorderRadius.circular(8),
                border: Border.all(color: CrmColors.line),
              ),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(c.cardTitle,
                      style: const TextStyle(
                          fontSize: 14, fontWeight: FontWeight.w700, color: CrmColors.ink)),
                  const SizedBox(height: 6),
                  Text(c.cardSubtitle,
                      style: const TextStyle(fontSize: 12, color: CrmColors.ink2)),
                  const SizedBox(height: 6),
                  Text(c.popupSubtitle,
                      style: const TextStyle(fontSize: 12, color: CrmColors.ink2)),
                  const SizedBox(height: 8),
                  Wrap(
                    spacing: 8,
                    runSpacing: 8,
                    children: [
                      _syllabusBtn(context, 'FY syllabus', c.fySyllabusUrl, c.hasFy),
                      _syllabusBtn(context, 'SY syllabus', c.sySyllabusUrl, c.hasSy),
                      _syllabusBtn(context, 'TY syllabus', c.tySyllabusUrl, c.hasTy),
                      CrmButton('Apply \u2192',
                          onPressed: () => state.addApplication(c.id)),
                    ],
                  ),
                ],
              ),
            )),
      ],
    );
  }

  Widget _syllabusBtn(BuildContext context, String label, String? url, bool enabled) {
    return CrmButton(
      label,
      onPressed: !enabled
          ? null
          : () async {
              final uri = Uri.parse(url!);
              if (!await launchUrl(uri, mode: LaunchMode.externalApplication)) {
                if (context.mounted) {
                  ScaffoldMessenger.of(context).showSnackBar(
                      SnackBar(content: Text('Could not open $label')));
                }
              }
            },
      kind: CrmButtonKind.outline,
      padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 6),
    );
  }
}
