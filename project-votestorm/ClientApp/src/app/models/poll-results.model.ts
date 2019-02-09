import { PollType } from './poll-type.enum';

export class PollResults {
  optionResults: Array<OptionResult>;
  pollType: PollType;
  totalVotes: number;
}

export class OptionResult {
  optionText: string;
  numberOfVotes: number;
}
