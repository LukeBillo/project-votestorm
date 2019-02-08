import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DevConfig, Config } from '../models/config.model';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { PluralityVote } from '../models/vote.model';

@Injectable({
  providedIn: 'root'
})
export class VoteService {
  constructor(private http: HttpClient, private config: Config) { }

  submit(vote: PluralityVote): Observable<void> {
    return this.http.post(`${this.config.apiUrl}/api/poll/${vote.pollId}/vote`, vote).pipe(
      map(_ => { })
    );
  }

  checkHasVoted(pollId: string, identity: string): Observable<boolean> {
    return this.http.get<boolean>(`${this.config.apiUrl}/api/poll/${pollId}/voted?identity=${identity}`);
  }
}
