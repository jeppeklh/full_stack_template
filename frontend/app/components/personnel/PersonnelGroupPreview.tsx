import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";

const demoGroups = [
  { id: "1", name: "Overlæge", abbreviation: "OVL" },
  { id: "2", name: "Afdelingslæge", abbreviation: "AFL" },
];

export function PersonnelGroupPreview() {
  return (
    <Card>
      <CardHeader>
        <CardTitle>Personalegrupper</CardTitle>
      </CardHeader>
      <CardContent className="space-y-2">
        {demoGroups.map((group) => (
          <div key={group.id} className="flex justify-between text-sm">
            <span>{group.name}</span>
            <span className="text-muted-foreground">{group.abbreviation}</span>
          </div>
        ))}
      </CardContent>
    </Card>
  );
}
