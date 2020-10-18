import { Injectable } from '@angular/core';

import { Register7ViewModel } from '../models/register7.model';
import { BaseService } from '../../shared/base.service';

@Injectable()
export class Register7Service {
    private apiUrl: string = 'api/register7/';

    constructor(private baseService: BaseService) {
    }

    getRegisterById(id: number): Promise<Register7ViewModel> {
        return this.baseService.get<Register7ViewModel>(this.apiUrl + id);
    }

    createRegister(register: Register7ViewModel): Promise<Register7ViewModel> {
        return this.baseService.post(this.apiUrl, register);
    }

    updateRegister(register: Register7ViewModel): Promise<any>{
        return this.baseService.put(this.apiUrl, register);
    }

    calculateRegister(register: Register7ViewModel): Promise<Register7ViewModel> {
        return this.baseService.post(this.apiUrl + 'calculate', register);
    }
}