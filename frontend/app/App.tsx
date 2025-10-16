import { AppShell } from "./components/layout/AppShell";
import { PersonnelGroupPreview } from "./components/personnel/PersonnelGroupPreview";

export default function App() {
  return (
    <AppShell>
      <PersonnelGroupPreview />
    </AppShell>
  );
}
