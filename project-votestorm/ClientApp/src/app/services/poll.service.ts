import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Poll } from '../models/poll.model';
import { DevConfig, Config } from '../models/config.model';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PollService {
  // TODO: provide this via factory in app.module
  config: Config = new DevConfig();

  constructor(private http: HttpClient) {}

  createPoll(poll: Poll): Observable<void> {
    return this.http.post(`${this.config.apiUrl}/poll/create`, poll).pipe(
      map(_ => {})
    );
  }
}
