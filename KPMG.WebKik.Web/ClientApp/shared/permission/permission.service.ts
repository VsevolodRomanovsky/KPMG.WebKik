import { Injectable } from '@angular/core';

import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';

import { PermissionViewModel } from './permission.model';
import { BaseService } from '../../shared/base.service';

@Injectable()
export class PermissionService {
    private apiUrl: string = 'api/permissions/';
    public permission: PermissionViewModel;

    constructor(private baseService: BaseService) {
        this.getPermission();
    }

    getPermission(): Promise<PermissionViewModel> {
        if (this.permission) {
            return Promise.resolve(this.permission);
        }

        return this.baseService
            .get<PermissionViewModel>(this.apiUrl + 'current')
            .then(permission => {
                this.permission = permission;
                return this.permission;
            });
    }
} 