import { Injectable } from '@angular/core';

import { Register10ViewModel } from '../models/register10.model';
import { BaseService } from '../../shared/base.service';
import { Http, RequestOptions, Request, RequestMethod, Headers } from '@angular/http';
import { Register10SectionViewModel } from '../models/register10section.model';

@Injectable()
export class Register10Service {
    private apiUrl: string = 'api/register10/';

    constructor(private baseService: BaseService, private http: Http) {
    }

    getRegisterById(id: number): Promise<Register10ViewModel> {
        return this.baseService.get<Register10ViewModel>(this.apiUrl + id);
    }

    createRegister(register: Register10ViewModel): Promise<Register10ViewModel> {
        return this.http.post(this.apiUrl, register).toPromise().then(response => response.json() as Register10ViewModel);
    }

    updateRegister(register: Register10ViewModel): Promise<any> {
        return this.http.post(this.apiUrl + 'edit', register).toPromise();
    }

    calculateRegister(register: Register10ViewModel): Promise<Register10ViewModel> {
        return this.baseService.post(this.apiUrl + 'calculate', register);
    }

    createRegisterData(registerData: Register10SectionViewModel): Promise<Register10SectionViewModel> {
        return this.http.post(this.apiUrl + 'createRegisterData', registerData).toPromise();
    }

    updateRegisterData(registerData: Register10SectionViewModel): Promise<any> {
        return this.http.post(this.apiUrl + 'editRegisterData', registerData).toPromise();
    }

    deleteRegisterData(registerData: Register10SectionViewModel): Promise<Register10SectionViewModel> {
        return this.http.post(this.apiUrl + 'deleteRegisterData', registerData).toPromise();
    }

}