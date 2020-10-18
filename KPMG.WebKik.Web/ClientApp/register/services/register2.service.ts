import { Injectable } from '@angular/core';

import { Register2ViewModel } from '../models/register2.model';
import { BaseService } from '../../shared/base.service';

@Injectable()
export class Register2Service {
    private apiUrl: string = 'api/register2/';

    constructor(private baseService: BaseService) {
    }

    getRegisterById(id: number): Promise<Register2ViewModel> {
        return this.baseService.get<Register2ViewModel>(this.apiUrl + id);
    }

    createRegister(register: Register2ViewModel): Promise<Register2ViewModel> {
        return this.baseService.post(this.apiUrl, register);
    }

    updateRegister(register: Register2ViewModel): Promise<any> {
        return this.baseService.put(this.apiUrl, register);
    }

    calculateRegister(register: Register2ViewModel): Promise<Register2ViewModel> {
        return this.baseService.post(this.apiUrl + 'calculate', register);
    }
}