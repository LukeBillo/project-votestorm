import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { Poll } from '../models/poll.model';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { ResultsService } from '../services/results.service';
import { IdentityService } from '../services/identity.service';
import { NgxChartsValuePair } from '../models/ngx-charts-value-pair.model';
import { timer } from 'rxjs';

@Component({
  selector: 'poll-results',
  templateUrl: './poll-results.component.html',
  styleUrls: ['./poll-results.component.scss']
})
export class PollResultsComponent implements OnDestroy {
  xAxisLabel: string;
  yAxisLabel = 'Votes';
  graphData: Array<NgxChartsValuePair>;
  chartDimensions = [0, 0];
  private _timerSubscription: Subscription;
  private _poll = new BehaviorSubject(null);

  @Input('poll') set poll(value: Poll) {
    this.xAxisLabel = value.prompt;
    this._poll.next(value);
  }

  get poll(): Poll {
    return this._poll.value;
  }

  constructor(private resultsService: ResultsService, private identityService: IdentityService) {
    const fiveSecondTimer = timer(0, 5000);
    this._timerSubscription = fiveSecondTimer.subscribe(_ => this.updateGraphData());
  }

  private updateGraphData() {
    const poll = this.poll;

    this.resultsService.get(poll.id, this.identityService.get()).subscribe(results => {
      this.graphData = results.optionResults.map(result => {
        return { name: result.optionText, value: result.numberOfVotes };
      });
    });
  }

  ngOnDestroy(): void {
    this._timerSubscription.unsubscribe();
  }
}
