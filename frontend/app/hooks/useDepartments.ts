import { useQuery } from "@tanstack/react-query";
import { getDepartments } from "@/services/departmentService";

const queryKey = ["departments"];

export function useDepartments() {
  return useQuery({
    queryKey,
    queryFn: getDepartments,
  });
}
