export class ConfirmMessage {
  constructor(
    public confirmMessage: string,
    public confirmAction: () => void,
    public rejectAction: () => void
  ) {}
}
