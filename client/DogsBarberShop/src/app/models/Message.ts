export enum MessageStatus {
  Success,
  Warning,
  Error,
}

export class Message {
  constructor(public messages: string[], public status: MessageStatus) {}
}
