import { Injectable } from '@angular/core';

import { Register5ViewModel } from '../models/register5.model';
import { BaseService } from '../../shared/base.service';

@Injectable()
export class Register5Service {
    private apiUrl: string = 'api/register5/';

    constructor(private baseService: BaseService) {
    }

    getRegisterById(id: number): Promise<Register5ViewModel> {
        return this.baseService.get<Register5ViewModel>(this.apiUrl + id);
    }

    createRegister(register: Register5ViewModel): Promise<Register5ViewModel> {
        return this.baseService.post(this.apiUrl, register);
    }

    updateRegister(register: Register5ViewModel): Promise<any> {
        return this.baseService.put(this.apiUrl, register);
    }

    calculateRegister(register: Register5ViewModel): Promise<Register5ViewModel> {
        return this.baseService.post(this.apiUrl + 'calculate', register);
    }
}