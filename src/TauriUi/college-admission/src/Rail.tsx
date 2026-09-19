import { STAGE_LABEL, docsLabel, feeLabel, meritLabel } from "./crm/data";
import { useCrm } from "./crm/store";
import { Avatar, CrmButton } from "./crm/ui";

export default function Rail({ stacked = false }: { stacked?: boolean }) {
  const s = useCrm();
  const a = s.selected;
  return (
    <aside
      className={`bg-surface p-4 overflow-y-auto overflow-x-clip min-w-0 ${
        stacked ? "border-t border-line" : "border-l border-line"
      }`}
    >
      {!a ? (
        <p className="text-[13px] text-muted">Select an applicant</p>
      ) : (
        <div className="space-y-2.5">
          <div className="text-[10px] text-muted">APPLICANT</div>
          <Avatar initials={a.initials} />
          <div>
            <div className="text-lg font-bold text-ink">{a.fullName}</div>
            <div className="text-[13px] text-ink2">{a.course}</div>
            <div className="text-[13px] text-ink tnum">{a.phone}</div>
          </div>

          <div className="grid grid-cols-2 gap-2">
            <div>
              <div className="text-[10px] text-muted">MERIT</div>
              <div className="text-[26px] leading-8 font-bold text-accent tnum">{meritLabel(a)}</div>
            </div>
            <div>
              <div className="text-[10px] text-muted">STAGE</div>
              <div className="text-sm font-semibold text-ink">{STAGE_LABEL[a.stage]}</div>
              <div className="text-xs text-ink2">{docsLabel(a)}</div>
            </div>
            <div>
              <div className="text-[10px] text-muted">FEE</div>
              <div className="text-sm font-semibold text-warn tnum">{feeLabel(a)}</div>
            </div>
            <div>
              <div className="text-[10px] text-muted">COUNSELLOR</div>
              <div className="text-[13px] text-ink">{a.counsellor}</div>
            </div>
          </div>

          <CrmButton className="w-full" onClick={() => s.advanceStage()}>
            Advance stage →
          </CrmButton>
          <CrmButton className="w-full" kind="success" onClick={() => s.collectFee()}>
            Collect fee
          </CrmButton>
          <CrmButton className="w-full" kind="outline" onClick={() => s.addEnquiry()}>
            New application
          </CrmButton>

          <hr className="border-linesoft" />
          <div className="text-[10px] text-muted">ACTIVITY</div>
          <p className="text-xs text-ink2 leading-5">
            · Enquiry logged
            <br />· Docs uploaded for verification
            <br />· Merit auto-computed from marks
          </p>
        </div>
      )}
    </aside>
  );
}
