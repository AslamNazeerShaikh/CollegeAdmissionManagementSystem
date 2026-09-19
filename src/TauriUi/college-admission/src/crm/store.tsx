import { createContext, useCallback, useContext, useMemo, useState } from "react";
import type { ReactNode } from "react";
import {
  COURSE_CATALOG,
  SEED_ACTIONS,
  SEED_APPLICANTS,
  SEED_FILLS,
  STAGES,
  type Applicant,
  type CrmStage,
} from "./data";

// Port of CrmViewModel.cs / CrmState. No store library: useState + context.
export type Section = "Dashboard" | "Pipeline" | "Applications" | "Courses" | "Fees";
export const SECTIONS: Section[] = ["Dashboard", "Pipeline", "Applications", "Courses", "Fees"];

interface CrmStore {
  section: Section;
  selectSection: (s: Section) => void;
  search: string;
  setSearch: (v: string) => void;
  applicants: Applicant[];
  selected: Applicant | undefined;
  selectApplicant: (a: Applicant) => void;
  filtered: Applicant[];
  feeDueList: Applicant[];
  lane: (s: CrmStage) => Applicant[];
  total: number;
  enrolledCount: number;
  docsPendingCount: number;
  advanceStage: (a?: Applicant) => void;
  collectFee: (a?: Applicant) => void;
  addEnquiry: () => void;
  addApplication: (courseId?: string) => void;
}

const Ctx = createContext<CrmStore | null>(null);

let seq = 1;

export function CrmProvider({ children }: { children: ReactNode }) {
  const [section, setSection] = useState<Section>("Pipeline");
  const [search, setSearchState] = useState("");
  const [applicants, setApplicants] = useState<Applicant[]>(SEED_APPLICANTS);
  const [selectedId, setSelectedId] = useState<string | undefined>(SEED_APPLICANTS[0]?.id);

  const selected = useMemo(
    () => applicants.find((a) => a.id === selectedId) ?? applicants[0],
    [applicants, selectedId],
  );

  const filtered = useMemo(() => {
    const q = search.trim().toLowerCase();
    if (!q) return applicants;
    return applicants.filter(
      (a) =>
        a.fullName.toLowerCase().includes(q) ||
        a.course.toLowerCase().includes(q) ||
        a.phone.includes(q),
    );
  }, [applicants, search]);

  const order = (s: CrmStage) => STAGES.indexOf(s);
  const feeDueList = useMemo(
    () => applicants.filter((a) => !a.feePaid && order(a.stage) >= order("Offered")),
    [applicants],
  );

  const replace = useCallback((oldItem: Applicant, next: Applicant) => {
    setApplicants((prev) => {
      const i = prev.indexOf(oldItem);
      if (i < 0) return prev;
      const copy = [...prev];
      copy[i] = next;
      return copy;
    });
    if (selectedId === oldItem.id) setSelectedId(next.id);
  }, [selectedId]);

  const store: CrmStore = {
    section,
    selectSection: (s) => setSection(s),
    search,
    setSearch: (v) => setSearchState(v),
    applicants,
    selected,
    selectApplicant: (a) => setSelectedId(a.id),
    filtered,
    feeDueList,
    lane: (s) => applicants.filter((a) => a.stage === s),
    total: applicants.length,
    enrolledCount: applicants.filter((a) => a.stage === "Enrolled").length,
    docsPendingCount: applicants.filter((a) => a.docsOk < a.docsTotal && a.stage !== "Enrolled").length,

    advanceStage: (a) => {
      const t = a ?? selected;
      if (!t || t.stage === "Enrolled") return;
      replace(t, { ...t, stage: STAGES[order(t.stage) + 1], daysInStage: 0 });
    },
    collectFee: (a) => {
      const t = a ?? selected;
      if (!t || t.feePaid) return;
      let next: Applicant = { ...t, feePaid: true, feeDue: 0 };
      if (next.stage === "Offered") next = { ...next, stage: "FeePaid" };
      replace(t, next);
    },
    addEnquiry: () => {
      const n = seq++;
      const item: Applicant = {
        id: `walkin-${n}`, fullName: `Walk-in Enquiry ${n}`, initials: "WE",
        course: "BCA - Computer Application", courseId: "bca", phone: "98XXXXXXXX",
        meritPct: 0, stage: "Enquiry", docsOk: 0, docsTotal: 4,
        feePaid: false, feeDue: 17900, daysInStage: 0, counsellor: "Desk", source: "Walk-in",
      };
      setApplicants((prev) => [item, ...prev]);
      setSelectedId(item.id);
      setSection("Applications");
    },
    addApplication: (courseId) => {
      const course = COURSE_CATALOG.find((c) => c.id === courseId);
      const n = seq++;
      const item: Applicant = {
        id: `walkin-${n}`, fullName: `Walk-in Application ${n}`, initials: "WA",
        course: course?.cardTitle.replace("\n", " ") ?? "General enquiry",
        courseId: course?.id ?? "general", phone: "98XXXXXXXX",
        meritPct: 0, stage: "Enquiry", docsOk: 0, docsTotal: 4,
        feePaid: false, feeDue: course ? 17900 : 0, daysInStage: 0,
        counsellor: "Desk", source: "Walk-in",
      };
      setApplicants((prev) => [item, ...prev]);
      setSelectedId(item.id);
      setSection("Applications");
    },
  };

  return <Ctx.Provider value={store}>{children}</Ctx.Provider>;
}

export function useCrm(): CrmStore {
  const v = useContext(Ctx);
  if (!v) throw new Error("useCrm outside CrmProvider");
  return v;
}

export { COURSE_CATALOG, SEED_ACTIONS, SEED_FILLS };
