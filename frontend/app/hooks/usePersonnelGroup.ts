import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import {
  createPersonnelGroup,
  deletePersonnelGroup,
  getPersonnelGroupById,
  getPersonnelGroups,
  updatePersonnelGroup,
} from "@/services/personnelGroupService";
import type { PersonnelGroupPayload } from "@/models/personnel";

const listKey = ["personnelGroups"];

export function usePersonnelGroupList() {
  const queryClient = useQueryClient();

  const query = useQuery({
    queryKey: listKey,
    queryFn: getPersonnelGroups,
  });

  const createMutation = useMutation({
    mutationFn: createPersonnelGroup,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: listKey });
    },
  });

  const updateMutation = useMutation({
    mutationFn: ({
      id,
      payload,
    }: {
      id: string;
      payload: PersonnelGroupPayload;
    }) => updatePersonnelGroup({ id, ...payload }),
    onSuccess: (_, variables) => {
      //  ignore the first parameter (data) -  we dont need it
      queryClient.invalidateQueries({ queryKey: listKey });
      queryClient.invalidateQueries({
        queryKey: ["personnelGroups", variables.id],
      }); // Invalidate the specific personnel group query
    },
  });

  const deleteMutation = useMutation({
    mutationFn: deletePersonnelGroup,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: listKey });
    },
  });

  return { query, createMutation, updateMutation, deleteMutation };
}

// Helper for editing forms
export function usePersonnelGroup(id: string | undefined) {
  return useQuery({
    queryKey: ["personnelGroups", id],
    queryFn: () =>
      id ? getPersonnelGroupById(id) : Promise.reject("missing id"),
    enabled: Boolean(id), // Only run the query if id is provided
  });
}
