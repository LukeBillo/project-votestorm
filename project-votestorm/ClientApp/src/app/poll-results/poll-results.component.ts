import { Component, OnInit, Input } from '@angular/core';
import { Poll } from '../models/poll.model';
import { BehaviorSubject } from 'rxjs';
import { ResultsService } from '../services/results.service';
import { IdentityService } from '../services/identity.service';
import { PollResults, OptionResult } from '../models/poll-results.model';
import { NgxChartsValuePair } from '../models/ngx-charts-value-pair.model';

@Component({
  selector: 'app-poll-results',
  templateUrl: './poll-results.component.html',
  styleUrls: ['./poll-results.component.scss']
})
export class PollResultsComponent implements OnInit {
  xAxisLabel: string;
  yAxisLabel = 'votes';
  graphData: Array<NgxChartsValuePair>;
  private _poll: BehaviorSubject<Poll>;

  @Input('poll') set poll(value: Poll) {
    this.xAxisLabel = value.prompt;
    this._poll.next(value);
  }

  get poll(): Poll {
    return this._poll.value;
  }

  constructor(private resultsService: ResultsService, private identityService: IdentityService) {
    this._poll.subscribe(poll => {
      this.resultsService.get(poll.id, identityService.get()).subscribe(results => {
          this.graphData = results.results.map(result => {
            return { name: result.text, value: result.numberOfVotes };
          });
      });
    });
  }

  ngOnInit() {
  }

}
