export class User {
  constructor(
    public readonly userName: string,
    public readonly firstName: string,
    public readonly jwtToken: string
  ) {}
}
