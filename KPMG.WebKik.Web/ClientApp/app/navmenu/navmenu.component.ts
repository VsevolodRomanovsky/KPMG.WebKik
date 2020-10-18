import { Component } from '@angular/core';
import { PermissionService } from '../../shared/permission/permission.service';
import { PermissionViewModel } from '../../shared/permission/permission.model';

@Component({
    selector: 'nav-menu',
    template: require('./navmenu.component.html'),
    styles: [require('./navmenu.component.scss')]
})
export class NavMenuComponent {
    constructor(private permissionService: PermissionService) {
    }

    get permission() {
        return this.permissionService.permission;
    }
}
