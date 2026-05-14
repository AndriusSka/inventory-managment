import { apiClient } from "./apiClient";
import type { User } from "../types/User";

export const usersApi = {
  async getAll(): Promise<User[]> {
    const response = await apiClient.get<User[]>("/users");
    return response.data;
  },
};
