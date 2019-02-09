import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from "@angular/router";
import { Poll } from "../models/poll.model";
import {PollService} from "../services/poll.service";

@Component({
  selector: 'poll-admin',
  templateUrl: './poll-admin.component.html',
  styleUrls: ['./poll-admin.component.scss']
})
export class PollAdminComponent implements OnInit {
  poll: Poll;

  constructor(private currentRoute: ActivatedRoute, private pollService: PollService) { }

  ngOnInit() {
    this.currentRoute.paramMap.subscribe(routeData => {
      this.pollService.get(routeData.get('pollId')).subscribe((poll) => {
        this.poll = poll;
      });
    });
  }

}
