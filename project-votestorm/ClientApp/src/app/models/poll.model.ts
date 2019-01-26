import { PollType } from './poll-type.enum';

export interface Poll {
  question: string;
  options: Array<string>;
  pollType: PollType;
}
