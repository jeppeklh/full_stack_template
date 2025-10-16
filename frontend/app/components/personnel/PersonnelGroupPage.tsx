import { useState } from "react";
import { Button } from "@/components/ui/button";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
  DialogDescription,
} from "@/components/ui/dialog";
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
} from "@/components/ui/alert-dialog";
import { usePersonnelGroupList } from "@/hooks/usePersonnelGroup";
import type { PersonnelGroup, PersonnelGroupPayload } from "@/models/personnel";
import { PersonnelGroupTable } from "./PersonnelGroupTable";

export function PersonnelGroupPage() {
  const { query } = usePersonnelGroupList();
  const groups = query.data ?? [];

  // States
  const [dialogOpen, setDialogOpen] = useState(false);
  const [editing, setEditing] = useState<PersonnelGroup | null>(null);
  const [deleteTarget, setDeleteTarget] = useState<PersonnelGroup | null>(null);

  if (query.isLoading) {
    return <div>Henter Personalegruppper...</div>;
  }
  if (query.isError) {
    return <div>Kunne ikke hente personalegrupper</div>;
  }

  const handleSubmit = (payload: PersonnelGroupPayload) => {
    if (editing) {
      // Update existing group
      console.log("Edit group", { id: editing.id, payload });
    } else {
      // Create new group
      console.log("Create group", payload);
    }
    setDialogOpen(false);
    setEditing(null);
  };

  return (
    <section className="space-y-6">
      <div className="flex items-center justify-between">
        <h2 className="text-2xl font-semibold">Personalegrupper</h2>
        <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
          <DialogTrigger asChild>
            <Button onClick={() => setEditing(null)}>Tilføj gruppe</Button>
          </DialogTrigger>
          <DialogContent>
            <DialogHeader>
              <DialogTitle>
                {editing ? "Rediger gruppe" : "Tilføj gruppe"}
              </DialogTitle>
              <DialogDescription>
                Udfyld felterne og klik Gem for at gemme gruppen.
              </DialogDescription>
            </DialogHeader>
          </DialogContent>
        </Dialog>
      </div>

      <PersonnelGroupTable
        groups={groups}
        onEdit={(group: PersonnelGroup) => {
          console.log("Edit clicked", group);
          setEditing(group);
        }}
        onDelete={(group: PersonnelGroup) => {
          console.log("Delete clicked", group);
          setDeleteTarget(group);
        }}
      />

      <AlertDialog>
        <AlertDialogContent>
          <AlertDialogHeader>
            <AlertDialogTitle>Bekræft sletning</AlertDialogTitle>
            <AlertDialogDescription>
              Er du sikker på, at du vil slette gruppen?
            </AlertDialogDescription>
          </AlertDialogHeader>
          <AlertDialogFooter>
            <AlertDialogCancel>Annuller</AlertDialogCancel>
            <AlertDialogAction
              onClick={() => {
                if (deleteTarget) console.log("Delete id", deleteTarget.id);
                setDeleteTarget(null);
              }}
            >
              Slet
            </AlertDialogAction>
          </AlertDialogFooter>
        </AlertDialogContent>
      </AlertDialog>
    </section>
  );
}
