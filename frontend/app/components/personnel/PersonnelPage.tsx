import { useMemo, useState } from "react";
import { Button } from "@/components/ui/button";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
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
import { PersonnelTable } from "./PersonnelTable";
import { PersonnelForm } from "./PersonnelForm";
import { usePersonnelList } from "@/hooks/usePersonnel";
import { usePersonnelGroupList } from "@/hooks/usePersonnelGroup";
import { type Personnel, type PersonnelPayload } from "@/models/personnel";

export function PersonnelPage() {
  const { query, createMutation, updateMutation, deactivateMutation } =
    usePersonnelList(false);
  const { query: groupQuery } = usePersonnelGroupList();
  const personnel = query.data ?? [];
  const groups = groupQuery.data ?? [];

  const sortedPersonnel = useMemo(() => {
    return [...personnel].sort((a, b) => a.fullName.localeCompare(b.fullName));
  }, [personnel]);

  const [dialogOpen, setDialogOpen] = useState(false);
  const [editing, setEditing] = useState<Personnel | null>(null);
  const [deactivateTarget, setDeactivateTarget] = useState<Personnel | null>(
    null
  );

  if (query.isLoading) {
    return <div className="text-muted-foreground">Henter Personale...</div>;
  }

  if (query.isError) {
    return <div className="text-destructive">Kunne ikke hente personale.</div>;
  }

  const handleSubmit = (payload: PersonnelPayload) => {
    if (editing) {
      updateMutation.mutate({ id: editing.id, payload });
      console.log("Update personnel", editing.id, payload);
    } else {
      createMutation.mutate(payload);
      console.log("Create personnel", payload);
    }
    setDialogOpen(false);
    setEditing(null);
  };

  const handleDeactivate = () => {
    if (deactivateTarget) {
      deactivateMutation.mutate(deactivateTarget.id);
      console.log("Deactivate personnel", deactivateTarget.id);
    }
    setDeactivateTarget(null);
  };

  return (
    <section className="space-y-6">
      <div className="flex items-center justify-between">
        <h2 className="text-2xl font-semibold">Personale</h2>
        <Dialog open={dialogOpen} onOpenChange={setDialogOpen}>
          <DialogTrigger asChild>
            <Button onClick={() => setEditing(null)}>Ny medarbejder</Button>
          </DialogTrigger>
          <DialogContent>
            <DialogHeader>
              <DialogTitle>
                {editing ? "Redigér medarbejder" : "Ny medarbejder"}
              </DialogTitle>
              <DialogDescription>Udfyld felterne og klik Gem</DialogDescription>
            </DialogHeader>
            <PersonnelForm
              initialData={editing}
              groups={groups}
              onSubmit={handleSubmit}
              onCancel={() => setDialogOpen(false)}
            />
          </DialogContent>
        </Dialog>
      </div>

      <PersonnelTable
        items={sortedPersonnel}
        onEdit={(person) => {
          setEditing(person);
          setDialogOpen(true);
        }}
        onDeactivate={(person) => setDeactivateTarget(person)}
      />

      <AlertDialog
        open={!!deactivateTarget}
        onOpenChange={(open) => !open && setDeactivateTarget(null)}
      >
        <AlertDialogContent>
          <AlertDialogHeader>
            <AlertDialogTitle>Bekræft deaktivering</AlertDialogTitle>
            <AlertDialogDescription>
              Er du sikker på, at du vil deaktivere {deactivateTarget?.fullName}
              ?
            </AlertDialogDescription>
          </AlertDialogHeader>
          <AlertDialogFooter>
            <AlertDialogCancel>Annuller</AlertDialogCancel>
            <AlertDialogAction onClick={handleDeactivate}>
              Bekræft
            </AlertDialogAction>
          </AlertDialogFooter>
        </AlertDialogContent>
      </AlertDialog>
    </section>
  );
}
