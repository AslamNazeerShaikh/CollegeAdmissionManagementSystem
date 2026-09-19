import { useEffect, useState } from "react";
import { CrmProvider, SECTIONS, useCrm, type Section } from "./crm/store";
import { CrmButton } from "./crm/ui";
import Dashboard from "./sections/Dashboard";
import Pipeline from "./sections/Pipeline";
import Applications from "./sections/Applications";
import Courses from "./sections/Courses";
import Fees from "./sections/Fees";
import Rail from "./Rail";

export default function App() {
  return (
    <CrmProvider>
      <Shell />
    </CrmProvider>
  );
}

function Shell() {
  const s = useCrm();
  const [drawer, setDrawer] = useState(false);
  // Stack vertically below 1100px so the 3-pane layout never squeezes
  // into a horizontal scroll when the macOS window is resized smaller.
  const [narrow, setNarrow] = useState(window.innerWidth < 1100);

  useEffect(() => {
    const onResize = () => setNarrow(window.innerWidth < 1100);
    window.addEventListener("resize", onResize);
    return () => window.removeEventListener("resize", onResize);
  }, []);

  return (
    <div className="h-full flex flex-col min-w-0 overflow-x-clip">
      <TopBar narrow={narrow} onMenu={() => setDrawer(true)} />

      {narrow ? (
        <div className="flex-1 min-h-0 overflow-y-auto overflow-x-clip">
          <div key={s.section} className="fade-section min-w-0">
            <SectionView section={s.section} />
          </div>
          <Rail stacked />
        </div>
      ) : (
        <div className="flex-1 flex min-h-0 min-w-0 overflow-x-clip">
          <nav className="w-[260px] shrink-0 min-w-0">
            <Nav onNavigate={() => {}} />
          </nav>

          <main className="flex-1 min-w-0 overflow-y-auto overflow-x-clip">
            <div key={s.section} className="fade-section min-w-0">
              <SectionView section={s.section} />
            </div>
          </main>

          <div className="w-[300px] shrink-0 min-w-0 overflow-y-auto overflow-x-clip">
            <Rail />
          </div>
        </div>
      )}

      {narrow && (
        <>
          <div
            className={`fixed inset-0 bg-black/30 z-40 transition-opacity duration-200 ${drawer ? "opacity-100" : "opacity-0 pointer-events-none"}`}
            onClick={() => setDrawer(false)}
          />
          <div
            className={`fixed top-0 bottom-0 left-0 w-[300px] z-50 transition-transform duration-200 ease-out ${
              drawer ? "translate-x-0" : "-translate-x-full"
            }`}
          >
            <Nav onNavigate={() => setDrawer(false)} />
          </div>
        </>
      )}
    </div>
  );
}

function SectionView({ section }: { section: Section }) {
  switch (section) {
    case "Dashboard":
      return <Dashboard />;
    case "Pipeline":
      return <Pipeline />;
    case "Applications":
      return <Applications />;
    case "Courses":
      return <Courses />;
    case "Fees":
      return <Fees />;
  }
}

function TopBar({ narrow, onMenu }: { narrow: boolean; onMenu: () => void }) {
  const s = useCrm();
  // Narrow phones: two rows (menu + search, then both CTAs half-width each)
  // so nothing clips and nothing scrolls horizontally.
  if (narrow) {
    return (
      <header className="shrink-0 bg-surface border-b border-line px-4 pt-[env(safe-area-inset-top)] pb-2 space-y-2">
        <div className="flex items-center gap-2">
          <button
            onClick={onMenu}
            aria-label="Open navigation"
            className="w-11 h-10 flex flex-col items-center justify-center gap-[5px] cursor-pointer shrink-0"
          >
            <span className="block w-5 h-[2.5px] rounded bg-ink" />
            <span className="block w-5 h-[2.5px] rounded bg-ink" />
            <span className="block w-5 h-[2.5px] rounded bg-ink" />
          </button>
          <div className="flex-1">
            <input
              value={s.search}
              onChange={(e) => s.setSearch(e.target.value)}
              placeholder="Search name, course, phone…"
              className="w-full h-[42px] px-3 text-[13px] bg-surface border border-line rounded-lg outline-none placeholder:text-muted focus:border-accent"
            />
          </div>
        </div>
        <div className="flex gap-2">
          <CrmButton className="flex-1" onClick={() => s.addEnquiry()}>
            + New enquiry
          </CrmButton>
          <CrmButton className="flex-1" kind="outline" onClick={() => s.addApplication()}>
            + New application
          </CrmButton>
        </div>
      </header>
    );
  }
  return (
    <header className="h-[60px] shrink-0 bg-surface border-b border-line px-4 flex items-center gap-2">
      <div className="flex-1 max-w-[420px] mr-4">
        <input
          value={s.search}
          onChange={(e) => s.setSearch(e.target.value)}
          placeholder="Search name, course, phone…"
          className="w-full h-[42px] px-3 text-[13px] bg-surface border border-line rounded-lg outline-none placeholder:text-muted focus:border-accent"
        />
      </div>
      <CrmButton onClick={() => s.addEnquiry()}>+ New enquiry</CrmButton>
      <CrmButton kind="outline" onClick={() => s.addApplication()}>
        + New application
      </CrmButton>
    </header>
  );
}

function Nav({ onNavigate }: { onNavigate: () => void }) {
  const s = useCrm();
  return (
    <div className="h-full bg-surface border-r border-line p-4 overflow-y-auto">
      <div className="flex items-center gap-2.5">
        <span className="w-9 h-9 rounded-[10px] bg-accent text-white text-lg flex items-center justify-center">
          ✦
        </span>
        <span className="min-w-0">
          <span className="block text-[15px] font-semibold text-ink truncate">College Admissions</span>
          <span className="block text-xs text-muted">CRM</span>
        </span>
      </div>
      <p className="text-[11px] text-muted mt-0.5 mb-3">2026–27 admission cycle</p>

      <div className="space-y-0.5">
        {SECTIONS.map((name) => {
          const active = s.section === name;
          return (
            <button
              key={name}
              onClick={() => {
                s.selectSection(name);
                onNavigate();
              }}
              className={`w-full text-left px-2.5 py-2.5 rounded-lg text-sm transition-colors duration-150 ease-out cursor-pointer ${
                active ? "bg-paper text-ink font-semibold" : "text-ink hover:bg-surface2"
              }`}
            >
              {name}
            </button>
          );
        })}
      </div>

      <hr className="border-linesoft my-3" />
      <p className="text-[11px] text-muted px-2.5 mb-1">MANAGEMENT</p>
      <div className="bg-paper rounded-lg p-2.5">
        <p className="text-xs font-semibold text-ink">Counsellor desk · Latur</p>
        <p className="text-[11px] text-muted">click card → rail · Advance moves stage</p>
      </div>
    </div>
  );
}
