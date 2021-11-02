export enum NotificationMessageStatus {
  Information = 'info',
  Success = 'success',
  Warning = 'warning',
  Error = 'error',
}

export interface NotificationMessage {
  message: string;
  status: NotificationMessageStatus;
}
