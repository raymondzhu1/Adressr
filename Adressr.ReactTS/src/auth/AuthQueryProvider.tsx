import { QueryCache, QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { useEffect, useState, type ReactNode } from "react";
import { ApiError } from "../client";
import { useAuth } from "./AuthContext";

export function AuthQueryProvider({ children }: {children: ReactNode}){
    const { token, signOut } = useAuth();

    const [queryClient] = useState(
        () => 
            new QueryClient({
                queryCache: new QueryCache({
                    onError: (error) => {
                        if(error instanceof ApiError && error.status === 401){
                            signOut();
                        }
                    }
                }),
                defaultOptions: {
                    queries: {
                        retry: (failureCount, error) =>
                            failureCount < 2 && !(error instanceof ApiError && error.status < 500)
                    }
                }
            })
    );

    useEffect(() => {
        if(token === null){
            queryClient.clear();
        }
    }, [token, queryClient]);

    return(
        <QueryClientProvider client={queryClient}>
            {children}
        </QueryClientProvider>
    );
}