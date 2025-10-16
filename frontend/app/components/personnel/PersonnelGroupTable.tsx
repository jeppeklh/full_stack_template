import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Button } from "@/components/ui/button";
import type { PersonnelGroup } from "@/models/personnel";

interface PersonnelGroupTableProps {
  groups: PersonnelGroup[];
  onEdit: (group: PersonnelGroup) => void;
  onDelete: (group: PersonnelGroup) => void;
}

export function PersonnelGroupTable({
  groups,
  onEdit,
  onDelete,
}: PersonnelGroupTableProps) {
  return (
    <div className="rounded-md border bg-card">
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead>Navn</TableHead>
            <TableHead>Forkortelse</TableHead>
            <TableHead className="text-right">Handlinger</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {groups.map((group) => (
            <TableRow key={group.id}>
              <TableCell>{group.name}</TableCell>
              <TableCell>{group.abbreviation}</TableCell>
              <TableCell className="flex justify-end gap-2">
                <Button
                  size="sm"
                  variant="outline"
                  onClick={() => onEdit(group)}
                >
                  Rediger
                </Button>
                <Button
                  size="sm"
                  variant="destructive"
                  onClick={() => onDelete(group)}
                >
                  Slet
                </Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </div>
  );
}
