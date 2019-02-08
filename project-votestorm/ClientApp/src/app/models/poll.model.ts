import { PollType } from './poll-type.enum';

export class Poll {
  prompt: string;
  options: Array<string>;
  pollType: PollType;
  identity: string;
}
