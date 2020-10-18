import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { UserService } from '../user.service';
import { RoleService } from '../role.service';
import { UserViewModel } from '../models/user.model';
import { RoleViewModel } from '../models/role.model';

@Component({
    template: require('./user-edit.component.html'),
})
export class UserEditComponent implements OnInit {
    model: UserViewModel;
    roles: Array<RoleViewModel>;
    isNew: boolean;
    isLoading: boolean;
    hasUserExistException: boolean;

    constructor(
        private userService: UserService,
        private roleService: RoleService,
        private router: Router,
        private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.isLoading = true;
        let id = parseInt(this.route.snapshot.params['userid'], 10);
        let loadUserTask: Promise<UserViewModel>;

        if (id == 0) {
            this.isNew = true;
            loadUserTask = Promise.resolve(new UserViewModel());
        } else {
            loadUserTask = this.userService.getUserById(id);
        }

        let loadDataTasks: [Promise<UserViewModel>, Promise<Array<RoleViewModel>>] = [
            loadUserTask,
            this.roleService.getRoles()
        ];

        Promise.all(loadDataTasks).then((results: any[]) => {
            this.roles = results[1];
            this.model = results[0];
            this.model.Role = this.roles.find(role => role.Id == this.model.RoleId);
            this.isLoading = false;
        });
    }

    onUserLoginChange() {
        this.hasUserExistException = false;
    }

    onSubmit() {
        this.isLoading = true;
        this.hasUserExistException = false;
        this.model.RoleId = this.model.Role.Id;
        let successCallback = () => {
            this.isLoading = false;
            this.router.navigate(['../../'], { relativeTo: this.route });
        };
        let errorCallback = (error) => {
            if (error.status == 400 && error.statusText === "UpdateException") {
                this.hasUserExistException = true;
                this.isLoading = false;
            }
        };

        if (this.isNew) {
            this.userService.createUser(this.model).then(successCallback).catch(errorCallback);
        } else {
            this.userService.updateUser(this.model).then(successCallback).catch(errorCallback);
        }
    }

    get title() { return this.isNew ? "Создание пользователя" : "Редактирование пользователя"; }
}

