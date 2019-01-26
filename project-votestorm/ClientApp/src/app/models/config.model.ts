export interface Config {
  readonly apiUrl: string;
}

export class DevConfig implements Config {
  apiUrl = 'https://localhost:5001/';
}
