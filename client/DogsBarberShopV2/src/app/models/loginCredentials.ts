export interface LoginCredentials {
  userName: string;
  password: string;
  confirmPassword?: string;
  email?: string;
  firstName?: string;
  emailConfirmationUrl?: string;
}
