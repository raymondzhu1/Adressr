import type { ProblemDetails } from "./types/Error/problem.type";

export class ApiError extends Error {
    readonly status: number;
    readonly problem: ProblemDetails | null;

    constructor(status: number, problem: ProblemDetails | null){
        super(problem?.title ?? 'Request failed (${status}).');
        this.status = status;
        this.problem = problem;
    }
}

async function readBody(response: Response): Promise<unknown> {
    const text = await response.text();
    if(!text) {
        return null;
    }
    try {
        return JSON.parse(text);
    }
    catch {
        return null;
    }
}

export async function post<TResponse>(path: string, payload:unknown): Promise<TResponse> {
    const API_BASE_URL = import.meta.env.VITE_API_BASE_URL ?? "";

    const response = await fetch(`${API_BASE_URL}${path}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload)
    });

    const body = await readBody(response);

    if(!response.ok) {
        throw new ApiError(response.status, body as ProblemDetails | null);
    }

    return body as TResponse;
}