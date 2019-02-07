import { Component } from '@angular/core';
import { FormBuilder, Validators, FormControl } from '@angular/forms';
import { Poll } from '../models/poll.model';
import { PollType } from '../models/poll-type.enum';
import { PollService } from '../services/poll.service';
import { Router } from '@angular/router';
import { IdentityService } from '../services/identity.service';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'create-poll',
  templateUrl: './create-poll.component.html',
  styleUrls: ['./create-poll.component.scss'],
})
export class CreatePollComponent {
  pollForm = this.formBuilder.group({
    prompt: ['', Validators.required]
  });

  options: Array<number> = [0, 1];

  constructor(private formBuilder: FormBuilder,
     private pollService: PollService, private router: Router,
     private identityService: IdentityService) {
    this.pollForm.addControl('option0', new FormControl(''));
    this.pollForm.addControl('option1', new FormControl(''));
  }

  createNewOption() {
    const optionControlName = `option${this.options.length}`;
    this.pollForm.addControl(optionControlName, new FormControl());
    console.log('thing');
    this.options.push(this.options.length);
  }

  onSubmit() {
    console.log('asdfasdfasdf');
    const poll = this.constructPoll();

    this.pollService.create(poll).subscribe(_ => {
      this.router.navigate(['/']);
    });
  }

  private constructPoll(): Poll {
    const form = this.pollForm.controls;

    const prompt = form.prompt.value;
    const options = [];
    const pollType: PollType = PollType.Plurality;
    const adminID: string = this.identityService.get();

    for (let i = 0; i < this.options.length; i++) {
      options.push(form[`option${i}`].value);
    }
    console.log('asdfasdfasdfasd');
    console.log(adminID);
    console.log(pollType);
    return { prompt: prompt, options, pollType, adminID };
  }
}
