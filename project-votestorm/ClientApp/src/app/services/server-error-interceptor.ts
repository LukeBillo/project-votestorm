import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from "@angular/common/http";
import {Observable} from "rxjs";
import {Injectable} from "@angular/core";
import {Router} from "@angular/router";

@Injectable()
export class ServerErrorInterceptor implements HttpInterceptor {
  constructor(private route: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let requestObservable = next.handle(req);

    requestObservable.subscribe(null, error => {
      if (error.status >= 500) {
        this.route.navigateByUrl('/oops')
      }
    });

    return requestObservable;
  }
}
