import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { Poll } from "../models/poll.model";
import {PollService} from "../services/poll.service";
import {IdentityService} from "../services/identity.service";

@Component({
  selector: 'poll-admin',
  templateUrl: './poll-admin.component.html'
})
export class PollAdminComponent implements OnInit {
  pollId: string;
  poll: Poll;

  constructor(private currentRoute: ActivatedRoute, private identityService: IdentityService,
              private pollService: PollService) { }

  ngOnInit() {
    this.currentRoute.paramMap.subscribe(routeData => {
      this.pollId = routeData.get('pollId');

      this.pollService.get(this.pollId).subscribe((poll) => {
        this.poll = poll;
      });
    });
  }

  onClosePoll() {
    this.pollService.close(this.pollId, this.identityService.get()).subscribe(() => {
      this.poll.isActive = false;
    });
  }
}
