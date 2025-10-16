import { PersonnelTable } from "./PersonnelTable";
import { usePersonnelList } from "@/hooks/usePersonnel";

export function PersonnelPage() {
  const { query } = usePersonnelList();
  const personnel = query.data ?? [];

  if (query.isLoading) {
    return <div className="text-muted-foreground">Henter Personale...</div>;
  }

  if (query.isError) {
    return <div className="text-destructive">Kunne ikke hente personale.</div>;
  }

  return (
    <section className="space-y-4">
      <header>
        <h2 className="text-2xl font-semibold">Personale</h2>
        <p className="text-muted-foreground">Lorem ipsum</p>
      </header>

      <PersonnelTable
        items={personnel}
        onEdit={(person) => console.log("Edit clicked", person)}
        onDeactivate={(person) => console.log("Deactivate clicked", person)}
      />
    </section>
  );
}
