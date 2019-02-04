import { PollType } from './poll-type.enum';

export interface Poll {
  prompt: string;
  options: Array<string>;
  pollType: PollType;
}
