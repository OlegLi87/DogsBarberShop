export class User {
  constructor(
    public readonly id: string,
    public readonly userName: string,
    public readonly firstName: string,
    public readonly jwtToken: string
  ) {}
}
