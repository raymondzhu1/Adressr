import { post } from "../client";
import type { LoginRequest, LoginResponse, RegisterUserRequest, UserResponse } from "../types/User/user.types";

export const registerUser = (request: RegisterUserRequest) => post<UserResponse>("/api/user/register", request);

export const loginUser = (request: LoginRequest) => post<LoginResponse>("/api/user/login", request);