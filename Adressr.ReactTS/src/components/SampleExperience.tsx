import type { DashboardTaskMiniResponse } from "../types/Task/task.types";
import { ExperienceArea } from "./ExperienceArea";
import { TaskAreaList } from "./TaskAreaList";

const TASKS: DashboardTaskMiniResponse[] = [
    {
        workTaskID: 1,
        description: "TASK1",
        isDone: true,
        subTasks: [
            {workTaskID: 2, description: "SUBTASK1", isDone: true},
            {workTaskID: 3, description: "SUBTASK2", isDone: true}
        ]
    },
    {
        workTaskID: 4,
        description: "TASK2",
        isDone: true,
        subTasks: []
    },
    {
        workTaskID: 5,
        description: "TASK3",
        isDone: false,
        subTasks: [
            {workTaskID: 6, description: "SUBTASK3", isDone: false}
        ]
    }
];

interface SampleExperienceProps {
    compact?: boolean
}

export function SampleExperience({ compact = false }: SampleExperienceProps) {
    return(
        <ExperienceArea companyName="Adressr" roleTitle="Fullstack Engineer" description="Make the whole website. Good luck.">
            <TaskAreaList tasks={compact ? [TASKS[0], TASKS[2]] : TASKS} />
        </ExperienceArea>
    );
}