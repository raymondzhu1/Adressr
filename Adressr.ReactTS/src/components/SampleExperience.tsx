import type { DashboardTaskMiniResponse } from "../types/Task/task.types";
import { ExperienceArea } from "./ExperienceArea";
import { TaskAreaList } from "./TaskAreaList";

const TASKS: DashboardTaskMiniResponse[] = [
    {
        workTaskID: 1,
        description: "TASK1",
        isDone: true,
        subTasks: [
            {workTaskID: 2, description: "Hi this is an actual subtask text, and totally finished this subtask. Woo?", isDone: true},
            {workTaskID: 3, description: "SUBTASK2", isDone: true},
            {workTaskID: 4, description: "SUBTASK3", isDone: true}
        ]
    },
    {
        workTaskID: 5,
        description: "TASK2",
        isDone: true,
        subTasks: []
    },
    {
        workTaskID: 6,
        description: "TASK3",
        isDone: false,
        subTasks: [
            {workTaskID: 7, description: "SUBTASK4", isDone: false}
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