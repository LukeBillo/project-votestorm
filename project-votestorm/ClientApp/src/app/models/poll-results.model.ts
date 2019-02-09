export class PollResults {
  results: Array<OptionResult>;
  totalVotes: number;
}

export class OptionResult {
  text: string;
  numberOfVotes: number;
}
