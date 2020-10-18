import { Injectable } from '@angular/core';

import { RoleViewModel } from './models/role.model';
import { BaseService } from '../shared/base.service';

@Injectable()
export class RoleService {
    private apiUrl: string = 'api/roles/';

    constructor(private baseService: BaseService) {
    }

    getRoles(): Promise<Array<RoleViewModel>> {
        return this.baseService.get<Array<RoleViewModel>>(this.apiUrl);
    }
}