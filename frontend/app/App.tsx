import React from "react";
import { AppShell } from "./components/layout/AppShell";
import { PersonnelGroupPage } from "./components/personnel/PersonnelGroupPage";

export default function App() {
  const [tab, setTab] = React.useState<"groups" | "personnel">("groups");

  return (
    <AppShell activeTab={tab} onTabChange={setTab}>
      {tab === "groups" ? <PersonnelGroupPage /> : <div>Personnel Page</div>}
    </AppShell>
  );
}
