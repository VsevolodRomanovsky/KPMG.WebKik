import { Injectable } from '@angular/core';

import { TaxReturnViewModel } from './models/tax-return.model';
import { BaseService } from '../shared/base.service';

@Injectable()
export class TaxReturnService {
    private apiUrl: string = 'api/taxreturn';

    constructor(private baseService: BaseService) {
    }

    getByProjectId(projectId: number): Promise<Array<TaxReturnViewModel>> {
        return this.baseService.get<TaxReturnViewModel>(this.apiUrl + '/project/' + projectId);
    }

    getById(id: number): Promise<TaxReturnViewModel> {
        return this.baseService.get<TaxReturnViewModel>(this.apiUrl + '/' + id);
    }

    getFileById(id: number): Promise<Blob> {
        return this.baseService.getBlob(this.apiUrl + '/' + id + '/file');
    }

    update(taxreturn: TaxReturnViewModel): Promise<any> {
        return this.baseService.put(this.apiUrl, taxreturn);
    }

    create(signatory: TaxReturnViewModel): Promise<TaxReturnViewModel> {
        return this.baseService.post(this.apiUrl, signatory);
    }
}