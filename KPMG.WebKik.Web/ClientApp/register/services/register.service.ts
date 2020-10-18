import { Injectable } from '@angular/core';

import { RegisterListDto } from '../models/register-list.model';
import { RegisterType } from '../models/register-type.model';
import { KeyValue } from '../../shared/models/key-value.model';
import { Year } from '../../shared/models/year.model';

import { BaseService } from '../../shared/base.service';

@Injectable()
export class RegisterService {
    private apiUrl: string = 'api/register/';

    constructor(private baseService: BaseService) {
    }

    getRegistersByCompanyIdAndYear(companyId: number, year: number): Promise<Array<RegisterListDto>> {
        return this.baseService.get<Array<RegisterListDto>>(this.apiUrl + 'company/' + companyId + '?year=' + year);
    }

    getRegisterTypes(): Promise<Array<KeyValue<RegisterType>>> {
        return this.baseService.get<Array<KeyValue<RegisterType>>>(this.apiUrl + 'types');
    }

    getYears(): Promise<Array<number>> {
        return this.baseService.get<Array<number>>(this.apiUrl + 'years');
    }
}