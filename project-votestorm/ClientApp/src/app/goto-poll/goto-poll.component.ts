import { Component } from '@angular/core';
import {FormControl, Validators} from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'goto-poll',
  templateUrl: './goto-poll.component.html',
  styleUrls: ['./goto-poll.component.scss']
})
export class GotoPollComponent {
  pollIdControl = new FormControl(null, [
    Validators.required,
    Validators.pattern('[A-Z1-9]{5}')
  ]);

  constructor(private router: Router) { }

  goToPoll() {
    if (this.pollIdControl.value) {
      this.router.navigateByUrl('/' + this.pollIdControl.value);
    }
  }
}
