import { useMemo, useState } from "react";
import { PersonnelTable } from "./PersonnelTable";
import { UserStatus, type Personnel } from "@/models/personnel";

const demo = [
  {
    id: "1",
    initials: "JHH",
    fullName: "Jens Hansen",
    email: "jens.hansen@example.com",
    userStatus: UserStatus.Active,
    doctorTypeName: "Kirurg",
  },

  {
    id: "2",
    initials: "MLL",
    fullName: "Mette Larsen",
    email: "mette.larsen@example.com",
    userStatus: UserStatus.Inactive,
    doctorTypeName: "Anæstesilæge",
  },
] satisfies Personnel[];

export function PersonnelPage() {
  const [list] = useState(demo);

  const sorted = useMemo(
    () => [...list].sort((a, b) => a.fullName.localeCompare(b.fullName)),
    [list]
  );

  return (
    <section className="space-y-4">
      <header>
        <h2 className="text-2xl font-semibold">Personale</h2>
        <p className="text-muted-foreground">Lorem ipsum</p>
      </header>

      <PersonnelTable
        items={sorted}
        onEdit={(person) => console.log("Edit clicked", person)}
        onDeactivate={(person) => console.log("Deactivate clicked", person)}
      />
    </section>
  );
}
