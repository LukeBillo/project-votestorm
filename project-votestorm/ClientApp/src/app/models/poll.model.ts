import { PollType } from './poll-type.enum';

export class Poll {
  id: string;
  prompt: string;
  options: Array<string>;
  pollType: PollType;
  identity: string;
  isActive: boolean;

  constructor(prompt: string, options: Array<string>, type: PollType, ownerIdentity: string) {
    this.prompt = prompt;
    this.options = options;
    this.pollType = type;
    this.identity = ownerIdentity;
  }
}
