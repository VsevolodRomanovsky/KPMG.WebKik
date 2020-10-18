import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ProjectService } from '../project.service';
import { ProjectViewModel } from '../model/project.model';

@Component({
    template: require('./project-detail.component.html')
})
export class ProjectDetailComponent implements OnInit {
    project: ProjectViewModel;

    isLoading: boolean;

    constructor(private projectService: ProjectService, private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.isLoading = true;
        let id = parseInt(this.route.snapshot.params['projectid'], 10);
        this.projectService.getProjectById(id).then(result => {
            this.project = result;
            this.isLoading = false;
        });
    }
}

