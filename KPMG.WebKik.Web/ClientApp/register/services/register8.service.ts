import { Injectable } from '@angular/core';

import { Register8ViewModel } from '../models/register8.model';
import { BaseService } from '../../shared/base.service';
import { Http, RequestOptions, Request, RequestMethod, Headers } from '@angular/http';

@Injectable()
export class Register8Service {
    private apiUrl: string = 'api/register8/';

	constructor(private baseService: BaseService, private http: Http) {
    }

	getRegisterById(id: number): Promise<Register8ViewModel> {
		return this.baseService.get<Register8ViewModel>(this.apiUrl + id);
    }

	createRegister(register: Register8ViewModel): Promise<Register8ViewModel> {
		return this.http.post(this.apiUrl, register).toPromise();
    }

    updateRegister(register: Register8ViewModel): Promise<any>{
		return this.http.post(this.apiUrl + 'edit', register).toPromise();
    }

    calculateRegister(register: Register8ViewModel): Promise<Register8ViewModel> {
        return this.baseService.post(this.apiUrl + 'calculate', register);
    }
}