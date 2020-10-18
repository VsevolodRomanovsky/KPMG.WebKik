import { Injectable } from '@angular/core';

import { ProjectViewModel } from './model/project.model';
import { ProjectOwnershipViewModel } from './model/project-ownership.model';
import { BaseService } from '../shared/base.service';

@Injectable()
export class ProjectService {
    private apiUrl: string = 'api/projects/';

    constructor(private baseService: BaseService) {
    }

    getProjects(): Promise<Array<ProjectViewModel>> {
        return this.baseService.get<Array<ProjectViewModel>>(this.apiUrl);
    }

    getProjectById(projectId: number): Promise<ProjectViewModel> {
        return this.baseService.get<ProjectViewModel>(this.apiUrl + projectId);
    }

    getProjectOwnership(projectId: number): Promise<ProjectOwnershipViewModel> {
        return this.baseService.get<ProjectOwnershipViewModel>(this.apiUrl + projectId + '/ownership');
    }

    updateProject(project: ProjectViewModel): Promise<any> {
        return this.baseService.put(this.apiUrl, project);
    }

    createProject(project: ProjectViewModel): Promise<ProjectViewModel> {
        return this.baseService.post(this.apiUrl, project);
    }
}