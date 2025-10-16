import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import {
  createPersonnel,
  deletePersonnel,
  getPersonnel,
  getPersonnelById,
  updatePersonnel,
} from "@/services/personnelService";
import type { PersonnelPayload } from "@/models/personnel";
import { create } from "domain";

// Hook to fetch the list of personnel
export function usePersonnelList(onlyActive = false) {
  const queryClient = useQueryClient();
  const listKey = ["personnel", { onlyActive }];

  // cache the personnel list query
  const query = useQuery({
    queryKey: listKey,
    queryFn: () => getPersonnel(onlyActive),
  });

  // Invalidate the query when a mutation occurs
  const createMutation = useMutation({
    mutationFn: createPersonnel,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["personnel"] });
    },
  });

  const updateMutation = useMutation({
    mutationFn: ({ id, payload }: { id: string; payload: PersonnelPayload }) =>
      updatePersonnel(id, payload),
    onSuccess: (_, variables) => { //  ignore the first parameter (data) -  we dont need it
      queryClient.invalidateQueries({ queryKey: ["personnel"] }); // Invalidate the personnel list query
      queryClient.invalidateQueries({ queryKey: ["personnel", variables.id] }); // Invalidate the specific personnel query
    },
  });

  const deactivateMutation = useMutation({
    mutationFn: deletePersonnel,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["personnel"] });
    },
  });

  return { query, createMutation, updateMutation, deactivateMutation };
}

// Hook to fetch details of a specific personnel by ID
export function usePersonnelDetail(id: string | undefined) {
  return useQuery({
    queryKey: ["personnel", { id }],
    queryFn: () => (id ? getPersonnelById(id) : Promise.reject("missing id")),
    enabled: Boolean(id), // Only run the query if id is provided
  });
}
