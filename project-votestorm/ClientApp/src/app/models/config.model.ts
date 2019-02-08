export abstract class Config {
  readonly apiUrl: string;
}

export class DevConfig implements Config {
  apiUrl = 'https://localhost:44301';
}
