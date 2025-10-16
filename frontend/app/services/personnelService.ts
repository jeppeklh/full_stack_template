import { del, get, post, put } from "@/lib/apiClient";
import type { Personnel, PersonnelPayload } from "@/models/personnel";

const baseUrl = "/api/users";

// GET /api/users?onlyActive=true|false
export const getPersonnel = (onlyActive = false) =>
  get<Personnel[]>(baseUrl, { onlyActive });

// GET /api/users/{userId}
export const getPersonnelById = (userId: string) =>
  get<Personnel>(`${baseUrl}/${userId}`);

// POST /api/users
export const createPersonnel = (payload: PersonnelPayload) =>
  post<void, PersonnelPayload>(baseUrl, payload);

// PUT /api/users/{userId}
export const updatePersonnel = (userId: string, payload: PersonnelPayload) =>
  put<void, PersonnelPayload>(`${baseUrl}/${userId}`, payload);

// DELETE /api/users/{userId} (makes user inactive)
export const deletePersonnel = (userId: string) =>
  del<void>(`${baseUrl}/${userId}`);
