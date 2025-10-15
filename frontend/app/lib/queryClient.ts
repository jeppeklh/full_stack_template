import { QueryClient } from "@tanstack/react-query";

export const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      refetchOnWindowFocus: false, // disable refetch on window focus
      retry: 1, 
      staleTime: 1000 * 60, // 60 seconds
    },
  },
});