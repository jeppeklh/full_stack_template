import { get } from "@/lib/apiClient";
import type { Department } from "@/models/department";

const baseUrl = "/api/departments";

export const getDepartments = () => get<Department[]>(baseUrl);
