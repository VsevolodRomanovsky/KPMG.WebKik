import { Injectable } from '@angular/core';
import {
    CanActivate,
    CanActivateChild
} from '@angular/router';

import { PermissionService } from '../shared/permission/permission.service';

@Injectable()
export class DirectoryAuthService implements CanActivate, CanActivateChild {
    constructor(private permissionService: PermissionService) {
    }

    canActivate(): Promise<boolean> {
        return this.permissionService.getPermission().then(permission => { return permission.IsAdmin });
    }

    canActivateChild(): Promise<boolean> {
        return this.canActivate();
    }
} 