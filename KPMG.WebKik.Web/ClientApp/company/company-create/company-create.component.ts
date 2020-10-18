import { Component, OnInit, Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ProjectCompanyViewModel } from '../models/project-company.model';
import { CompanyService } from '../company.service';
import { TransferDataService } from '../company.service';
import { KeyValue } from '../../shared/models/key-value.model';
import { State } from '../models/state.model';

@Component({
    template: require('./company-create.component.html')
})
export class CompanyCreateComponent implements OnInit {
    model: ProjectCompanyViewModel;
    companyStates: Array<KeyValue<State>>;
    isLoading: boolean;
    selectedState: number;

    constructor(private companyService: CompanyService, private route: ActivatedRoute, private _router: Router) {

    }

    ngOnInit(): void {
        this.model = new ProjectCompanyViewModel();
        this.model.ProjectId = parseInt(this.route.snapshot.params['projectid'], 10);
        this.companyService.getCompanyStates().then(result => {
            this.companyStates = result;
        });
    }

    submit(selected): void {
        const stateName = State[this.model.State];
        let route = this._router.url + '/' + stateName.toLowerCase();
        TransferDataService.getInstance().setCompany(this.model);
        this._router.navigate([route]);
    }
}

