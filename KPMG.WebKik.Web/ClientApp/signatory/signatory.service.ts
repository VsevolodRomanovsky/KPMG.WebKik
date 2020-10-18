import { Injectable } from '@angular/core';

import { SignatoryViewModel } from './models/signatory.model';
import { BaseService } from '../shared/base.service';

@Injectable()
export class SignatoryService {
    private apiUrl: string = 'api/signatories';

    constructor(private baseService: BaseService) {
    }

    getByCompanyId(companyId: number): Promise<Array<SignatoryViewModel>> {
        return this.baseService.get<SignatoryViewModel>(this.apiUrl + '/company/' + companyId);
    }

    getById(id: number): Promise<SignatoryViewModel> {
        return this.baseService.get<SignatoryViewModel>(this.apiUrl + '/' + id);
    }

    update(signatory: SignatoryViewModel): Promise<any> {
        return this.baseService.put(this.apiUrl, signatory);
    }

    create(signatory: SignatoryViewModel): Promise<SignatoryViewModel> {
        return this.baseService.post(this.apiUrl, signatory);
    }
}