import { Component, Input, OnInit } from '@angular/core';

import { PermissionService } from '../../shared/permission/permission.service';
import { PermissionViewModel } from '../../shared/permission/permission.model';
import { UserService } from '../user.service';
import { UserViewModel } from '../models/user.model';

@Component({
    selector: 'user-in-project',
    template: require('./user-in-project.component.html')
})
export class UserInProjectComponent implements OnInit {
    @Input('project-id') projectId: number;

    userList: Array<UserViewModel> = [];
    allUsers: Array<UserViewModel> = [];
    selectedUserToAdd: UserViewModel;
    permission: PermissionViewModel;

    constructor(private userService: UserService, private permissionService: PermissionService) { }

    get usersToAdd(): Array<UserViewModel> {
        return this.allUsers.filter(x => this.userList.filter(y => y.Id == x.Id).length === 0);
    }

    ngOnInit(): void {
        this.permission = this.permissionService.permission;

        let loadDataTasks: [Promise<Array<UserViewModel>>, Promise<Array<UserViewModel>>] = [
            this.userService.getUsersByProjectId(this.projectId),
            this.userService.getUsers()
        ];

        Promise.all(loadDataTasks).then((results: any[]) => {
            this.userList = results[0];
            this.allUsers = results[1];
        });
    }

    addUser(): void {
        this.userService
            .addUserToProject(this.selectedUserToAdd.Id, this.projectId)
            .then(user => {
                this.selectedUserToAdd = null;
                this.userList.push(user);
            });
    }

    removeUser(user: UserViewModel): void {
        this.userService
            .removeUserFromProject(user.Id, this.projectId)
            .then(user => {
                this.selectedUserToAdd = null;
                let index = this.userList.indexOf(user);
                this.userList.splice(index, 1);
            });
    }
}

