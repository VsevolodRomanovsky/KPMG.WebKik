import { Injectable } from '@angular/core';

import { Register3ViewModel } from '../models/register3.model';
import { BaseService } from '../../shared/base.service';

@Injectable()
export class Register3Service {
    private apiUrl: string = 'api/register3/';

    constructor(private baseService: BaseService) {
    }

    getRegisterById(id: number): Promise<Register3ViewModel> {
        return this.baseService.get<Register3ViewModel>(this.apiUrl + id);
    }

    createRegister(register: Register3ViewModel): Promise<Register3ViewModel> {
        return this.baseService.post(this.apiUrl, register);
    }

    updateRegister(register: Register3ViewModel): Promise<any> {
        return this.baseService.put(this.apiUrl, register);
    }

    calculateRegister(register: Register3ViewModel): Promise<Register3ViewModel> {
        return this.baseService.post(this.apiUrl + 'calculate', register);
    }
}