import { Injectable } from '@angular/core';
import {v4 as uuidv4} from 'uuid';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {

  get(): string {
      // check if identity exists in cookie
      const identity: string  = localStorage.getItem('identity');
      if (identity) {
        return identity;
      } else {
        // if it doesn't, genearate one from UUID
        const newIdentity = uuidv4();
        localStorage.setItem('identity', newIdentity);
        return newIdentity;
      }
  }
}
