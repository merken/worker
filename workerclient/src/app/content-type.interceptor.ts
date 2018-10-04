import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable, Injector } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class ContentTypeInterceptor implements HttpInterceptor {
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let headers = request.headers;
        const isApiRequest = this.isApiRequest(request);

        if (isApiRequest) {
            headers = this.addJsonToHeaders(request);
            request = request.clone({ headers });

            headers = this.addNoCacheToHeaders(request);
            request = request.clone({ headers });
        }
        return next.handle(request);
    }

    private isApiRequest(request: HttpRequest<any>): boolean {
        return request.url.indexOf('/api/') > 0;
    }

    private addJsonToHeaders(request: HttpRequest<any>) {
        const headers = request.headers.append('Accept', 'application/json, text/plain, */*');
        return headers.append('Content-Type', 'application/json');
    }

    private addNoCacheToHeaders(request: HttpRequest<any>) {
        const headers = request.headers.append('Cache-Control', 'no-cache');
        return request.headers.append('If-Modified-Since', '0');
    }
}
