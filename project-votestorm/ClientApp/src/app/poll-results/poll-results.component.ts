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
export class PollResultsComponent implements OnDestroy, OnInit {
  xAxisLabel: string;
  yAxisLabel = 'Votes';
  graphData: Array<NgxChartsValuePair>;
  chartDimensions = [0, 0];
  liveUpdateTimer: Observable<number>;
  private _timerSubscription: Subscription;
  private _poll = new BehaviorSubject(null);
  private _liveUpdateEnabled: boolean;

  @Input('poll') set poll(value: Poll) {
    this.xAxisLabel = value.prompt;
    this._poll.next(value);
  }

  get poll(): Poll {
    return this._poll.value;
  }

  @Input('liveUpdateEnabled') set liveUpdateEnabled(isEnabled: boolean) {
    this._liveUpdateEnabled = isEnabled;

    if (this._liveUpdateEnabled && (!this._timerSubscription || this._timerSubscription.closed)) {
      this._timerSubscription = this.liveUpdateTimer.subscribe(_ => this.updateGraphData());
      return;
    }

    if (!this.liveUpdateEnabled) {
      this._timerSubscription.unsubscribe();
    }
  }

  constructor(private resultsService: ResultsService, private identityService: IdentityService) {
    this.liveUpdateTimer = timer(0, 5000);
  }

  ngOnInit() {
    // to make sure we populate the graph at least once
    this.updateGraphData();
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
