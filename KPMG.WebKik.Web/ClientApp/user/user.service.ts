import { Injectable } from '@angular/core';

import { UserViewModel } from './models/user.model';
import { BaseService } from '../shared/base.service';

@Injectable()
export class UserService {
    private apiUrl: string = 'api/users/';

    constructor(private baseService: BaseService) {
    }

    getUsers(): Promise<Array<UserViewModel>> {
        return this.baseService.get<Array<UserViewModel>>(this.apiUrl);
    }

    getUserById(id: number): Promise<UserViewModel> {
        return this.baseService.get<UserViewModel>(this.apiUrl + '/' + id);
    }

    getUsersByProjectId(projectId: number): Promise<Array<UserViewModel>> {
        return this.baseService.get<UserViewModel>(this.apiUrl + 'project/' + projectId);
    }

    addUserToProject(userid: number, projectId: number): Promise<UserViewModel> {
        return this.baseService.post<UserViewModel>(this.apiUrl + 'project/' + projectId + '/add/' + userid, '');
    }

    removeUserFromProject(userid: number, projectId: number): Promise<any> {
        return this.baseService.delete(this.apiUrl + 'project/' + projectId + '/remove/' + userid);
    }

    updateUser(user: UserViewModel): Promise<any> {
        return this.baseService.put(this.apiUrl, user);
    }

    createUser(user: UserViewModel): Promise<UserViewModel> {
        return this.baseService.post(this.apiUrl, user);
    }
}