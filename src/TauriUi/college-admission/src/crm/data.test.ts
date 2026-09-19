import { describe, it } from "node:test";
import assert from "node:assert/strict";
import {
  STAGES,
  docsLabel,
  feeDueLabel,
  feeLabel,
  meritLabel,
  type Applicant,
} from "./data.ts";

const base: Applicant = {
  id: "t1", fullName: "Test", initials: "T", course: "BCA",
  courseId: "bca", phone: "98", meritPct: 91.234, stage: "Merit",
  docsOk: 3, docsTotal: 4, feePaid: false, feeDue: 17900,
  daysInStage: 1, counsellor: "Desk", source: "Walk-in",
};

describe("crm labels", () => {
  it("meritLabel keeps one decimal", () => {
    assert.equal(meritLabel(base), "91.2%");
  });
  it("docsLabel is ok/total", () => {
    assert.equal(docsLabel(base), "3/4 docs");
  });
  it("feeLabel shows due, Paid, or em-dash", () => {
    assert.ok(feeLabel(base).includes("17,900"));
    assert.equal(feeLabel({ ...base, feePaid: true }), "Paid");
    assert.equal(feeLabel({ ...base, feeDue: 0 }), "—");
  });
  it("feeDueLabel sums unpaid only", () => {
    const list = [base, { ...base, id: "t2", feePaid: true, feeDue: 0 }];
    assert.ok(feeDueLabel(list).includes("17,900"));
    assert.ok(feeDueLabel([]).includes("0"));
  });
  it("STAGES order ends at Enrolled", () => {
    assert.equal(STAGES[0], "Enquiry");
    assert.equal(STAGES[STAGES.length - 1], "Enrolled");
  });
});
