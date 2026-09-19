import { STAGES, STAGE_LABEL, feeDueLabel } from "../crm/data";
import { SEED_ACTIONS, SEED_FILLS, useCrm } from "../crm/store";
import { Card, Pill, StatCard } from "../crm/ui";

export default function Dashboard() {
  const s = useCrm();
  return (
    <div className="p-4 space-y-4 max-w-full overflow-x-clip">
      <div>
        <h1 className="text-[26px] leading-8 font-semibold text-ink">Dashboard</h1>
        <p className="text-[13px] text-muted">{s.total} applicants · 2026–27 cycle</p>
      </div>

      <div className="grid grid-cols-[repeat(auto-fill,minmax(150px,1fr))] gap-3">
        <StatCard label="Total applicants" value={`${s.total}`} sub="2026–27 cycle" />
        <StatCard label="New enquiries" value={`${s.lane("Enquiry").length}`} sub="need counsellor" valueClass="text-info" />
        <StatCard label="Enrolled" value={`${s.enrolledCount}`} sub="admissions confirmed" />
        <StatCard label="Fees due" value={feeDueLabel(s.applicants)} sub={`${s.feeDueList.length} applicants`} />
        <StatCard label="Docs pending" value={`${s.docsPendingCount}`} sub="awaiting verification" valueClass="text-bad" />
      </div>

      <h2 className="text-[18px] font-semibold text-ink">Needs action today</h2>
      <div className="space-y-2 min-w-0">
        {SEED_ACTIONS.map((a) => (
          <Card key={a.title} className="px-3 py-2.5 flex items-start gap-3 min-w-0">
            <Pill>{a.kind}</Pill>
            <div className="min-w-0">
              <div className="text-[13px] font-semibold text-ink text-balance">{a.title}</div>
              <div className="text-[12px] text-ink2 text-balance">{a.detail}</div>
            </div>
          </Card>
        ))}
      </div>

      <h3 className="text-sm font-semibold text-ink">Intake funnel</h3>
      <Card className="p-3">
        <div className="grid grid-cols-[repeat(auto-fill,minmax(88px,1fr))] gap-x-4 gap-y-3">
          {STAGES.map((st) => (
            <div key={st} className="min-w-0 text-center">
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
      <div className="space-y-2 min-w-0">
        {SEED_FILLS.map((f) => (
          <Card key={f.courseId} className="px-3 py-2.5 min-w-0">
            <div className="flex items-baseline gap-2">
              <div className="min-w-0 flex-1 text-[13px] font-semibold text-ink truncate">{f.name}</div>
              <div className="shrink-0 text-[12px] text-ink2 tnum">
                {Math.round((f.filled / f.total) * 100)}% full
              </div>
            </div>
            <div className="mt-2 h-2 rounded bg-linesoft overflow-hidden">
              <div className="h-full rounded bg-accent" style={{ width: `${(f.filled / f.total) * 100}%` }} />
            </div>
          </Card>
        ))}
      </div>
    </div>
  );
}
