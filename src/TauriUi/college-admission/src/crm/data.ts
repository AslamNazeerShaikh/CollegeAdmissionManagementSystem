// Port of CrmModels.cs + Course.cs + CrmSeed + CourseCatalog.
// Immutable data; stage moves replace the item in the array.
export type CrmStage =
  | "Enquiry"
  | "Applied"
  | "Verified"
  | "Merit"
  | "Offered"
  | "FeePaid"
  | "Enrolled";

export const STAGES: CrmStage[] = [
  "Enquiry",
  "Applied",
  "Verified",
  "Merit",
  "Offered",
  "FeePaid",
  "Enrolled",
];

export const STAGE_LABEL: Record<CrmStage, string> = {
  Enquiry: "Enquiry",
  Applied: "Applied",
  Verified: "Verified",
  Merit: "Merit",
  Offered: "Offered",
  FeePaid: "Fee paid",
  Enrolled: "Enrolled",
};

export interface Applicant {
  id: string;
  fullName: string;
  initials: string;
  course: string;
  courseId: string;
  phone: string;
  meritPct: number;
  stage: CrmStage;
  docsOk: number;
  docsTotal: number;
  feePaid: boolean;
  feeDue: number;
  daysInStage: number;
  counsellor: string;
  source: string;
}

export interface CrmAction {
  title: string;
  detail: string;
  kind: string;
}

export interface CourseFill {
  courseId: string;
  name: string;
  filled: number;
  total: number;
}

export interface Course {
  id: string;
  cardTitle: string;
  cardSubtitle: string;
  popupSubtitle: string;
  fySyllabusUrl?: string;
  sySyllabusUrl?: string;
  tySyllabusUrl?: string;
}

export const docsLabel = (a: Applicant) => `${a.docsOk}/${a.docsTotal} docs`;
export const feeLabel = (a: Applicant) =>
  a.feePaid ? "Paid" : a.feeDue > 0 ? `Due \u20B9${a.feeDue.toLocaleString("en-IN")}` : "\u2014";
export const meritLabel = (a: Applicant) => `${a.meritPct.toFixed(1)}%`;
export const feeDueLabel = (list: Applicant[]) =>
  `\u20B9${list.reduce((s, a) => s + (a.feePaid ? 0 : a.feeDue), 0).toLocaleString("en-IN")}`;

export const SEED_APPLICANTS: Applicant[] = [
  { id: "a01", fullName: "Sneha Deshmukh", initials: "SD", course: "BCA - Computer Application", courseId: "bca", phone: "9822011456", meritPct: 91.2, stage: "Merit", docsOk: 3, docsTotal: 4, feePaid: false, feeDue: 17900, daysInStage: 2, counsellor: "Patil", source: "Website" },
  { id: "a02", fullName: "Rahul Shinde", initials: "RS", course: "B.Sc. Computer Science", courseId: "bsccs", phone: "9764452301", meritPct: 84.6, stage: "Verified", docsOk: 2, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 4, counsellor: "Jadhav", source: "Referral" },
  { id: "a03", fullName: "Pooja Kulkarni", initials: "PK", course: "B.Sc. Data Science", courseId: "bscds-x", phone: "9881123470", meritPct: 88.0, stage: "Offered", docsOk: 4, docsTotal: 4, feePaid: false, feeDue: 17900, daysInStage: 6, counsellor: "Patil", source: "Expo" },
  { id: "a04", fullName: "Amit Pawar", initials: "AP", course: "BBA - Business Admin", courseId: "bba", phone: "9730012890", meritPct: 62.4, stage: "Applied", docsOk: 1, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 1, counsellor: "Desk", source: "Walk-in" },
  { id: "a05", fullName: "Kiran Jadhav", initials: "KJ", course: "M.Sc. Computer Science", courseId: "msccs", phone: "9422377812", meritPct: 78.9, stage: "FeePaid", docsOk: 4, docsTotal: 4, feePaid: true, feeDue: 0, daysInStage: 1, counsellor: "Jadhav", source: "Website" },
  { id: "a06", fullName: "Divya Nair", initials: "DN", course: "B.Sc. AI & ML", courseId: "bscaiml-x", phone: "9810034567", meritPct: 93.5, stage: "Merit", docsOk: 4, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 3, counsellor: "Patil", source: "Website" },
  { id: "a07", fullName: "Sagar More", initials: "SM", course: "B.Sc. Network Technology", courseId: "bscnt", phone: "9657781234", meritPct: 58.2, stage: "Enquiry", docsOk: 0, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 0, counsellor: "Desk", source: "Phone" },
  { id: "a08", fullName: "Anjali Bhosale", initials: "AB", course: "B.Sc. Biotechnology", courseId: "bscbt", phone: "9890123678", meritPct: 81.3, stage: "Verified", docsOk: 3, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 5, counsellor: "Jadhav", source: "School visit" },
  { id: "a09", fullName: "Vikram Reddy", initials: "VR", course: "MBA - Business Admin", courseId: "mba", phone: "9705011223", meritPct: 70.0, stage: "Offered", docsOk: 4, docsTotal: 4, feePaid: false, feeDue: 29900, daysInStage: 9, counsellor: "Patil", source: "Referral" },
  { id: "a10", fullName: "Neha Joshi", initials: "NJ", course: "B.Sc. Software Engineering", courseId: "bscse", phone: "9822456789", meritPct: 86.8, stage: "Applied", docsOk: 2, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 2, counsellor: "Desk", source: "Website" },
  { id: "a11", fullName: "Imran Shaikh", initials: "IS", course: "BCA - Computer Application", courseId: "bca", phone: "9765012987", meritPct: 74.5, stage: "Enrolled", docsOk: 4, docsTotal: 4, feePaid: true, feeDue: 0, daysInStage: 0, counsellor: "Jadhav", source: "Walk-in" },
  { id: "a12", fullName: "Priya Patil", initials: "PP", course: "B.Sc. Information Technology", courseId: "bscit-x", phone: "9881402563", meritPct: 89.4, stage: "Merit", docsOk: 3, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 2, counsellor: "Patil", source: "Expo" },
  { id: "a13", fullName: "Rohit Kale", initials: "RK", course: "B.Sc. Computer Management", courseId: "bsccm-x", phone: "9730458123", meritPct: 66.1, stage: "Enquiry", docsOk: 0, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 1, counsellor: "Desk", source: "Phone" },
  { id: "a14", fullName: "Sana Sheikh", initials: "SS", course: "M.Sc. Biotechnology", courseId: "mscbt", phone: "9422001456", meritPct: 79.7, stage: "Verified", docsOk: 4, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 3, counsellor: "Jadhav", source: "Website" },
  { id: "a15", fullName: "Akash Thorat", initials: "AT", course: "B.Sc. Software Development", courseId: "bscsd-x", phone: "9657001892", meritPct: 72.8, stage: "Applied", docsOk: 1, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 4, counsellor: "Desk", source: "School visit" },
  { id: "a16", fullName: "Meera Iyer", initials: "MI", course: "B.Sc. Computer Science", courseId: "bsccs", phone: "9811012233", meritPct: 95.0, stage: "Offered", docsOk: 4, docsTotal: 4, feePaid: false, feeDue: 17900, daysInStage: 1, counsellor: "Patil", source: "Website" },
  { id: "a17", fullName: "Nikhil Gaikwad", initials: "NG", course: "B.Sc. Network Technology", courseId: "bscnt", phone: "9890334455", meritPct: 60.5, stage: "FeePaid", docsOk: 4, docsTotal: 4, feePaid: true, feeDue: 0, daysInStage: 2, counsellor: "Desk", source: "Referral" },
  { id: "a18", fullName: "Farah Khan", initials: "FK", course: "BBA - Business Admin", courseId: "bba", phone: "9764009988", meritPct: 76.3, stage: "Enrolled", docsOk: 4, docsTotal: 4, feePaid: true, feeDue: 0, daysInStage: 0, counsellor: "Jadhav", source: "Walk-in" },
  { id: "a19", fullName: "Omkar Desai", initials: "OD", course: "BCA - Computer Application", courseId: "bca", phone: "9730022455", meritPct: 69.9, stage: "Enquiry", docsOk: 1, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 0, counsellor: "Desk", source: "Phone" },
  { id: "a20", fullName: "Rutuja Mane", initials: "RM", course: "M.Sc. Computer Management", courseId: "msccm", phone: "9422013678", meritPct: 82.6, stage: "Applied", docsOk: 2, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 3, counsellor: "Patil", source: "Website" },
  { id: "a21", fullName: "Aditya Nair", initials: "AN", course: "B.Sc. AI & ML", courseId: "bscaiml-x", phone: "9881133901", meritPct: 90.1, stage: "Merit", docsOk: 4, docsTotal: 4, feePaid: false, feeDue: 0, daysInStage: 1, counsellor: "Jadhav", source: "Expo" },
];

export const SEED_FILLS: CourseFill[] = [
  { courseId: "bca", name: "BCA", filled: 96, total: 120 },
  { courseId: "bsccs", name: "B.Sc. CS", filled: 88, total: 120 },
  { courseId: "bscse", name: "B.Sc. SE", filled: 61, total: 80 },
  { courseId: "bscnt", name: "B.Sc. NT", filled: 44, total: 60 },
  { courseId: "bba", name: "BBA", filled: 52, total: 60 },
  { courseId: "msccs", name: "M.Sc. CS", filled: 30, total: 40 },
];

export const SEED_ACTIONS: CrmAction[] = [
  { title: "Fee overdue \u2014 Vikram Reddy (MBA)", detail: "\u20B929,900 \u00B7 9 days in Offered \u00B7 call today", kind: "Fee" },
  { title: "Fee overdue \u2014 Pooja Kulkarni (Data Science)", detail: "\u20B917,900 \u00B7 6 days in Offered \u00B7 send reminder", kind: "Fee" },
  { title: "Docs missing \u2014 Rahul Shinde (B.Sc. CS)", detail: "2/4 \u00B7 TC + caste certificate pending", kind: "Docs" },
  { title: "Docs missing \u2014 Sneha Deshmukh (BCA)", detail: "3/4 \u00B7 photo ID pending \u00B7 merit 91.2%", kind: "Docs" },
  { title: "Merit list \u2014 4 applicants ready", detail: "Divya 93.5 \u00B7 Aditya 90.1 \u00B7 Priya 89.4 \u00B7 publish?", kind: "Merit" },
  { title: "New enquiries \u2014 3 unassigned", detail: "Sagar, Rohit, Omkar \u00B7 assign counsellor", kind: "New" },
];

const BASE = "https://www.cocsit.org.in/download/syllabus";

export const COURSE_CATALOG: Course[] = [
  { id: "bsccs", cardTitle: "B.Sc. Computer Science", cardSubtitle: "Eligibility: 12th Science \u00B7 3 Years", popupSubtitle: "Fees/Year: \u20B917,900", fySyllabusUrl: `${BASE}/2024-25/05BScComputerScienceSingleMajorFirstYearNEPSyllabuswef202425.pdf`, sySyllabusUrl: `${BASE}/2025-26/5BScComputerScienceSingalMajorSecondYearsyllabuswef202526.pdf`, tySyllabusUrl: `${BASE}/2026-27/B.Sc_.-III-Year-Computer-Science-Single-Major-Syllabus-2026-27.pdf` },
  { id: "bscse", cardTitle: "B.Sc. Software Engineering", cardSubtitle: "Eligibility: 12th Science \u00B7 3 Years", popupSubtitle: "Fees/Year: \u20B917,900", fySyllabusUrl: `${BASE}/2024-25/07BScSoftwareEngineeringSingleMajorFirstYearNEPSyllabuswef202425.pdf`, sySyllabusUrl: `${BASE}/2025-26/3BScSoftwareEngineeringSingalMajorSecondYearsyllabuswef202526.pdf`, tySyllabusUrl: `${BASE}/2026-27/B.Sc_.-III-Year-Software-Engineering-Single-Major-Syllabus-2026-27.pdf` },
  { id: "bscnt", cardTitle: "B.Sc. Network Technology", cardSubtitle: "Eligibility: 12th Science \u00B7 3 Years", popupSubtitle: "Fees/Year: \u20B917,900", fySyllabusUrl: `${BASE}/2024-25/06BScComputerNetworkTechnologySingleMajorFirstYearNEPSyllabuswef202425.pdf`, sySyllabusUrl: `${BASE}/2025-26/4BScComputerNetworkTechnologySingalMajorSecondYearsyllabuswef202526.pdf`, tySyllabusUrl: `${BASE}/2026-27/B.Sc_.-III-Year-Network-Technology-Single-Major-Syllabus-2026-27.pdf` },
  { id: "msccs", cardTitle: "M.Sc. Computer Science", cardSubtitle: "Eligibility: Any Computer UG \u00B7 2 Years", popupSubtitle: "Fees/Year: \u20B929,900", fySyllabusUrl: `${BASE}/2023-24/12MScComputerScienceFirstyearAffiliatedCollege.pdf`, sySyllabusUrl: `${BASE}/2024-25/13MScComputerScienceAffiliatedCollegesSecondYearSyllabuswef202425.pdf` },
  { id: "mscse", cardTitle: "M.Sc. Software Engineering", cardSubtitle: "Eligibility: Any Computer UG \u00B7 2 Years", popupSubtitle: "Fees/Year: \u20B929,900", fySyllabusUrl: `${BASE}/2023-24/MScFirstYearSoftwareEngineeringsyllabuswef202324.pdf`, sySyllabusUrl: `${BASE}/2024-25/14MScSoftwareEngineeringSecondYearSyllabuswef202425.pdf` },
  { id: "bca", cardTitle: "BCA - Computer Application", cardSubtitle: "Eligibility: Any 12th \u00B7 3 Years", popupSubtitle: "Fees/Year: \u20B917,900", fySyllabusUrl: `${BASE}/2024-25/2.SRTMUN-BCA-Final%2023-10-2024QP%20(2).pdf`, sySyllabusUrl: `${BASE}/2025-26/07BScBCASingalMajorSecondYearsyllabuswef202526.pdf`, tySyllabusUrl: `${BASE}/2026-27/B.C.A.-III-Year-Single-Major-Syllabus-2026-27.pdf` },
  { id: "bscbt", cardTitle: "B.Sc. Biotechnology", cardSubtitle: "Eligibility: 12th Science \u00B7 3 Years", popupSubtitle: "Fees/Year: \u20B917,900", fySyllabusUrl: `${BASE}/2025-26/B.sc%20Biotechnology%20FirstYear%20NEP%20Syllabus%202024-25.pdf`, sySyllabusUrl: `${BASE}/2025-26/BSc%20Biotechnology%20Second%20Year%20Syllabus%20202526.pdf`, tySyllabusUrl: `${BASE}/2026-27/B.Sc_.-III-Year-Biotechnology-Syllabus-2026-27.pdf` },
  { id: "mscbt", cardTitle: "M.Sc. Biotechnology", cardSubtitle: "Eligibility: Any UG \u00B7 2 Years", popupSubtitle: "Fees/Year: \u20B917,900", fySyllabusUrl: `${BASE}/2025-26/MScBiotechnologyFirstyear.pdf`, sySyllabusUrl: `${BASE}/2025-26/MSc_Biotechnology%20Second_Year_Syllabus.pdf` },
  { id: "bba", cardTitle: "BBA - Business Administration", cardSubtitle: "Eligibility: Any 12th \u00B7 3 Years", popupSubtitle: "Fees/Year: \u20B917,900", fySyllabusUrl: `${BASE}/2024-25/BBA%20FY%20NEP%20Syllbus.pdf`, tySyllabusUrl: `${BASE}/2026-27/B.B.A.-TY-NEP-Syllabus-Affiliated-College.pdf` },
  { id: "mba", cardTitle: "MBA - Business Administration", cardSubtitle: "Eligibility: Any UG \u00B7 2 Years", popupSubtitle: "Fees/Year: \u20B917,900" },
];
