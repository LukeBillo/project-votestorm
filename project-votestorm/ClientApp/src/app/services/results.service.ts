import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PollResults } from '../models/poll-results.model';
import { Config } from '../models/config.model';

@Injectable({
  providedIn: 'root'
})
export class ResultsService {
  constructor(private http: HttpClient, private config: Config) {}

  public get(pollId: string, identity: string): Observable<PollResults> {
    return this.http.get<PollResults>(`${this.config.apiUrl}/api/poll/${pollId}/results?adminIdentity=${identity}`);
  }
}
