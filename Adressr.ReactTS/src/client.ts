import type { ProblemDetails } from "./types/Error/problem.type";

export const API_BASE_URL = import.meta.env.VITE_API_BASE_URL ?? "";

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

async function request<TResponse>(path: string, init: RequestInit): Promise<TResponse> {
    const response = await fetch(`${API_BASE_URL}${path}`, init);
    const body = await readBody(response);

    if(!response.ok){
        throw new ApiError(response.status, body as ProblemDetails | null);
    }

    return body as TResponse;
}

export const post = <TResponse>(path: string, payload: unknown) =>
    request<TResponse>(path, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(payload)
    });

export const get = <TResponse>(path: string, token: string) =>
    request<TResponse>(path, {
        headers: { Authorization: `Bearer ${token}` }
    });

export const assetUrl = (location: string) =>
    /^https?:\/\//.test(location) ? location : `${API_BASE_URL}${location}`;