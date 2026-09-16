// Port of CrmModels.cs + Course.cs + CrmSeed + CourseCatalog.
// Immutable data; stage moves = replace item in list.
enum CrmStage { enquiry, applied, verified, merit, offered, feePaid, enrolled }

extension CrmStageLabel on CrmStage {
  String get label => switch (this) {
        CrmStage.enquiry => 'Enquiry',
        CrmStage.applied => 'Applied',
        CrmStage.verified => 'Verified',
        CrmStage.merit => 'Merit',
        CrmStage.offered => 'Offered',
        CrmStage.feePaid => 'Fee paid',
        CrmStage.enrolled => 'Enrolled',
      };
}

class Applicant {
  final String id, fullName, initials, course, courseId, phone, counsellor, source;
  final double meritPct;
  final CrmStage stage;
  final int docsOk, docsTotal, daysInStage;
  final bool feePaid;
  final double feeDue;

  const Applicant({
    required this.id,
    required this.fullName,
    required this.initials,
    required this.course,
    required this.courseId,
    required this.phone,
    required this.meritPct,
    required this.stage,
    required this.docsOk,
    required this.docsTotal,
    required this.feePaid,
    required this.feeDue,
    required this.daysInStage,
    required this.counsellor,
    required this.source,
  });

  String get docsLabel => '$docsOk/$docsTotal docs';
  String get feeLabel => feePaid
      ? 'Paid'
      : feeDue > 0
          ? 'Due \u20B9${feeDue.toStringAsFixed(0)}'
          : '\u2014';
  String get meritLabel => '${meritPct.toStringAsFixed(1)}%';
  bool get docsPending => docsOk < docsTotal;

  Applicant copyWith({CrmStage? stage, int? daysInStage, bool? feePaid, double? feeDue}) =>
      Applicant(
        id: id,
        fullName: fullName,
        initials: initials,
        course: course,
        courseId: courseId,
        phone: phone,
        meritPct: meritPct,
        stage: stage ?? this.stage,
        docsOk: docsOk,
        docsTotal: docsTotal,
        feePaid: feePaid ?? this.feePaid,
        feeDue: feeDue ?? this.feeDue,
        daysInStage: daysInStage ?? this.daysInStage,
        counsellor: counsellor,
        source: source,
      );
}

class CrmAction {
  final String title, detail, kind;
  const CrmAction(this.title, this.detail, this.kind);
}

class CourseFill {
  final String courseId, name;
  final int filled, total;
  const CourseFill(this.courseId, this.name, this.filled, this.total);
  double get pct => total <= 0 ? 0 : filled / total * 100;
  String get pctLabel => '${pct.toStringAsFixed(0)}% full';
}

class Course {
  final String id, cardTitle, cardSubtitle, popupSubtitle;
  final String? fySyllabusUrl, sySyllabusUrl, tySyllabusUrl;
  const Course(this.id, this.cardTitle, this.cardSubtitle, this.popupSubtitle,
      [this.fySyllabusUrl, this.sySyllabusUrl, this.tySyllabusUrl]);
  bool get hasFy => fySyllabusUrl != null;
  bool get hasSy => sySyllabusUrl != null;
  bool get hasTy => tySyllabusUrl != null;
}

const _base = 'https://www.cocsit.org.in/download/syllabus';

List<Applicant> seedApplicants() => const [
      Applicant(id: 'a01', fullName: 'Sneha Deshmukh', initials: 'SD', course: 'BCA - Computer Application', courseId: 'bca', phone: '9822011456', meritPct: 91.2, stage: CrmStage.merit, docsOk: 3, docsTotal: 4, feePaid: false, feeDue: 17900, daysInStage: 2, counsellor: 'Patil', source: 'Website'),
      Applicant(id: 'a02', fullName: 'Rahul Shinde', initials: 'RS', course: 'B.Sc. Computer Science', courseId: 'bsccs', phone: '9764452301', meritPct: 84.6, stage: CrmStage.verified, docsOk: 2, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 4, counsellor: 'Jadhav', source: 'Referral'),
      Applicant(id: 'a03', fullName: 'Pooja Kulkarni', initials: 'PK', course: 'B.Sc. Data Science', courseId: 'bscds-x', phone: '9881123470', meritPct: 88.0, stage: CrmStage.offered, docsOk: 4, docsTotal: 4, feePaid: false, feeDue: 17900, daysInStage: 6, counsellor: 'Patil', source: 'Expo'),
      Applicant(id: 'a04', fullName: 'Amit Pawar', initials: 'AP', course: 'BBA - Business Admin', courseId: 'bba', phone: '9730012890', meritPct: 62.4, stage: CrmStage.applied, docsOk: 1, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 1, counsellor: 'Desk', source: 'Walk-in'),
      Applicant(id: 'a05', fullName: 'Kiran Jadhav', initials: 'KJ', course: 'M.Sc. Computer Science', courseId: 'msccs', phone: '9422377812', meritPct: 78.9, stage: CrmStage.feePaid, docsOk: 4, docsTotal: 4, feePaid: true, feeDue: 0, daysInStage: 1, counsellor: 'Jadhav', source: 'Website'),
      Applicant(id: 'a06', fullName: 'Divya Nair', initials: 'DN', course: 'B.Sc. AI & ML', courseId: 'bscaiml-x', phone: '9810034567', meritPct: 93.5, stage: CrmStage.merit, docsOk: 4, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 3, counsellor: 'Patil', source: 'Website'),
      Applicant(id: 'a07', fullName: 'Sagar More', initials: 'SM', course: 'B.Sc. Network Technology', courseId: 'bscnt', phone: '9657781234', meritPct: 58.2, stage: CrmStage.enquiry, docsOk: 0, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 0, counsellor: 'Desk', source: 'Phone'),
      Applicant(id: 'a08', fullName: 'Anjali Bhosale', initials: 'AB', course: 'B.Sc. Biotechnology', courseId: 'bscbt', phone: '9890123678', meritPct: 81.3, stage: CrmStage.verified, docsOk: 3, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 5, counsellor: 'Jadhav', source: 'School visit'),
      Applicant(id: 'a09', fullName: 'Vikram Reddy', initials: 'VR', course: 'MBA - Business Admin', courseId: 'mba', phone: '9705011223', meritPct: 70.0, stage: CrmStage.offered, docsOk: 4, docsTotal: 4, feePaid: false, feeDue: 29900, daysInStage: 9, counsellor: 'Patil', source: 'Referral'),
      Applicant(id: 'a10', fullName: 'Neha Joshi', initials: 'NJ', course: 'B.Sc. Software Engineering', courseId: 'bscse', phone: '9822456789', meritPct: 86.8, stage: CrmStage.applied, docsOk: 2, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 2, counsellor: 'Desk', source: 'Website'),
      Applicant(id: 'a11', fullName: 'Imran Shaikh', initials: 'IS', course: 'BCA - Computer Application', courseId: 'bca', phone: '9765012987', meritPct: 74.5, stage: CrmStage.enrolled, docsOk: 4, docsTotal: 4, feePaid: true, feeDue: 0, daysInStage: 0, counsellor: 'Jadhav', source: 'Walk-in'),
      Applicant(id: 'a12', fullName: 'Priya Patil', initials: 'PP', course: 'B.Sc. Information Technology', courseId: 'bscit-x', phone: '9881402563', meritPct: 89.4, stage: CrmStage.merit, docsOk: 3, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 2, counsellor: 'Patil', source: 'Expo'),
      Applicant(id: 'a13', fullName: 'Rohit Kale', initials: 'RK', course: 'B.Sc. Computer Management', courseId: 'bsccm-x', phone: '9730458123', meritPct: 66.1, stage: CrmStage.enquiry, docsOk: 0, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 1, counsellor: 'Desk', source: 'Phone'),
      Applicant(id: 'a14', fullName: 'Sana Sheikh', initials: 'SS', course: 'M.Sc. Biotechnology', courseId: 'mscbt', phone: '9422001456', meritPct: 79.7, stage: CrmStage.verified, docsOk: 4, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 3, counsellor: 'Jadhav', source: 'Website'),
      Applicant(id: 'a15', fullName: 'Akash Thorat', initials: 'AT', course: 'B.Sc. Software Development', courseId: 'bscsd-x', phone: '9657001892', meritPct: 72.8, stage: CrmStage.applied, docsOk: 1, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 4, counsellor: 'Desk', source: 'School visit'),
      Applicant(id: 'a16', fullName: 'Meera Iyer', initials: 'MI', course: 'B.Sc. Computer Science', courseId: 'bsccs', phone: '9811012233', meritPct: 95.0, stage: CrmStage.offered, docsOk: 4, docsTotal: 4, feePaid: false, feeDue: 17900, daysInStage: 1, counsellor: 'Patil', source: 'Website'),
      Applicant(id: 'a17', fullName: 'Nikhil Gaikwad', initials: 'NG', course: 'B.Sc. Network Technology', courseId: 'bscnt', phone: '9890334455', meritPct: 60.5, stage: CrmStage.feePaid, docsOk: 4, docsTotal: 4, feePaid: true, feeDue: 0, daysInStage: 2, counsellor: 'Desk', source: 'Referral'),
      Applicant(id: 'a18', fullName: 'Farah Khan', initials: 'FK', course: 'BBA - Business Admin', courseId: 'bba', phone: '9764009988', meritPct: 76.3, stage: CrmStage.enrolled, docsOk: 4, docsTotal: 4, feePaid: true, feeDue: 0, daysInStage: 0, counsellor: 'Jadhav', source: 'Walk-in'),
      Applicant(id: 'a19', fullName: 'Omkar Desai', initials: 'OD', course: 'BCA - Computer Application', courseId: 'bca', phone: '9730022455', meritPct: 69.9, stage: CrmStage.enquiry, docsOk: 1, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 0, counsellor: 'Desk', source: 'Phone'),
      Applicant(id: 'a20', fullName: 'Rutuja Mane', initials: 'RM', course: 'M.Sc. Computer Management', courseId: 'msccm', phone: '9422013678', meritPct: 82.6, stage: CrmStage.applied, docsOk: 2, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 3, counsellor: 'Patil', source: 'Website'),
      Applicant(id: 'a21', fullName: 'Aditya Nair', initials: 'AN', course: 'B.Sc. AI & ML', courseId: 'bscaiml-x', phone: '9881133901', meritPct: 90.1, stage: CrmStage.merit, docsOk: 4, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 1, counsellor: 'Jadhav', source: 'Expo'),
    ];

List<CourseFill> seedFills() => const [
      CourseFill('bca', 'BCA', 96, 120),
      CourseFill('bsccs', 'B.Sc. CS', 88, 120),
      CourseFill('bscse', 'B.Sc. SE', 61, 80),
      CourseFill('bscnt', 'B.Sc. NT', 44, 60),
      CourseFill('bba', 'BBA', 52, 60),
      CourseFill('msccs', 'M.Sc. CS', 30, 40),
    ];

List<CrmAction> seedActions() => const [
      CrmAction('Fee overdue \u2014 Vikram Reddy (MBA)', '\u20B929,900 \u00B7 9 days in Offered \u00B7 call today', 'Fee'),
      CrmAction('Fee overdue \u2014 Pooja Kulkarni (Data Science)', '\u20B917,900 \u00B7 6 days in Offered \u00B7 send reminder', 'Fee'),
      CrmAction('Docs missing \u2014 Rahul Shinde (B.Sc. CS)', '2/4 \u00B7 TC + caste certificate pending', 'Docs'),
      CrmAction('Docs missing \u2014 Sneha Deshmukh (BCA)', '3/4 \u00B7 photo ID pending \u00B7 merit 91.2%', 'Docs'),
      CrmAction('Merit list \u2014 4 applicants ready', 'Divya 93.5 \u00B7 Aditya 90.1 \u00B7 Priya 89.4 \u00B7 publish?', 'Merit'),
      CrmAction('New enquiries \u2014 3 unassigned', 'Sagar, Rohit, Omkar \u00B7 assign counsellor', 'New'),
    ];

List<Course> courseCatalog() => const [
      Course('bsccs', 'B.Sc. Computer Science', 'Eligibility: 12th Science \u00B7 3 Years', 'Fees/Year: \u20B917,900',
          '$_base/2024-25/05BScComputerScienceSingleMajorFirstYearNEPSyllabuswef202425.pdf', '$_base/2025-26/5BScComputerScienceSingalMajorSecondYearsyllabuswef202526.pdf', '$_base/2026-27/B.Sc_.-III-Year-Computer-Science-Single-Major-Syllabus-2026-27.pdf'),
      Course('bscse', 'B.Sc. Software Engineering', 'Eligibility: 12th Science \u00B7 3 Years', 'Fees/Year: \u20B917,900',
          '$_base/2024-25/07BScSoftwareEngineeringSingleMajorFirstYearNEPSyllabuswef202425.pdf', '$_base/2025-26/3BScSoftwareEngineeringSingalMajorSecondYearsyllabuswef202526.pdf', '$_base/2026-27/B.Sc_.-III-Year-Software-Engineering-Single-Major-Syllabus-2026-27.pdf'),
      Course('bscnt', 'B.Sc. Network Technology', 'Eligibility: 12th Science \u00B7 3 Years', 'Fees/Year: \u20B917,900',
          '$_base/2024-25/06BScComputerNetworkTechnologySingleMajorFirstYearNEPSyllabuswef202425.pdf', '$_base/2025-26/4BScComputerNetworkTechnologySingalMajorSecondYearsyllabuswef202526.pdf', '$_base/2026-27/B.Sc_.-III-Year-Network-Technology-Single-Major-Syllabus-2026-27.pdf'),
      Course('msccs', 'M.Sc. Computer Science', 'Eligibility: Any Computer UG \u00B7 2 Years', 'Fees/Year: \u20B929,900',
          '$_base/2023-24/12MScComputerScienceFirstyearAffiliatedCollege.pdf', '$_base/2024-25/13MScComputerScienceAffiliatedCollegesSecondYearSyllabuswef202425.pdf'),
      Course('mscse', 'M.Sc. Software Engineering', 'Eligibility: Any Computer UG \u00B7 2 Years', 'Fees/Year: \u20B929,900',
          '$_base/2023-24/MScFirstYearSoftwareEngineeringsyllabuswef202324.pdf', '$_base/2024-25/14MScSoftwareEngineeringSecondYearSyllabuswef202425.pdf'),
      Course('bca', 'BCA - Computer Application', 'Eligibility: Any 12th \u00B7 3 Years', 'Fees/Year: \u20B917,900',
          '$_base/2024-25/2.SRTMUN-BCA-Final%2023-10-2024QP%20(2).pdf', '$_base/2025-26/07BScBCASingalMajorSecondYearsyllabuswef202526.pdf', '$_base/2026-27/B.C.A.-III-Year-Single-Major-Syllabus-2026-27.pdf'),
      Course('bscbt', 'B.Sc. Biotechnology', 'Eligibility: 12th Science \u00B7 3 Years', 'Fees/Year: \u20B917,900',
          '$_base/2025-26/B.sc%20Biotechnology%20FirstYear%20NEP%20Syllabus%202024-25.pdf', '$_base/2025-26/BSc%20Biotechnology%20Second%20Year%20Syllabus%20202526.pdf', '$_base/2026-27/B.Sc_.-III-Year-Biotechnology-Syllabus-2026-27.pdf'),
      Course('mscbt', 'M.Sc. Biotechnology', 'Eligibility: Any UG \u00B7 2 Years', 'Fees/Year: \u20B917,900',
          '$_base/2025-26/MScBiotechnologyFirstyear.pdf', '$_base/2025-26/MSc_Biotechnology%20Second_Year_Syllabus.pdf'),
      Course('bba', 'BBA - Business Administration', 'Eligibility: Any 12th \u00B7 3 Years', 'Fees/Year: \u20B917,900',
          '$_base/2024-25/BBA%20FY%20NEP%20Syllbus.pdf', null, '$_base/2026-27/B.B.A.-TY-NEP-Syllabus-Affiliated-College.pdf'),
      Course('mba', 'MBA - Business Administration', 'Eligibility: Any UG \u00B7 2 Years', 'Fees/Year: \u20B917,900'),
    ];
