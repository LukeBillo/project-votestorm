import { PollType } from './poll-type.enum';

export class PollResults {
  optionResults: Array<PluralityOptionResult>;
  pollType: PollType;
  totalVotes: number;
}

export abstract class OptionResult {
  optionText: string;
}

export class PluralityOptionResult extends OptionResult {
  numberOfVotes: number;
}
