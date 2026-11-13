import type { CreateWorkTaskRequest, WorkTaskResponse } from "../Task/task.types";
import type { SkillRequest, SkillResponse } from "../Skill/skill.types";

export interface ExperienceResponse {
    experienceID: number;
    companyName: string;
    roleTitle: string;
    description: string;
    isExported: boolean;
    startDate: string;
    endDate: string | null;
    skills: SkillResponse[];
    previousTasks: WorkTaskResponse[];
    currentTasks: WorkTaskResponse[];
}

export interface CreateExperienceRequest {
    companyName: string;
    roleTitle: string;
    description: string;
    startDate: string;
    endDate: string | null;
    skills: SkillRequest[];
    tasks: CreateWorkTaskRequest[];
}