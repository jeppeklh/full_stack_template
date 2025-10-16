import type { ReactNode } from "react";

interface AppShellProps {
  children: ReactNode;
}

export function AppShell({ children }: AppShellProps) {
  return (
    <div className="flex min-h-screen bg-muted/10">
      <aside className="w-64 border-r bg-background p-4">
        <h1 className="mb-8 text-xl font-semibold">Vagtplan</h1>
        <nav className="space-y-2 text-sm text-muted-foreground">
          <p>Navigation her!</p>
        </nav>
      </aside>
      <main className="flex-1 p-6">{children}</main>
    </div>
  );
}
