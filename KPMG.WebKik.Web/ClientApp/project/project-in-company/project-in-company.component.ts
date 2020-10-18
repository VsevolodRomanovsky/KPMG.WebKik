import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ProjectService } from '../project.service';
import { ProjectViewModel } from '../model/project.model';

@Component({
    selector: 'project-in-company',
    template: require('./project-in-company.component.html')
})
export class ProjectInCompanyComponent implements OnInit {
    @Input('project-id') projectId: number;
    project: ProjectViewModel;
    isLoading: boolean;

    constructor(private projectService: ProjectService, private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.isLoading = true;
        this.projectService.getProjectById(this.projectId).then(result => {
            this.project = result;
            this.isLoading = false;
        });
    }
}

