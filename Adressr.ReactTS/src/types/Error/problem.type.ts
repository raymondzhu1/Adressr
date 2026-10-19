export interface ProblemDetails {
    status: number;
    title: string;
    errors?: Record<string, string[]>; //Handles ValidationProblemDetails from .NET API in case of 400 error for validation issues on API side.
}