export interface BaseUser {
  email: string;
  password: string;
}

export interface CreateUser extends BaseUser {
  firstName: string;
  lastName: string;
}

export interface User {
  userId: string;
  role: number;
  firstName: string;
  lastName: string;
  email: string;   
  token?: string;
  sidebarStatus: number;
  created: number;
}
