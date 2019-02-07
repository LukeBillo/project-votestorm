import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Poll } from '../models/poll.model';
import { DevConfig, Config } from '../models/config.model';
import { CreatedResponse } from '../models/created-response.model';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PollService {
  // TODO: provide this via factory in app.module
  config: Config = new DevConfig();

  constructor(private http: HttpClient) {}

  create(poll: Poll): Observable<CreatedResponse<Poll>> {
    return this.http.post(`${this.config.apiUrl}/api/poll`, poll, { observe: 'response' }).pipe(
      map(response => {
          const location = response.headers.get('location');
          const createdPoll = response.body as Poll;

          return new CreatedResponse<Poll>(location, createdPoll);
       })
    );
  }

  get(id: string): Observable<Poll> {
    return this.http.get<Poll>(`${this.config.apiUrl}/api/poll/${id}`);
  }
}
