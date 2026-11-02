import type { WorkTaskResponse } from "../Task/task.types";
import type { SkillResponse } from "../Skill/skill.types";

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