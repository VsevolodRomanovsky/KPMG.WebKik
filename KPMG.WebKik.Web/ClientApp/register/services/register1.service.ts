import { Injectable } from '@angular/core';

import { Register1ViewModel } from '../models/register1.model';
import { BaseService } from '../../shared/base.service';

@Injectable()
export class Register1Service {
    private apiUrl: string = 'api/register1/';

    constructor(private baseService: BaseService) {
    }

    getRegisterById(id: number): Promise<Register1ViewModel> {
        return this.baseService.get<Register1ViewModel>(this.apiUrl + id);
    }

    createRegister(register: Register1ViewModel): Promise<Register1ViewModel> {
        return this.baseService.post(this.apiUrl, register);
    }

    updateRegister(register: Register1ViewModel): Promise<any> {
        return this.baseService.put(this.apiUrl, register);
    }

    calculateRegister(register: Register1ViewModel): Promise<Register1ViewModel> {
        return this.baseService.post(this.apiUrl + 'calculate', register);
    }
}