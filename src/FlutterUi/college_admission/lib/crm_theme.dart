import 'package:flutter/material.dart';

// CrmTokens.axaml, verbatim. Warm neutral surfaces; color is scarce:
// green CTA only, red/amber/blue status only.
abstract final class CrmColors {
  static const paperBg = Color(0xFFF7F6F3);
  static const surface = Color(0xFFFFFFFF);
  static const surface2 = Color(0xFFFBFBFA);
  static const ink = Color(0xFF171717);
  static const ink2 = Color(0xFF666666);
  static const muted = Color(0xFF8A8A8A);
  static const line = Color(0xFFDDDDD8);
  static const lineSoft = Color(0xFFE7E7E3);
  static const accent = Color(0xFF147A5A);
  static const accentSoft = Color(0xFFE7F5ED);
  static const accentInk = Color(0xFFFFFFFF);
  static const ok = Color(0xFF147A5A);
  static const okBg = Color(0xFFE7F5ED);
  static const warn = Color(0xFFB45309);
  static const warnBg = Color(0xFFFFF3E4);
  static const bad = Color(0xFFA94444);
  static const badBg = Color(0xFFFBECEC);
  static const info = Color(0xFF3D73D9);
  static const infoBg = Color(0xFFEDF3FF);
}

ThemeData crmTheme() {
  return ThemeData(
    useMaterial3: true,
    fontFamily: 'Inter',
    scaffoldBackgroundColor: CrmColors.paperBg,
    colorScheme: const ColorScheme.light(
      primary: CrmColors.accent,
      onPrimary: CrmColors.accentInk,
      surface: CrmColors.surface,
      onSurface: CrmColors.ink,
      error: CrmColors.bad,
    ),
    textTheme: const TextTheme(
      headlineSmall: TextStyle(fontSize: 26, fontWeight: FontWeight.w600, color: CrmColors.ink),
      titleLarge: TextStyle(fontSize: 20, fontWeight: FontWeight.w700, color: CrmColors.ink),
      titleMedium: TextStyle(fontSize: 18, fontWeight: FontWeight.w600, color: CrmColors.ink),
      bodyLarge: TextStyle(fontSize: 14, color: CrmColors.ink),
      bodyMedium: TextStyle(fontSize: 13, color: CrmColors.ink),
      bodySmall: TextStyle(fontSize: 12, color: CrmColors.ink2),
    ),
  );
}
