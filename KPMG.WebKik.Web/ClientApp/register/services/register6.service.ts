import { Injectable } from '@angular/core';

import { Register6ViewModel } from '../models/register6.model';
import { BaseService } from '../../shared/base.service';

@Injectable()
export class Register6Service {
    private apiUrl: string = 'api/register6/';

    constructor(private baseService: BaseService) {
    }

    getRegisterById(id: number): Promise<Register6ViewModel> {
        return this.baseService.get<Register6ViewModel>(this.apiUrl + id);
    }

    createRegister(register: Register6ViewModel): Promise<Register6ViewModel> {
        return this.baseService.post(this.apiUrl, register);
    }

    updateRegister(register: Register6ViewModel): Promise<any> {
        return this.baseService.put(this.apiUrl, register);
    }

    calculateRegister(register: Register6ViewModel): Promise<Register6ViewModel> {
        return this.baseService.post(this.apiUrl + 'calculate', register);
    }
}