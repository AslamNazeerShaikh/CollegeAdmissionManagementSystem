import { docsLabel, feeLabel, meritLabel } from "../crm/data";
import { useCrm } from "../crm/store";
import { SectionTitle } from "../crm/ui";

// Vertical register: one card per applicant, meta wraps in a 2-col grid.
// No fixed 6-column row, so nothing ever scrolls horizontally.
export default function Applications() {
  const s = useCrm();
  const rows = s.filtered;
  return (
    <div className="p-4 space-y-3 max-w-full overflow-x-clip">
      <SectionTitle>Applications — register</SectionTitle>
      <p className="text-[11px] text-muted">{rows.length} shown · tap a card → detail rail</p>

      {rows.length === 0 ? (
        <p className="text-[13px] text-muted text-center py-8">No applicants match your search</p>
      ) : (
        <div className="space-y-2 min-w-0">
          {rows.map((a) => {
            const selected = s.selected?.id === a.id;
            return (
              <button
                key={a.id}
                onClick={() => s.selectApplicant(a)}
                className={`w-full min-w-0 text-left px-3 py-3 bg-surface rounded-lg border transition-colors duration-150 ease-out cursor-pointer ${
                  selected ? "border-accent border-2" : "border-line hover:border-ink2"
                }`}
              >
                <span className="flex items-baseline gap-2 min-w-0">
                  <span className="min-w-0 flex-1 text-[13px] font-semibold text-ink truncate">{a.fullName}</span>
                  <span className="shrink-0 text-[13px] text-ink tnum">{meritLabel(a)}</span>
                </span>
                <span className="block text-[12px] text-ink2 truncate">{a.course}</span>
                <span className="mt-1.5 grid grid-cols-3 gap-2">
                  <span className="min-w-0">
                    <span className="block text-[10px] text-muted">STAGE</span>
                    <span className="block text-[12px] text-accent truncate">{a.stage === "FeePaid" ? "Fee paid" : a.stage}</span>
                  </span>
                  <span className="min-w-0">
                    <span className="block text-[10px] text-muted">DOCS</span>
                    <span className="block text-[12px] text-ink2 truncate">{docsLabel(a)}</span>
                  </span>
                  <span className="min-w-0 text-right">
                    <span className="block text-[10px] text-muted">FEE</span>
                    <span className="block text-[12px] font-semibold text-warn truncate">{feeLabel(a)}</span>
                  </span>
                </span>
              </button>
            );
          })}
        </div>
      )}
    </div>
  );
}
