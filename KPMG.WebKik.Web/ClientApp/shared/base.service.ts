import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Response, RequestOptionsArgs, ResponseContentType } from '@angular/http';
import { Http, RequestOptions, Request, RequestMethod, Headers } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';

@Injectable()
export class BaseService {
    private headers: Headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8', 'Data-Type': 'json' });;

    constructor(private http: Http, private router: Router ) {
    }

    public getBlob(url: string, options?: RequestOptionsArgs): Promise<Blob> {
        let args = options || {};
        args.responseType = ResponseContentType.Blob;

        return this.http.get(url, args)
            .map(res => res.blob())
            .toPromise()
            .catch(this.handleError.bind(this));
    }

    public get<T>(url: string, options?: RequestOptionsArgs): Promise<T> {
        return this.http.get(url, options)
            .toPromise()
            .then(response => response.json() as T)
            .catch(this.handleError.bind(this));
    }

    public handleError(error: any) {
        if (error.status == 400) {
            switch (error.statusText) {
                case "Authentication":
                    this.router.navigate(['/error/auth']);
                    break;
                default:
                    return Promise.reject(error);
            }
            return;
        }
        return Promise.reject(error.toString());
    }

    public post<T>(url: string, body: any, options?: RequestOptionsArgs): Promise<T> {
        return this.http.post(url, body, this.mergeHeaders(options))
            .toPromise()
            .then(response => response.json() as T)
            .catch(this.handleError.bind(this));
    }

    public put(url: string, body: any, options?: RequestOptionsArgs): Promise<any> {
        return this.http.put(url, body, this.mergeHeaders(options))
            .toPromise()
            .then(response => response)
            .catch(this.handleError.bind(this));
    }

    public delete(url: string, options?: RequestOptionsArgs): Promise<any> {
        return this.http.delete(url, options)
            .toPromise()
            .then(response => response)
            .catch(this.handleError.bind(this));
    }

    private mergeHeaders(options?: RequestOptionsArgs): RequestOptionsArgs {
        let args = options || {};
        args.headers = args.headers || this.headers;
        return args;
    }
}