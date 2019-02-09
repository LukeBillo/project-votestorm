import { Component, OnInit, Input } from '@angular/core';
import { Poll } from '../models/poll.model';
import { BehaviorSubject } from 'rxjs';
import { ResultsService } from '../services/results.service';

@Component({
  selector: 'app-poll-results',
  templateUrl: './poll-results.component.html',
  styleUrls: ['./poll-results.component.scss']
})
export class PollResultsComponent implements OnInit {
  xAxisLabel: string;
  yAxisLabel = 'votes';
  private _poll: BehaviorSubject<Poll>;

  @Input('poll') set poll(value: Poll) {
    this.xAxisLabel = value.prompt;
    this._poll.next(value);
  }

  get poll(): Poll {
    return this._poll.value;
  }

  constructor(private resultsService: ResultsService) {
    this._poll.subscribe(poll => {
    });
  }

  ngOnInit() {
  }

}
