import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Button } from "@/components/ui/button";

const demoGroups = [
  { id: "1", name: "Overlæge", abbreviation: "OVL" },
  { id: "2", name: "Afdelingslæge", abbreviation: "AFL" },
];

export function PersonnelGroupTable() {
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
          {demoGroups.map((group) => (
            <TableRow key={group.id}>
              <TableCell>{group.name}</TableCell>
              <TableCell>{group.abbreviation}</TableCell>
              <TableCell className="flex justify-end gap-2">
                <Button
                  size="sm"
                  variant="outline"
                  onClick={() => console.log("Edit clicked", group)}
                >
                  Rediger
                </Button>
                <Button
                  size="sm"
                  variant="destructive"
                  onClick={() => console.log("Delete clicked", group)}
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
