import axios from "axios";

export const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL ?? "http://localhost:5166",
  headers: { "Content-Type": "application/json" },
  timeout: 10000, // 10 seconds
  // withCredentials: true,
});

export const get = <T>(url: string, params?: Record<string, unknown>) =>
  api.get<T>(url, { params }).then((res) => res.data);

// Request body type B defaults to unknown
export const post = <T, B = unknown>(url: string, body: B) =>
  api.post<T>(url, body).then((res) => res.data);

export const put = <T, B = unknown>(url: string, body: B) =>
  api.put<T>(url, body).then((res) => res.data);

export const del = <T>(url: string) => api.delete<T>(url).then((res) => res.data);
