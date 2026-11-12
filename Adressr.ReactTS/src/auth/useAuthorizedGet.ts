import { useQuery } from "@tanstack/react-query";
import { get } from "../client";
import { useAuth } from "./AuthContext";

//This makes use of Tanstack Query for the AbortSignal it gives us without us needing to keep track of an AbortController.
export function useAuthorizedGet<T>(path: string | null) {
    const { token } = useAuth();

    return useQuery({
        queryKey: ["authorized", path],
        queryFn: ({ signal }) => get<T>(path!, token!, signal),
        enabled: token !== null && path !== null
    });
}

export function usePublicGet<T>(path: string | null) {
    return useQuery({
        queryKey: ["public", path],
        queryFn: ({ signal }) => get<T>(path!, undefined, signal),
        enabled: path !== null
    });
}