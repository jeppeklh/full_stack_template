import { usePersonnelGroupList } from "@/hooks/usePersonnelGroup";
import type { PersonnelGroup } from "@/models/personnel";
import { PersonnelGroupTable } from "./PersonnelGroupTable";

export function PersonnelGroupPage() {
  const { query } = usePersonnelGroupList();

  if (query.isLoading) {
    return <div>Loading...</div>;
  }
  if (query.isError) {
    return <div>Error: {String(query.error)}</div>;
  }
  const groups = query.data ?? [];

  return (
    <PersonnelGroupTable
      groups={groups}
      onEdit={(group: PersonnelGroup) => {
        console.log("Edit clicked", group);
      }}
      onDelete={(group: PersonnelGroup) => {
        console.log("Delete clicked", group);
      }}
    />
  );
}
