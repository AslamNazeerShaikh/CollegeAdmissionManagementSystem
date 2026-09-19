import { feeDueLabel, feeLabel } from "../crm/data";
import { useCrm } from "../crm/store";
import { Card, CrmButton, SectionTitle } from "../crm/ui";

export default function Fees() {
  const s = useCrm();
  const due = s.feeDueList;
  return (
    <div className="p-4 space-y-3 max-w-full overflow-x-clip min-w-0">
      <div className="flex flex-wrap items-center gap-x-2 gap-y-1 min-w-0">
        <div className="flex-1">
          <SectionTitle>Fees — collection queue</SectionTitle>
        </div>
        <span className="text-xs text-muted mr-2">Outstanding:</span>
        <span className="text-lg font-bold text-warn tnum">{feeDueLabel(s.applicants)}</span>
      </div>

      {due.length === 0 ? (
        <p className="text-[13px] text-muted text-center py-8">Nothing outstanding 🎉</p>
      ) : (
        <div className="space-y-2 min-w-0">
          {due.map((a) => (
            <Card key={a.id} className="p-3 min-w-0">
              <div className="flex items-center gap-3 min-w-0">
              <div className="flex-1 min-w-0">
                <div className="text-[13px] font-semibold text-ink truncate">{a.fullName}</div>
                <div className="text-xs text-muted truncate">{a.course}</div>
              </div>
              <span className="text-sm font-bold text-warn tnum shrink-0">{feeLabel(a)}</span>
              <CrmButton kind="success" onClick={() => s.collectFee(a)}>
                Collect
              </CrmButton>
              </div>
            </Card>
          ))}
        </div>
      )}
    </div>
  );
}
