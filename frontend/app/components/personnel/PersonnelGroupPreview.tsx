import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";

const demoGroups = [
  { id: "1", name: "Overlæge", abbreviation: "OVL" },
  { id: "2", name: "Afdelingslæge", abbreviation: "AFL" },
];

export function PersonnelGroupPreview() {
  return (
    <Card>
      <CardHeader>
        <CardTitle>Overlæge</CardTitle>
      </CardHeader>
      <CardContent>
        <p>Abbreviation: OVL</p>
      </CardContent>
    </Card>
  );
}
