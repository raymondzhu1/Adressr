import { useAuth } from "../../auth/AuthContext";
import { Dashboard } from "./Dashboard";
import { LandingPage } from "./LandingPage";

export function HomePage() {
    const { isAuthenticated } = useAuth();
    return isAuthenticated ? <Dashboard /> : <LandingPage />;
}