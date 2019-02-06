import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { VoteService } from '../services/vote.service';
import { PollService } from '../services/poll.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Poll } from '../models/poll.model';
import { PluralityVote } from '../models/vote.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'submit-vote',
  templateUrl: './submit-vote.component.html',
  styleUrls: ['./submit-vote.component.scss'],
})
export class SubmitVoteComponent {
  pollID: string;
  options: Array<string>;
  userID: string;
  pollObservable: Observable<Poll>;
  poll: Poll;


  voteForm: FormGroup = this.formBuilder.group({ options: ['1', Validators.required] });

  constructor(private formBuilder: FormBuilder, private voteService: VoteService,
    private pollService: PollService, private router: Router,
    private activatedRoute: ActivatedRoute) {

    activatedRoute.params.subscribe(params => {
    this.pollID = params['pollId'];
    });

    this.pollObservable = pollService.get(this.pollID);
    this.pollObservable.subscribe(poll => {

      this.poll = poll;
      this.options = poll.options;

      console.log(poll);
      // let controlNames = {};

      // for (let o = 0; o < this.poll.options.length; o++)
      // {
      //  controlNames["option" + o] = ['', Validators.required];
      // }

     // this.voteForm = formBuilder.group({ options: ['1', Validators.required] });

     // console.log(controlNames);
    });
  }
  public onSubmit() {
   const vote: PluralityVote = new PluralityVote();
    vote.Identity = 'changeme';
    vote.pollId = this.pollID;

    vote.selectionIndex = this.voteForm.controls.options.value;

    this.voteService.submit(vote).subscribe(_ => {
      this.router.navigateByUrl('/');
    });
    // this.router.navigateByUrl('/');
  }
}
