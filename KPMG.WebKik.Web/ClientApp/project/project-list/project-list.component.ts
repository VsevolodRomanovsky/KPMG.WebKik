import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';

import { ProjectService } from '../project.service';
import { ProjectViewModel } from '../model/project.model';

@Component({
    template: require('./project-list.component.html')
})
export class ProjectListComponent implements OnInit {

    projectList: Array<ProjectViewModel>;

    isLoading: boolean;

    constructor(private projectService: ProjectService, private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.isLoading = true;
        this.projectService.getProjects().then(result => {
            this.projectList = result;
            this.isLoading = false;
        });
    }
}

