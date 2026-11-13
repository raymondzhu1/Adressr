export interface DashboardTaskMiniResponse {
    workTaskID: number;
    description: string;
    isDone: boolean;
    subTasks: DashboardSubTaskResponse[];
}

export interface DashboardExpTaskResponse {
    experienceID: number;
    companyName: string;
    roleTitle: string;
    openTaskCount: number;
    tasks: DashboardTaskMiniResponse[];
}

export interface DashboardSubTaskResponse {
    workTaskID: number;
    description: string;
    isDone: boolean;
}

export interface WorkTaskResponse extends DashboardTaskMiniResponse {
    creationDate: string;
    completedDate: string | null;
    subTasks: WorkTaskResponse[];
}

export interface CreateWorkTaskRequest {
    description: string;
    isDone: boolean;
    subTasks: CreateWorkTaskRequest[];
}