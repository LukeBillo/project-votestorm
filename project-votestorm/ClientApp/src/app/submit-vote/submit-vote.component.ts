import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { VoteService } from '../services/vote.service';
import { PollService } from '../services/poll.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Poll } from '../models/poll.model';
import { PluralityVote } from '../models/vote.model';
import { Observable } from 'rxjs';
import { IdentityService } from '../services/identity.service';

@Component({
  selector: 'submit-vote',
  templateUrl: './submit-vote.component.html',
  styleUrls: ['./submit-vote.component.scss'],
})
export class SubmitVoteComponent {
  pollID: string;
  options: Array<string>;
  poll: Poll;
  hasVoted: boolean;

  voteForm: FormGroup = this.formBuilder.group({ options: ['1', Validators.required] });

  constructor(private formBuilder: FormBuilder, private voteService: VoteService,
    pollService: PollService, private router: Router,
    activatedRoute: ActivatedRoute, private identityService: IdentityService) {

    activatedRoute.params.subscribe(params => {
      this.pollID = params['pollId'];
    });

    voteService.checkHasVoted(this.pollID, this.identityService.get()).subscribe(hasVoted => {
      this.hasVoted = hasVoted;

      if (hasVoted) {
        return;
      }

      pollService.get(this.pollID).subscribe(poll => {
        this.poll = poll;
        this.options = poll.options;
      });
    });
  }
  public onSubmit() {
    const vote: PluralityVote = {
      identity: this.identityService.get(),
      pollId: this.pollID,
      selectionIndex: this.voteForm.controls.options.value
    };

    this.voteService.submit(vote).subscribe(_ => {
      this.hasVoted = true;
    });
  }
}
