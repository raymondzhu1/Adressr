export interface RegisterUserRequest {
    username: string;
    email: string;
    password: string;
    name: string;
    education?: string;
}

export interface LoginRequest {
    identifier: string;
    password: string;
}

export interface LoginResponse {
    token: string;
}

export interface UserResponse {
    userID: number;
    username: string;
    email: string;
}