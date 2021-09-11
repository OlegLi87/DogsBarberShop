export class Order {
  constructor(
    public orderId: string,
    public userId: string,
    public userFirstName: string,
    public arrivalTime: Date,
    public orderTime: Date
  ) {}
}
