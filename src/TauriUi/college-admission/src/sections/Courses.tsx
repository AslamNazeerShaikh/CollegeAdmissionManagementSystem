import { openUrl } from "@tauri-apps/plugin-opener";
import { COURSE_CATALOG, useCrm } from "../crm/store";
import { Card, CrmButton, SectionTitle } from "../crm/ui";

export default function Courses() {
  const s = useCrm();
  return (
    <div className="p-4 space-y-3 max-w-full overflow-x-clip min-w-0">
      <SectionTitle>Course catalog — seats, fees, syllabi</SectionTitle>
      <div className="grid grid-cols-[repeat(auto-fill,minmax(240px,1fr))] gap-2 min-w-0">
        {COURSE_CATALOG.map((c) => (
          <Card key={c.id} className="p-3 space-y-1.5 min-w-0">
            <div className="text-sm font-bold text-ink text-balance">{c.cardTitle}</div>
            <div className="text-xs text-ink2 text-balance">{c.cardSubtitle}</div>
            <div className="text-xs text-ink2">{c.popupSubtitle}</div>
            <div className="flex flex-wrap gap-2 pt-1">
              <SyllabusBtn label="FY syllabus" url={c.fySyllabusUrl} />
              <SyllabusBtn label="SY syllabus" url={c.sySyllabusUrl} />
              <SyllabusBtn label="TY syllabus" url={c.tySyllabusUrl} />
              <CrmButton onClick={() => s.addApplication(c.id)}>Apply →</CrmButton>
            </div>
          </Card>
        ))}
      </div>
    </div>
  );
}

function SyllabusBtn({ label, url }: { label: string; url?: string }) {
  return (
    <CrmButton
      kind="outline"
      disabled={!url}
      onClick={() => url && openUrl(url).catch(() => alert(`Could not open ${label}`))}
    >
      {label}
    </CrmButton>
  );
}
