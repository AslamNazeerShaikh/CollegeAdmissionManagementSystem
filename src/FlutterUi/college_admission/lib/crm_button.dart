import 'package:flutter/material.dart';
import 'crm_theme.dart';

// One button owns all call sites. Flat fills, 1px borders, radius 8,
// 40px height — ReferenceUi, not Material defaults (no elevation).
enum CrmButtonKind { primary, outline, success }

class CrmButton extends StatelessWidget {
  final String label;
  final VoidCallback? onPressed;
  final CrmButtonKind kind;
  final EdgeInsetsGeometry padding;

  const CrmButton(this.label,
      {super.key,
      required this.onPressed,
      this.kind = CrmButtonKind.primary,
      this.padding = const EdgeInsets.symmetric(horizontal: 14, vertical: 8)});

  @override
  Widget build(BuildContext context) {
    final enabled = onPressed != null;
    final bg = switch (kind) {
      CrmButtonKind.primary => CrmColors.accent,
      CrmButtonKind.success => CrmColors.ok,
      CrmButtonKind.outline => Colors.transparent,
    };
    final fg = switch (kind) {
      CrmButtonKind.primary => CrmColors.accentInk,
      CrmButtonKind.success => Colors.white,
      CrmButtonKind.outline => CrmColors.ink2,
    };
    return Opacity(
      opacity: enabled ? 1 : 0.5,
      child: Material(
        color: bg,
        borderRadius: BorderRadius.circular(8),
        child: InkWell(
          borderRadius: BorderRadius.circular(8),
          onTap: onPressed,
          child: Container(
            constraints: const BoxConstraints(minHeight: 40),
            padding: padding,
            alignment: Alignment.center,
            decoration: kind == CrmButtonKind.outline
                ? BoxDecoration(
                    border: Border.all(color: CrmColors.line),
                    borderRadius: BorderRadius.circular(8),
                  )
                : null,
            child: Text(label,
                style: TextStyle(
                    fontSize: 13, fontWeight: FontWeight.w600, color: fg)),
          ),
        ),
      ),
    );
  }
}
