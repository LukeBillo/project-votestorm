import { PollType } from './poll-type.enum';

export class Poll {
  prompt: string;
  options: Array<string>;
  pollType: PollType;
  identity: string;
  isActive: boolean;

  constructor(prompt, options, type, ownerIdentity) {
    this.prompt = prompt;
    this.options = options;
    this.pollType = type;
    this.identity = ownerIdentity;
  }
}
