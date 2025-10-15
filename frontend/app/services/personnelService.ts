import { api } from "@/lib/apiClient";
import type { Personnel, PersonnelPayload } from "@/models/personnel";

const baseUrl = "/api/users";

// GET /api/users?onlyActive=true|false
export async function getPersonnel(onlyActive = false): Promise<Personnel[]> {
  const response = await api.get<Personnel[]>(baseUrl, {
    params: { onlyActive },
  });
  return response.data;
}

// GET /api/users/{userId}
export async function getPersonnelById(userId: string): Promise<Personnel> {
  const response = await api.get<Personnel>(`${baseUrl}/${userId}`);
  return response.data;
}

// POST /api/users
export async function createPersonnel(
  payload: PersonnelPayload
): Promise<void> {
  await api.post<void>(baseUrl, payload);
}

// PUT /api/users/{userId}
export async function updatePersonnel(
  userId: string,
  payload: PersonnelPayload
): Promise<void> {
  await api.put<void>(`${baseUrl}/${userId}`, payload);
}

// DELETE /api/users/{userId} (makes user inactive)
export async function deletePersonnel(userId: string): Promise<void> {
  await api.delete(`${baseUrl}/${userId}`);
}
