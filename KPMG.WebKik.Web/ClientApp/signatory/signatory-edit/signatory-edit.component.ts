import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { SignatoryService } from '../signatory.service';
import { SignatoryViewModel } from '../models/signatory.model';

@Component({
    template: require('./signatory-edit.component.html'),
})
export class SignatoryEditComponent implements OnInit {
    model: SignatoryViewModel;
    isNew: boolean;
    isLoading: boolean;

    constructor(
        private signatoryService: SignatoryService,
        private router: Router,
        private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.isLoading = true;
        let id = parseInt(this.route.snapshot.params['signatoryid'], 10);
        let loadTask: Promise<SignatoryViewModel>;

        if (id == 0) {
            this.isNew = true;
            loadTask = Promise.resolve(new SignatoryViewModel());
        } else {
            loadTask = this.signatoryService.getById(id);
        }

        loadTask.then(result => {
            this.model = result;
            this.model.ProjectCompanyId = parseInt(this.route.snapshot.params['companyid'], 10);
            this.isLoading = false;
        });
    }

    onSubmit() {
        this.isLoading = true;
        const successCallback = () => {
            this.isLoading = false;
            this.router.navigate(['../'], { relativeTo: this.route });
        };

        if (this.isNew) {
            this.signatoryService.create(this.model).then(successCallback);
        } else {
            this.signatoryService.update(this.model).then(successCallback);
        }
    }

    get title() { return this.isNew ? "Создание подписанта" : "Редактирование подписанта"; }
}

