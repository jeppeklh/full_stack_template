import { Button } from "@/components/ui/button";
import { Badge } from "@/components/ui/badge";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { UserStatus, type Personnel } from "@/models/personnel";

interface Props {
  items: Personnel[];
  onEdit: (item: Personnel) => void;
  onDeactivate: (item: Personnel) => void;
}

export function PersonnelTable({ items, onEdit, onDeactivate }: Props) {
  return (
    <div className="rounded-md border bg-card">
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead>Initialer</TableHead>
            <TableHead>Navn</TableHead>
            <TableHead>Email</TableHead>
            <TableHead>Personalegruppe</TableHead>
            <TableHead>Status</TableHead>
            <TableHead className="text-right">Handlinger</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {items.map((person) => (
            <TableRow key={person.id}>
              <TableCell>{person.initials}</TableCell>
              <TableCell>{person.fullName}</TableCell>
              <TableCell>{person.email}</TableCell>
              <TableCell>{person.doctorTypeName ?? "Ingen"}</TableCell>
              <TableCell>
                <Badge
                  variant={
                    person.userStatus === UserStatus.Active
                      ? "default"
                      : "secondary"
                  }
                >
                  {person.userStatus === UserStatus.Active
                    ? "Aktiv"
                    : "Inaktiv"}
                </Badge>
              </TableCell>
              <TableCell className="flex justify-end gap-2">
                <Button
                  size="sm"
                  variant="outline"
                  onClick={() => onEdit(person)}
                >
                  Rediger
                </Button>
                <Button
                  size="sm"
                  variant="destructive"
                  onClick={() => onDeactivate(person)}
                >
                  Deaktiver
                </Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </div>
  );
}
