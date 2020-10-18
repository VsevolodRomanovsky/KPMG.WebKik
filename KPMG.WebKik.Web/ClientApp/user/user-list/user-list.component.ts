import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';

import { UserService } from '../user.service';
import { UserViewModel } from '../models/user.model';

@Component({
    template: require('./user-list.component.html')
})
export class UserListComponent implements OnInit {

    userList: Array<UserViewModel>;

    isLoading: boolean;

    constructor(private userService: UserService, private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.isLoading = true;
        this.userService.getUsers().then(result => {
            this.userList = result;
            this.isLoading = false;
        });
    }
}

