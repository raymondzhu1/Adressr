import { createContext, useContext, useState, type ReactNode } from "react";

const TOKEN_KEY = "adressr_token";

interface AuthContextValue {
    token: string | null;
    isAuthenticated: boolean;
    signIn: (token: string) => void;
    signOut: () => void;
}

const AuthContext = createContext<AuthContextValue | null>(null);

export function AuthProvider({ children }: { children: ReactNode }) {
    const [token, setToken] = useState<string | null>(() => localStorage.getItem(TOKEN_KEY));

    const signIn = (newToken: string) => {
        localStorage.setItem(TOKEN_KEY, newToken);
        setToken(newToken);
    };

    const signOut = () => {
        localStorage.removeItem(TOKEN_KEY);
        setToken(null);
    };

    return (
        <AuthContext value={{ token, isAuthenticated: token !== null, signIn, signOut }}>
            {children}
        </AuthContext>
    );
}

export function useAuth(): AuthContextValue {
    const context = useContext(AuthContext);
    if(!context) {
        throw new Error("useAuth must be used inside an AuthProvider");
    }
    return context;
}