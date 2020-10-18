import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { ProjectService } from '../project.service';
import { ProjectViewModel } from '../model/project.model';

@Component({
    template: require('./project-edit.component.html')
})
export class ProjectEditComponent implements OnInit {
    model: ProjectViewModel;
    isNew: boolean;
    isLoading: boolean;

    constructor(
        private projectService: ProjectService,
        private router: Router,
        private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.isLoading = true;
        let id = parseInt(this.route.snapshot.params['projectid'], 10);

        if (id == 0) {
            this.isLoading = false;
            this.isNew = true;
            this.model = new ProjectViewModel();
            return;
        }

        this.isNew = false;
        this.projectService.getProjectById(id).then(result => {
            this.model = result;
            this.isLoading = false;
        });
    }

    onSubmit() {
        this.isLoading = true;
        if (this.isNew) {
            this.projectService.createProject(this.model).then(result => {
                this.isLoading = false;
                this.router.navigate(['../../' + result.Id], { relativeTo: this.route });
            });
        } else {
            this.projectService.updateProject(this.model).then(result => {
                this.isLoading = false;
                this.router.navigate(['../'], { relativeTo: this.route });
            });
        }
    }

    get title() { return this.isNew ? "Создание проекта" : "Редактирование проекта"; }
}

