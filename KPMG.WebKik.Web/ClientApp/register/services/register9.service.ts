import { Injectable } from '@angular/core';

import { Register9ViewModel } from '../models/register9.model';
import { Register9DataViewModel } from '../models/register9data.model';
import { BaseService } from '../../shared/base.service';
import { Http, RequestOptions, Request, RequestMethod, Headers } from '@angular/http';

@Injectable()
export class Register9Service {
    private apiUrl: string = 'api/register9/';

	constructor(private baseService: BaseService, private http: Http) {
    }

	getRegisterById(id: number): Promise<Register9ViewModel> {
		return this.baseService.get<Register9ViewModel>(this.apiUrl + id);
    }

	createRegister(register: Register9ViewModel): Promise<Register9ViewModel> {
		return this.http.post(this.apiUrl, register).toPromise().then(response => response.json() as Register9ViewModel);
	}

	createRegisterData(registerData: Register9DataViewModel): Promise<Register9DataViewModel> {
		return this.http.post(this.apiUrl + 'createRegisterData', registerData).toPromise();
	}

	deleteRegisterData(registerData: Register9DataViewModel): Promise<Register9DataViewModel> {
		return this.http.post(this.apiUrl + 'deleteRegisterData', registerData).toPromise();
	}

	updateRegisterData(registerData: Register9DataViewModel): Promise<any>{
		return this.http.post(this.apiUrl + 'editRegisterData', registerData).toPromise();
	}

	updateRegister(register: Register9ViewModel): Promise<any> {
		return this.http.post(this.apiUrl + 'edit', register).toPromise();
	}

	calculateRegister(register: Register9ViewModel): Promise<Register9ViewModel> {
        return this.baseService.post(this.apiUrl + 'calculate', register);
    }
}