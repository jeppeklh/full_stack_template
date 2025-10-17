import type { ReactNode } from "react";
import { Button } from "@/components/ui/button";

export function AppShell({
  activeTab,
  onTabChange,
  children,
}: {
  activeTab: "groups" | "personnel";
  onTabChange: (tab: "groups" | "personnel") => void;
  children: ReactNode;
}) {
  return (
    <div className="flex min-h-screen bg-muted/10">
      <aside className="w-64 border-r bg-background p-4">
        <h1 className="mb-8 text-xl font-semibold">Vagtplan</h1>
        <nav className="space-y-2 text-sm text-muted-foreground">
          <Button
          variant={activeTab === "groups" ? "default" : "ghost"}
            className="w-full justify-start"
            onClick={() => onTabChange("groups")}
          >
            Personalegrupper
          </Button>
            <Button
          variant={activeTab === "personnel" ? "default" : "ghost"}
            className="w-full justify-start"
            onClick={() => onTabChange("personnel")}
          >
            Personale
          </Button>
        </nav>
      </aside>
      <main className="flex-1 p-6">{children}</main>
    </div>
  );
}
