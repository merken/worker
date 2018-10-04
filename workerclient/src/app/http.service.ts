import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable()
export class HttpService {
    constructor(
        private http: HttpClient
    ) { }

    public makeRequest(method, path, options: any = {}) {
        if (options.body) {
            options.body = JSON.stringify(options.body);
        }

        return this.http
            .request(method, `https://localhost:5001${path}`, options)
            .pipe(
                map(resp => resp || []),
                catchError(errResp => Observable.throw(errResp.error)));
    }
}
