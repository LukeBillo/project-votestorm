import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'goto-poll',
  templateUrl: './goto-poll.component.html'
})
export class GotoPollComponent {
  // TODO: add validator for min/max length and/or regex
  pollIdControl = new FormControl();

  constructor(private router: Router) { }

  goToPoll() {
    if (this.pollIdControl.value) {
      this.router.navigateByUrl('/' + this.pollIdControl.value);
    }
  }
}
