import React from "react";
import { AppShell } from "./components/layout/AppShell";
import { PersonnelGroupPage } from "./components/personnel/PersonnelGroupPage";
import { PersonnelPage } from "./components/personnel/PersonnelPage";

export default function App() {
  const [tab, setTab] = React.useState<"groups" | "personnel">("groups");

  return (
    <AppShell activeTab={tab} onTabChange={setTab}>
      {tab === "groups" ? <PersonnelGroupPage /> : <PersonnelPage />}
    </AppShell>
  );
}
