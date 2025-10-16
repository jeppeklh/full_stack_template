import { del, get, post, put } from "@/lib/apiClient";
import type { PersonnelGroup, PersonnelGroupPayload } from "@/models/personnel";

const baseUrl = "/api/DoctorType";

// GET /api/DoctorType/GetAll
export const getPersonnelGroups = () =>
  get<PersonnelGroup[]>(`${baseUrl}/GetAll`);

// GET /api/DoctorType/{id}
export const getPersonnelGroupById = (id: string) =>
  get<PersonnelGroup>(`${baseUrl}/${id}`);

// POST /api/DoctorType
export const createPersonnelGroup = (payload: PersonnelGroupPayload) =>
  post<PersonnelGroup, PersonnelGroupPayload>(`${baseUrl}/Add`, payload);

// PUT /api/DoctorType/Update
// The backend's DoctorTypeController expects the entire entity (with id) in the body
// and does not include the id in the route, so we send the full object to /Update.
export const updatePersonnelGroup = (
  group: PersonnelGroup & PersonnelGroupPayload
) =>
  put<PersonnelGroup, PersonnelGroup & PersonnelGroupPayload>(
    `${baseUrl}/Update/`,
    group
  );

// DELETE /api/DoctorType/{id}
export const deletePersonnelGroup = (id: string) =>
  del<void>(`${baseUrl}/Delete/${id}`);
