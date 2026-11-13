import { Link } from "react-router";
import { ExperienceArea } from "./ExperienceArea";
import { TaskAreaList } from "./TaskAreaList";
import type { ExperienceResponse } from "../types/Experience/experience.types";
import type { DashboardExpTaskResponse } from "../types/Task/task.types";

//This component will be used for the profile page and experience manager page to list experiences.

type ExperienceListProps = 
    | { 
        variant: "dashboard";
        experiences: DashboardExpTaskResponse[];
    }
    | {
        variant: "full";
        experiences: ExperienceResponse[];
    };

export function ExperienceList({ experiences, variant } : ExperienceListProps) { //{ experiences }: { experiences: ExperienceResponse[] }
    if(variant === "dashboard")
    {
        <ul className="task-experiences">
            {experiences.map((experience) => (
                <li key={experience.experienceID}>
                    <ExperienceArea companyName={experience.companyName} roleTitle={experience.roleTitle}>
                        <TaskAreaList tasks={experience.tasks} />
                        {experience.openTaskCount > experience.tasks.length && (
                            <Link className="task-experience__more" to="/experiencemanager">
                                { experience.openTaskCount - experience.tasks.length } more open tasks
                            </Link>
                        )}
                    </ExperienceArea>
                </li>
            ))}
        </ul>
    }

    if(variant === "full"){
        return(
            <ul className="experience-list">
                {experiences.map((experience) => (
                    <li key={experience.experienceID}>
                        <ExperienceArea companyName={experience.companyName} roleTitle={experience.roleTitle} description={experience.description} startDate={experience.startDate} endDate={experience.endDate}>
                            <TaskAreaList tasks={[...experience.currentTasks, ...experience.previousTasks]} />
                        </ExperienceArea>
                    </li>
                ))}
            </ul>
        );
    }
}