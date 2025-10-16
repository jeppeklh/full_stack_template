import { z } from "zod";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import type { PersonnelGroup, PersonnelGroupPayload } from "@/models/personnel";

// Validation schema
const scema = z.object({
  name: z.string().min(1, "Navn er påkrævet"),
  abbreviation: z.string().min(1, "Forkortelse er påkrævet"),
});

type FormValues = z.infer<typeof scema>;

interface Props {
  initialData?: PersonnelGroup | null;
  onSubmit: (payload: PersonnelGroupPayload) => void;
  onCancel: () => void;
}

export function PersonnelGroupForm({ initialData, onSubmit, onCancel }: Props) {
  const form = useForm<FormValues>({
    resolver: zodResolver(scema),
    defaultValues: {
      name: initialData?.name ?? "",
      abbreviation: initialData?.abbreviation ?? "",
    },
  });

  return (
    <Form {...form}>
      <form
        onSubmit={form.handleSubmit((values) => onSubmit(values))}
        className="space-y-4"
      >
        <FormField
          control={form.control}
          name="name"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Navn</FormLabel>
              <FormControl>
                <Input {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="abbreviation"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Forkortelse</FormLabel>
              <FormControl>
                <Input {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <div className="flex justify-end gap-2">
          <Button variant="outline" onClick={onCancel}>
            Annuller
          </Button>
          <Button type="submit">{initialData ? "Opdater" : "Opret"}</Button>
        </div>
      </form>
    </Form>
  );
}
