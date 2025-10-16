import { AppShell } from "./components/layout/AppShell";
import { PersonnelGroupPreview } from "./components/personnel/PersonnelGroupPreview";
import { PersonnelGroupTable } from "./components/personnel/PersonnelGroupTable";

export default function App() {
  return (
    <AppShell>
      <PersonnelGroupTable />
    </AppShell>
  );
}
