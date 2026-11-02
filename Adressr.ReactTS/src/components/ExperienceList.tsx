import { ExperienceArea } from "./ExperienceArea";
import { TaskAreaList } from "./TaskAreaList";
import type { ExperienceResponse } from "../types/Experience/experience.types";

//This component will be used for the profile page and experience manager page to list experiences.

export function ExperienceList({ experiences }: { experiences: ExperienceResponse[] }) {
    return(
        <ul className="experience-list">
            {experiences.map((experience) => (
                <li key={experience.experienceID}>
                    <ExperienceArea companyName={experience.companyName} roleTitle={experience.roleTitle} description={experience.description}>
                        <TaskAreaList tasks={[...experience.currentTasks, ...experience.previousTasks]} />
                    </ExperienceArea>
                </li>
            ))}
        </ul>
    );
}