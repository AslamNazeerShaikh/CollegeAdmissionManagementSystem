import { STAGES, STAGE_LABEL, feeDueLabel } from "../crm/data";
import { SEED_ACTIONS, SEED_FILLS, useCrm } from "../crm/store";
import { Card, Pill, StatCard } from "../crm/ui";

export default function Dashboard() {
  const s = useCrm();
  return (
    <div className="p-4 space-y-4">
      <div>
        <h1 className="text-[26px] leading-8 font-semibold text-ink">Dashboard</h1>
        <p className="text-[13px] text-muted">{s.total} applicants · 2026–27 cycle</p>
      </div>

      <div className="flex flex-wrap gap-3">
        <StatCard label="Total applicants" value={`${s.total}`} sub="2026–27 cycle" />
        <StatCard label="New enquiries" value={`${s.lane("Enquiry").length}`} sub="need counsellor" valueClass="text-info" />
        <StatCard label="Enrolled" value={`${s.enrolledCount}`} sub="admissions confirmed" />
        <StatCard label="Fees due" value={feeDueLabel(s.applicants)} sub={`${s.feeDueList.length} applicants`} />
        <StatCard label="Docs pending" value={`${s.docsPendingCount}`} sub="awaiting verification" valueClass="text-bad" />
      </div>

      <h2 className="text-[18px] font-semibold text-ink">Needs action today</h2>
      <div className="space-y-2">
        {SEED_ACTIONS.map((a) => (
          <Card key={a.title} className="px-3 py-2.5 flex items-center gap-3">
            <Pill>{a.kind}</Pill>
            <div>
              <div className="text-[13px] font-semibold text-ink">{a.title}</div>
              <div className="text-[12px] text-ink2">{a.detail}</div>
            </div>
          </Card>
        ))}
      </div>

      <h3 className="text-sm font-semibold text-ink">Intake funnel</h3>
      <Card className="p-3">
        <div className="flex flex-wrap gap-x-4 gap-y-2">
          {STAGES.map((st) => (
            <div key={st} className="w-24 text-center">
              <div
                className={`text-xl font-bold tnum ${
                  st === "Merit" ? "text-accent" : st === "FeePaid" ? "text-warn" : st === "Enrolled" ? "text-ok" : "text-ink"
                }`}
              >
                {s.lane(st).length}
              </div>
              <div className="text-[11px] text-muted">{STAGE_LABEL[st]}</div>
            </div>
          ))}
        </div>
      </Card>

      <h3 className="text-sm font-semibold text-ink">Course fill %</h3>
      <div className="space-y-2">
        {SEED_FILLS.map((f) => (
          <Card key={f.courseId} className="px-3 py-2.5 flex items-center gap-3">
            <div className="w-36 text-[13px] font-semibold text-ink">{f.name}</div>
            <div className="flex-1 h-2 rounded bg-linesoft overflow-hidden">
              <div className="h-full rounded bg-accent" style={{ width: `${(f.filled / f.total) * 100}%` }} />
            </div>
            <div className="w-[70px] text-right text-[12px] text-ink2 tnum">
              {Math.round((f.filled / f.total) * 100)}% full
            </div>
          </Card>
        ))}
      </div>
    </div>
  );
}
