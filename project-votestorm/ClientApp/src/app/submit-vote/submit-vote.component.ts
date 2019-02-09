import {Component, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { VoteService } from '../services/vote.service';
import { PollService } from "../services/poll.service";
import { Router, ActivatedRoute } from '@angular/router';
import { Poll } from '../models/poll.model';
import { PluralityVote } from '../models/vote.model';
import { IdentityService } from '../services/identity.service';

@Component({
  selector: 'submit-vote',
  templateUrl: './submit-vote.component.html',
  styleUrls: ['./submit-vote.component.scss'],
})
export class SubmitVoteComponent implements OnInit {
  options: Array<string>;
  poll: Poll;
  hasVoted: boolean;

  voteForm: FormGroup = this.formBuilder.group({ options: ['1', Validators.required] });

  constructor(private formBuilder: FormBuilder, private voteService: VoteService,
    private pollService: PollService, private router: Router, private activatedRoute: ActivatedRoute,
    private identityService: IdentityService) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      let pollId = params['pollId'];

      this.voteService.checkHasVoted(pollId, this.identityService.get()).subscribe(hasVoted => {
        this.hasVoted = hasVoted;

        if (hasVoted) {
          return;
        }

        this.pollService.get(pollId).subscribe(poll => {
          this.poll = poll;
          this.options = poll.options;
        });
      });
    });
  }

  public onSubmit() {
    const vote: PluralityVote = {
      identity: this.identityService.get(),
      pollId: this.poll.id,
      selectionIndex: this.voteForm.controls.options.value
    };

    this.voteService.submit(vote).subscribe(_ => {
      this.hasVoted = true;
    });
  }

  public returnToHome() {
    this.router.navigateByUrl('/');
  }
}
