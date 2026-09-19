import { docsLabel, feeLabel, meritLabel } from "../crm/data";
import { useCrm } from "../crm/store";
import { SectionTitle } from "../crm/ui";

// Dense register: 6-column grid rows, ~64px, horizontal separators only.
const COLS = "grid-cols-[2fr_2fr_1fr_1fr_1fr_1fr]";

export default function Applications() {
  const s = useCrm();
  const rows = s.filtered;
  return (
    <div className="p-4 space-y-3">
      <SectionTitle>Applications — dense register</SectionTitle>

      <div className={`grid ${COLS} gap-2 px-3 py-2 bg-surface rounded-lg border border-line text-[10px] text-muted`}>
        <span>NAME</span>
        <span>COURSE</span>
        <span>STAGE</span>
        <span>MERIT</span>
        <span>DOCS</span>
        <span>FEE</span>
      </div>

      {rows.length === 0 ? (
        <p className="text-[13px] text-muted text-center py-8">No applicants match your search</p>
      ) : (
        <div className="space-y-1">
          {rows.map((a) => {
            const selected = s.selected?.id === a.id;
            return (
              <button
                key={a.id}
                onClick={() => s.selectApplicant(a)}
                className={`w-full text-left grid ${COLS} gap-2 items-center px-3 py-3.5 bg-surface rounded-md border transition-colors duration-150 ease-out cursor-pointer ${
                  selected ? "border-accent border-2" : "border-line hover:border-ink2"
                }`}
              >
                <span className="text-[13px] font-semibold text-ink truncate">{a.fullName}</span>
                <span className="text-[12px] text-ink2 truncate">{a.course}</span>
                <span className="text-[12px] text-accent">{a.stage === "FeePaid" ? "Fee paid" : a.stage}</span>
                <span className="text-[13px] text-ink tnum">{meritLabel(a)}</span>
                <span className="text-[12px] text-ink2">{docsLabel(a)}</span>
                <span className="text-[12px] font-semibold text-warn">{feeLabel(a)}</span>
              </button>
            );
          })}
        </div>
      )}
    </div>
  );
}
