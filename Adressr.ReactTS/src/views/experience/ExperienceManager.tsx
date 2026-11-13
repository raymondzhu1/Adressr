import { Navigate } from "react-router";
import { ExperienceList } from "../../components/ExperienceList";
import { useAuthorizedGet } from "../../auth/useAuthorizedGet";
import { useAuth } from "../../auth/AuthContext";
import type { ExperienceResponse } from "../../types/Experience/experience.types";

//I'll spruce up some CSS for this later.

export function ExperienceManager() {
    const { isAuthenticated } = useAuth();

    const query = useAuthorizedGet<ExperienceResponse[]>("/api/experience/experiencemanager");

    if(!isAuthenticated) {
        return <Navigate to="/" replace />;
    }

    if(query.isLoading) {
        <p>Loading experiences....</p>;
    }

    if(query.isError) {
        return <p>Something went wrong. Oops. Teehee.</p>;
    }

    if(!query.data || query.data.length === 0) {
        return <p>Nothing here.</p>
    }
    
    return(
        <ExperienceList experiences={query.data} variant="full" />
    );
}