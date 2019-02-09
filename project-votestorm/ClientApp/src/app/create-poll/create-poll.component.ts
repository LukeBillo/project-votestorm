import { Component } from '@angular/core';
import { FormBuilder, Validators, FormControl } from '@angular/forms';
import { Poll } from '../models/poll.model';
import { PollType } from '../models/poll-type.enum';
import { PollService } from '../services/poll.service';
import { Router } from '@angular/router';
import { IdentityService } from '../services/identity.service';

@Component({
  selector: 'create-poll',
  templateUrl: './create-poll.component.html',
  styleUrls: ['./create-poll.component.scss'],
})
export class CreatePollComponent {
  pollForm = this.formBuilder.group({
    prompt: ['', Validators.required]
  });
  options: Array<number> = [0, 1];
  created = false;
  createdPollId: string;

  constructor(private formBuilder: FormBuilder,
     private pollService: PollService, private router: Router,
     private identityService: IdentityService) {
    this.pollForm.addControl('option0', new FormControl(''));
    this.pollForm.addControl('option1', new FormControl(''));
  }

  createNewOption() {
    const optionControlName = `option${this.options.length}`;
    this.pollForm.addControl(optionControlName, new FormControl());
    this.options.push(this.options.length);
  }

  onSubmit() {
    const poll = this.constructPoll();

    this.pollService.create(poll).subscribe(result => {
        this.created = true;

        // last element of the object location will
        // be the poll's ID
        // location is in format 'http://localhost:5000/api/poll/abcde'
        this.createdPollId = result.location.split('/').pop();
    });
  }

  private constructPoll(): Poll {
    const form = this.pollForm.controls;

    const prompt = form.prompt.value;
    const options = [];
    const pollType: PollType = PollType.Plurality;
    const adminIdentity: string = this.identityService.get();

    for (let i = 0; i < this.options.length; i++) {
      options.push(form[`option${i}`].value);
    }

    return new Poll(prompt, options, pollType, adminIdentity);
  }
}
