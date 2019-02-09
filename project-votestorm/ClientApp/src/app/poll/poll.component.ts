import {Component, OnInit} from '@angular/core';
import {PollService} from "../services/poll.service";
import {Poll} from "../models/poll.model";
import {ActivatedRoute} from "@angular/router";
import {IdentityService} from "../services/identity.service";

@Component({
  selector: 'poll',
  templateUrl: './poll.component.html'
})
export class PollComponent implements OnInit {
  poll: Poll;
  isLoading: boolean;

  constructor(private pollService: PollService,
              private identityService: IdentityService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.isLoading = true;

    this.route.params.subscribe(params => {
        this.pollService.get(params['pollId']).subscribe(poll => {
          this.poll = poll;
          this.isLoading = false;
        });
      });
  }

  isAdmin() {
    return this.poll.identity === this.identityService.get();
  }

  isVoter() {
    return !this.isAdmin();
  }
}
