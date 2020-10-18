import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { TaxReturnService } from '../tax-return.service';
import { TaxReturnViewModel } from '../models/tax-return.model';

@Component({
    template: require('./tax-return-edit.component.html'),
})
export class TaxReturnEditComponent implements OnInit {
    model: TaxReturnViewModel;
    taxreturnId: number;
    projectId: number;
    companyId: number;
    isLoading: boolean;

    constructor(
        private dataService: TaxReturnService,
        private router: Router,
        private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.isLoading = true;
        this.taxreturnId = parseInt(this.route.snapshot.params['taxreturnid'], 10);
        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
        let loadTask: Promise<TaxReturnViewModel>;

        if (this.taxreturnId == 0) {
            loadTask = Promise.resolve(new TaxReturnViewModel());
        } else {
            loadTask = this.dataService.getById(this.taxreturnId);
        }

        loadTask.then(result => {
            this.model = Object.assign(new TaxReturnViewModel(), result);
            this.isLoading = false;
        });
    }

    onSubmit() {
        this.isLoading = true;
        const successCallback = (result) => {
            this.isLoading = false;
            if (result) {
                this.taxreturnId = result.Id;
            }
            this.router.navigate(['../' + this.taxreturnId], { relativeTo: this.route });
        };

        if (this.isNew) {
            this.dataService.create(this.model).then(successCallback);
        } else {
            this.dataService.update(this.model).then(successCallback);
        }
    }

    getFile() {
        this.dataService.getFileById(this.taxreturnId).then(blobContent => {
            const url = window.URL.createObjectURL(blobContent);
            window.open(url);
        });
    }

    get title() { return this.isNew ? "Создание декларации" : "Редактирование декларации"; }
    get isNew() { return this.taxreturnId == 0; }
}

