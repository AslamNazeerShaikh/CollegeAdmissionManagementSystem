import 'package:flutter/foundation.dart';
import 'crm_models.dart';

extension _FirstOrNull<T> on Iterable<T> {
  T? get firstOrNull => isEmpty ? null : first;
}

// Port of CrmViewModel.cs. Same derived lists, same commands.
class CrmState extends ChangeNotifier {
  static const sections = ['Dashboard', 'Pipeline', 'Applications', 'Courses', 'Fees'];

  final List<Applicant> applicants = seedApplicants();
  final List<Course> courses = courseCatalog();
  final List<CourseFill> fills = seedFills();
  final List<CrmAction> actionQueue = seedActions();

  String selectedSection = 'Pipeline';
  String searchText = '';
  Applicant? selectedApplicant;
  int _enquirySeq = 1;

  CrmState() {
    selectedApplicant = applicants.firstOrNull;
  }

  bool show(String s) => selectedSection == s;

  List<Applicant> get filtered {
    final q = searchText.trim().toLowerCase();
    if (q.isEmpty) return applicants;
    return applicants
        .where((a) =>
            a.fullName.toLowerCase().contains(q) ||
            a.course.toLowerCase().contains(q) ||
            a.phone.contains(q))
        .toList();
  }

  List<Applicant> get feeDueList =>
      applicants.where((a) => !a.feePaid && a.stage.index >= CrmStage.offered.index).toList();

  List<Applicant> lane(CrmStage s) => applicants.where((a) => a.stage == s).toList();

  int get total => applicants.length;
  int get enrolledCount => applicants.where((a) => a.stage == CrmStage.enrolled).length;
  double get feeDueTotal => applicants.where((a) => !a.feePaid).fold(0, (p, a) => p + a.feeDue);
  String get feeDueLabel => '\u20B9${feeDueTotal.toStringAsFixed(0)}';
  int get docsPendingCount =>
      applicants.where((a) => a.docsPending && a.stage != CrmStage.enrolled).length;

  void selectSection(String s) {
    selectedSection = s;
    notifyListeners();
  }

  void setSearch(String v) {
    searchText = v;
    notifyListeners();
  }

  void selectApplicant(Applicant a) {
    selectedApplicant = a;
    notifyListeners();
  }

  void _replace(Applicant oldItem, Applicant next) {
    final i = applicants.indexOf(oldItem);
    if (i < 0) return;
    applicants[i] = next;
    if (selectedApplicant == oldItem) selectedApplicant = next;
    notifyListeners();
  }

  void advanceStage([Applicant? a]) {
    a ??= selectedApplicant;
    if (a == null || a.stage == CrmStage.enrolled) return;
    _replace(a, a.copyWith(stage: CrmStage.values[a.stage.index + 1], daysInStage: 0));
  }

  void collectFee([Applicant? a]) {
    a ??= selectedApplicant;
    if (a == null || a.feePaid) return;
    var next = a.copyWith(feePaid: true, feeDue: 0);
    if (next.stage == CrmStage.offered) next = next.copyWith(stage: CrmStage.feePaid);
    _replace(a, next);
  }

  void addEnquiry() {
    final n = _enquirySeq++;
    final item = Applicant(
      id: 'walkin-$n',
      fullName: 'Walk-in Enquiry $n',
      initials: 'WE',
      course: 'BCA - Computer Application',
      courseId: 'bca',
      phone: '98XXXXXXXX',
      meritPct: 0,
      stage: CrmStage.enquiry,
      docsOk: 0,
      docsTotal: 4,
      feePaid: false,
      feeDue: 17900,
      daysInStage: 0,
      counsellor: 'Desk',
      source: 'Walk-in',
    );
    applicants.insert(0, item);
    selectedApplicant = item;
    selectedSection = 'Applications';
    notifyListeners();
  }

  void addApplication([String? courseId]) {
    final course = courses.where((c) => c.id == courseId).firstOrNull;
    final n = _enquirySeq++;
    final item = Applicant(
      id: 'walkin-$n',
      fullName: 'Walk-in Application $n',
      initials: 'WA',
      course: course?.cardTitle.replaceAll('\n', ' ') ?? 'General enquiry',
      courseId: course?.id ?? 'general',
      phone: '98XXXXXXXX',
      meritPct: 0,
      stage: CrmStage.enquiry,
      docsOk: 0,
      docsTotal: 4,
      feePaid: false,
      feeDue: course == null ? 0 : 17900,
      daysInStage: 0,
      counsellor: 'Desk',
      source: 'Walk-in',
    );
    applicants.insert(0, item);
    selectedApplicant = item;
    selectedSection = 'Applications';
    notifyListeners();
  }
}
