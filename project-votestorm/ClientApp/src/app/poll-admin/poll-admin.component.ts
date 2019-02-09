import {Component, Input, OnInit} from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { Poll } from "../models/poll.model";
import {PollService} from "../services/poll.service";
import {IdentityService} from "../services/identity.service";

@Component({
  selector: 'poll-admin',
  templateUrl: './poll-admin.component.html'
})
export class PollAdminComponent implements OnInit {
  @Input('poll') poll: Poll;

  constructor(private currentRoute: ActivatedRoute, private identityService: IdentityService,
              private pollService: PollService) { }

  ngOnInit() { }

  onClosePoll() {
    this.pollService.close(this.poll.id, this.identityService.get()).subscribe(() => {
      this.poll.isActive = false;
    });
  }
}
