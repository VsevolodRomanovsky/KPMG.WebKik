import { Injectable } from '@angular/core';

import { Register4ViewModel } from '../models/register4.model';
import { BaseService } from '../../shared/base.service';

@Injectable()
export class Register4Service {
    private apiUrl: string = 'api/register4/';

    constructor(private baseService: BaseService) {
    }

    getRegisterById(id: number): Promise<Register4ViewModel> {
        return this.baseService.get<Register4ViewModel>(this.apiUrl + id);
    }

    createRegister(register: Register4ViewModel): Promise<Register4ViewModel> {
        return this.baseService.post(this.apiUrl, register);
    }

    updateRegister(register: Register4ViewModel): Promise<any> {
        return this.baseService.put(this.apiUrl, register);
    }

    calculateRegister(register: Register4ViewModel): Promise<Register4ViewModel> {
        return this.baseService.post(this.apiUrl + 'calculate', register);
    }
}