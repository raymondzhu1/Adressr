import { Link } from "react-router";
import { ExperienceArea } from "../../../components/ExperienceArea";
import { TaskAreaList } from "../../../components/TaskAreaList";
import type { DashboardExpTaskResponse } from "../../../types/Task/task.types";
import { DataPanel } from "./DataPanel";

export function TasksPanel() {
    return(
        <DataPanel<DashboardExpTaskResponse[]>
            title="Current Tasks"
            section="Tasks"
            path="/api/worktask/dashboard"
            action={<Link className="panel__action" to="/experiencemanager">Experience Manager</Link>}
            isEmpty={(experiences) => experiences.length === 0}
            empty={<p className="panel__empty">Dang, you got nothing to do. Try adding some tasks to your current experience in the experience manager.</p>}
        >
            {(experiences) => (
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
                            {/* <h3 className="task-experience__title">
                                {experience.roleTitle} <span className="task-experience__company">at {experience.companyName}</span>
                            </h3>
                            <ul className="tasks">
                                {experience.tasks.map((task) => (
                                    <li key={task.workTaskID} className="task">
                                        <span>
                                            {task.description}
                                        </span>
                                        {task.subTasksTotal > 0 && (
                                            <span className="task__progress">
                                                {task.subTasksDone} of {task.subTasksTotal} sub tasks dones.
                                            </span>
                                        )}
                                    </li>
                                ))}
                            </ul>
                            {experience.openTaskCount > experience.tasks.length && (
                                <Link className="task-experience__more" to="/experiencemanager">
                                    Experience Manager
                                </Link>
                            )} */}
                        </li>
                    ))}
                </ul>
            )}
        </DataPanel>
    );
}