import { z } from "zod";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { Switch } from "@/components/ui/switch";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Button } from "@/components/ui/button";
import {
  UserStatus,
  type Personnel,
  type PersonnelGroup,
  type PersonnelPayload,
} from "@/models/personnel";
import type { Department } from "@/models/department";

const schema = z.object({
  initials: z
    .string()
    .min(2, "Initialer skal være 2-3 store bogstaver")
    .max(3, "Initialer skal være 2-3 store bogstaver")
    .regex(/^[A-Z]{2,3}$/, "Initialer skal være 2-3 store bogstaver"),
  fullName: z
    .string()
    .min(3, "Navn skal være mindst 3 karakterer")
    .max(100, "Navn må maks være 100 karakterer"),
  email: z.email("Ugyldig emailadresse"),
  doctorTypeId: z.uuid().optional().or(z.literal("none")),
  departmentId: z.uuid().optional().or(z.literal("none")),
  active: z.boolean(),
});

type FormValues = z.infer<typeof schema>;

interface Props {
  initialData?: Personnel | null;
  groups: PersonnelGroup[];
  departments: Department[];
  onSubmit: (payload: PersonnelPayload) => void;
  onCancel: () => void;
}

export function PersonnelForm({
  initialData,
  groups,
  departments,
  onSubmit,
  onCancel,
}: Props) {
  const form = useForm<FormValues>({
    resolver: zodResolver(schema),
    defaultValues: {
      initials: initialData?.initials ?? "",
      fullName: initialData?.fullName ?? "",
      email: initialData?.email ?? "",
      doctorTypeId: initialData?.doctorTypeId ?? "none",
      departmentId: initialData?.departmentId ?? "none",
      active: initialData?.userStatus !== UserStatus.Inactive,
    },
  });

  const handleSubmit = form.handleSubmit((values) => {
    onSubmit({
      initials: values.initials,
      fullName: values.fullName,
      email: values.email,
      departmentId: values.departmentId === "none" ? null : values.departmentId,
      doctorTypeId: values.doctorTypeId === "none" ? null : values.doctorTypeId,
      userStatus: values.active ? UserStatus.Active : UserStatus.Inactive,
    });
  });

  return (
    <Form {...form}>
      <form onSubmit={handleSubmit} className="space-y-4">
        <FormField
          control={form.control}
          name="initials"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Initialer</FormLabel>
              <FormControl>
                <Input {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="fullName"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Fuldt navn</FormLabel>
              <FormControl>
                <Input {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="email"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Email</FormLabel>
              <FormControl>
                <Input type="email" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="doctorTypeId"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Personalegruppe</FormLabel>
              <Select
                value={field.value ?? "none"}
                onValueChange={(value) =>
                  field.onChange(value === "none" ? undefined : value)
                }
              >
                <FormControl>
                  <SelectTrigger>
                    <SelectValue placeholder="Vælg gruppe" />
                  </SelectTrigger>
                </FormControl>
                <SelectContent>
                  <SelectItem value="none">Ingen</SelectItem>
                  {groups.map((group) => (
                    <SelectItem key={group.id} value={group.id}>
                      {group.name}
                    </SelectItem>
                  ))}
                </SelectContent>
              </Select>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="departmentId"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Afdeling</FormLabel>
              <Select value={field.value} onValueChange={field.onChange}>
                <FormControl>
                  <SelectTrigger>
                    <SelectValue placeholder="Vælg afdeling" />
                  </SelectTrigger>
                </FormControl>
                <SelectContent>
                  <SelectItem value="none">Ingen</SelectItem>
                  {departments.map((dept) => (
                    <SelectItem key={dept.id} value={dept.id}>
                      {dept.name}
                    </SelectItem>
                  ))}
                </SelectContent>
              </Select>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="active"
          render={({ field }) => (
            <FormItem className="flex items-center justify-between rounded-md border p-3">
              <div>
                <FormLabel>Aktiv</FormLabel>
                <FormDescription>
                  Markér som aktiv for at vise medarbejderen i oversigten.
                </FormDescription>
              </div>
              <FormControl>
                <Switch
                  checked={field.value}
                  onCheckedChange={field.onChange}
                />
              </FormControl>
            </FormItem>
          )}
        />
        <div className="flex justify-end gap-2">
          <Button type="button" variant="outline" onClick={onCancel}>
            Annuller
          </Button>
          <Button type="submit">Gem</Button>
        </div>
      </form>
    </Form>
  );
}
