import { useState } from "react";
import { STAGES, STAGE_LABEL, docsLabel, feeLabel, meritLabel, type Applicant, type CrmStage } from "../crm/data";
import { useCrm } from "../crm/store";
import { Avatar, SectionTitle } from "../crm/ui";

// Stage tabs + vertical applicant list. No horizontal scrolling.
export default function Pipeline() {
  const s = useCrm();
  const [tab, setTab] = useState<CrmStage>(s.selected?.stage ?? "Enquiry");
  const items = s.lane(tab);
  return (
    <div className="p-4 space-y-3 max-w-full overflow-x-clip min-w-0">
      <div>
        <SectionTitle>Admission pipeline — merit board</SectionTitle>
        <p className="text-[11px] text-muted">pick a stage · tap a card → detail rail · Advance moves stage</p>
      </div>

      <div className="flex flex-wrap gap-2">
        {STAGES.map((st) => {
          const active = st === tab;
          return (
            <button
              key={st}
              onClick={() => setTab(st)}
              className={`min-h-10 px-3.5 rounded-lg text-[13px] border transition-colors duration-150 ease-out cursor-pointer ${
                active
                  ? "bg-accentsoft text-accent border-accent font-semibold"
                  : "bg-surface text-ink2 border-line hover:bg-surface2"
              }`}
            >
              {STAGE_LABEL[st]} · {s.lane(st).length}
            </button>
          );
        })}
      </div>

      {items.length === 0 ? (
        <p className="text-[13px] text-muted text-center py-8">No applicants in {STAGE_LABEL[tab]}</p>
      ) : (
        <div className="space-y-2 min-w-0">
          {items.map((a) => (
            <Row key={a.id} a={a} />
          ))}
        </div>
      )}
    </div>
  );
}

function Row({ a }: { a: Applicant }) {
  const s = useCrm();
  const selected = s.selected?.id === a.id;
  return (
    <button
      onClick={() => s.selectApplicant(a)}
      className={`w-full min-w-0 text-left bg-surface rounded-lg border px-3 py-3 flex items-center gap-3 transition-colors duration-150 ease-out cursor-pointer ${
        selected ? "border-accent border-2" : "border-line hover:border-ink2"
      }`}
    >
      <Avatar initials={a.initials} />
      <span className="flex-1 min-w-0">
        <span className="block text-sm font-semibold text-ink truncate">{a.fullName}</span>
        <span className="block text-xs text-muted truncate">
          {a.course} · {a.phone}
        </span>
      </span>
      <span className="text-right shrink-0">
        <span className="block text-sm font-bold text-accent tnum">{meritLabel(a)}</span>
        <span className="block text-[11px] text-ink2">
          {docsLabel(a)} · {feeLabel(a)}
        </span>
      </span>
      <span className="text-xl text-muted shrink-0">›</span>
    </button>
  );
}
