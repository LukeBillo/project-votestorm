import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'poll',
  templateUrl: './poll.component.html'
})
export class PollComponent {
  pollId: string;

  constructor(private currentRoute: ActivatedRoute) { }

  ngOnInit() {
    this.pollId = this.currentRoute.snapshot.paramMap.get("pollId");
  }
}
