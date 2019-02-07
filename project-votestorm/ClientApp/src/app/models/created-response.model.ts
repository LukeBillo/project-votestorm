export class CreatedResponse<T> {
  public constructor(location: string, resource: T) {
    this.location = location;
    this.resource = resource;
  }

  location: string;
  resource: T;
}
