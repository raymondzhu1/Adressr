import { useQuery } from "@tanstack/react-query";
import { get } from "../client";
import { useAuth } from "./AuthContext";

export function useAuthorizedGet<T>(path: string | null) {
    const { token } = useAuth();

    return useQuery({
        queryKey: [path],
        queryFn: () => get<T>(path!, token!),
        enabled: token !== null && path !== null
    });
}