export enum MessageStatus {
  Success,
  Warning,
  Error,
}

export class Message {
  constructor(
    private _message: string | string[],
    public status: MessageStatus
  ) {}

  get messages(): string[] {
    return ([] as string[]).concat(this._message);
  }
}
