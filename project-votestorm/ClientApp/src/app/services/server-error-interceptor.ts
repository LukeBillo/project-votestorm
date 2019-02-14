import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpResponse
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs/operators';

@Injectable()
export class ServerErrorInterceptor implements HttpInterceptor {
  constructor(private route: Router) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const requestObservable = next.handle(req);

    return requestObservable.pipe(
      map(httpEvent => {
        if (!(httpEvent instanceof HttpResponse)) {
          return httpEvent;
        }

        const response = httpEvent as HttpResponse<any>;

        if (response.status >= 500) {
          this.route.navigateByUrl('/oops');
        }

        return response;
      })
    );
  }
}
