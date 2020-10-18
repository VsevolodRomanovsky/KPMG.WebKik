import { Injectable } from '@angular/core';

import { Register11ViewModel } from '../models/register11.model';
import { Register11DataViewModel } from '../models/register11data.model ';

import { BaseService } from '../../shared/base.service';
import { Http, RequestOptions, Request, RequestMethod, Headers } from '@angular/http';

@Injectable()
export class Register11Service {
    private apiUrl: string = 'api/register11/';

	constructor(private baseService: BaseService, private http: Http) {
    }

	getRegisterById(id: number): Promise<Register11ViewModel> {
		return this.baseService.get<Register11ViewModel>(this.apiUrl + id);
    }

	createRegister(register: Register11ViewModel): Promise<Register11ViewModel> {
		return this.http.post(this.apiUrl, register).toPromise().then(response => response.json() as Register11ViewModel);
	}

	createRegisterData(registerData: Register11DataViewModel): Promise<Register11DataViewModel> {
		return this.http.post(this.apiUrl + 'createRegisterData', registerData).toPromise();
	}

	deleteRegisterData(registerData: Register11DataViewModel): Promise<Register11DataViewModel> {
		return this.http.post(this.apiUrl + 'deleteRegisterData', registerData).toPromise();
	}

	updateRegisterData(registerData: Register11DataViewModel): Promise<any>{
		return this.http.post(this.apiUrl + 'editRegisterData', registerData).toPromise();
	}

	updateRegister(register: Register11ViewModel): Promise<any> {
		return this.http.post(this.apiUrl + 'edit', register).toPromise();
	}

	calculateRegister(register: Register11ViewModel): Promise<Register11ViewModel> {
        return this.baseService.post(this.apiUrl + 'calculate', register);
    }
}