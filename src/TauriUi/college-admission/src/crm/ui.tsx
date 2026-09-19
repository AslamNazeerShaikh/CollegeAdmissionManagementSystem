import type { ReactNode } from "react";

// One button owns all call sites. Flat fills, 1px borders, radius 8,
// 40px height — ReferenceUi, not framework defaults (no elevation).
export function CrmButton({
  children,
  onClick,
  kind = "primary",
  disabled,
  className = "",
}: {
  children: ReactNode;
  onClick?: () => void;
  kind?: "primary" | "outline" | "success";
  disabled?: boolean;
  className?: string;
}) {
  const styles =
    kind === "primary"
      ? "bg-accent text-accentink hover:brightness-95"
      : kind === "success"
        ? "bg-ok text-white hover:brightness-95"
        : "bg-transparent text-ink2 border border-line hover:bg-surface2";
  return (
    <button
      onClick={onClick}
      disabled={disabled}
      className={`min-h-10 px-3.5 rounded-lg text-[13px] font-semibold transition-[filter,background-color] duration-150 ease-out active:scale-[0.98] disabled:opacity-50 disabled:pointer-events-none cursor-pointer ${styles} ${className}`}
    >
      {children}
    </button>
  );
}

// Status pill: 999px radius, 11px semibold.
export function Pill({ children }: { children: ReactNode }) {
  return (
    <span className="inline-block px-[9px] py-1 rounded-full text-[11px] font-semibold bg-warnbg text-warn">
      {children}
    </span>
  );
}

// Circular initials avatar, 36px.
export function Avatar({ initials }: { initials: string }) {
  return (
    <span className="inline-flex w-9 h-9 rounded-full bg-accentsoft text-accent text-[13px] font-bold items-center justify-center shrink-0">
      {initials}
    </span>
  );
}

// KPI card: white, 1px border, radius 12, generous padding, no shadow.
export function StatCard({
  label,
  value,
  sub,
  valueClass = "text-ink",
}: {
  label: string;
  value: string;
  sub: string;
  valueClass?: string;
}) {
  return (
    <div className="w-40 bg-surface rounded-xl border border-line px-[18px] py-4">
      <div className="text-[13px] text-muted">{label}</div>
      <div className={`text-[26px] leading-8 font-semibold tnum ${valueClass}`}>{value}</div>
      <div className="text-[13px] text-muted">{sub}</div>
    </div>
  );
}

export function SectionTitle({ children }: { children: ReactNode }) {
  return <h2 className="text-xl font-bold text-ink">{children}</h2>;
}

export function Card({ children, className = "" }: { children: ReactNode; className?: string }) {
  return (
    <div className={`bg-surface rounded-lg border border-line ${className}`}>{children}</div>
  );
}
