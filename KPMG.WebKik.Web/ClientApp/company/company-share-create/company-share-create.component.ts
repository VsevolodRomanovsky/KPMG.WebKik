import { Component, OnInit, Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { CompanyService } from '../company.service';
import { ProjectCompanyViewModel } from '../models/project-company.model';
import { ProjectCompanyShareViewModel } from '../models/company-share.model';
import { KeyValue } from '../../shared/models/key-value.model';
import { State } from '../models/state.model';

@Component({
    template: require('./company-share-create.component.html'),
    providers: [CompanyService]
})
export class CompanyShareCreateComponent implements OnInit {
    model: ProjectCompanyShareViewModel;
    companyStates: Array<KeyValue<State>>;
    isLoading: boolean;
    projectId: number;
    companyId: number
    companyList: Array<ProjectCompanyViewModel>;
    isIndividualCompany: boolean = false;

    constructor(private companyService: CompanyService, private route: ActivatedRoute, private _router: Router) {

    }

    ngOnInit(): void {
        this.model = new ProjectCompanyShareViewModel();
        this.model.OwnerProjectCompany = new ProjectCompanyViewModel();
        this.model.DependentProjectCompany = new ProjectCompanyViewModel();

        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
        this.companyId = parseInt(this.route.snapshot.params['companyid'], 10);



        let loadDataTasks: Array<Promise<Array<any>>> = [
            this.companyService.getCompaniesByProjectId(this.projectId),
            this.companyService.getCompanyStates()
        ];

        Promise.all(loadDataTasks).then((results: any[]) => {
            this.companyList = results[0].map(item => { return Object.assign(new ProjectCompanyViewModel(), item); })
            let currentCompany = this.companyList.find(c => c.Id == this.companyId);
            this.model.OwnerProjectCompany = currentCompany;
            this.isIndividualCompany = this.model.OwnerProjectCompany.State == State.Individual;
            this.model.OwnerProjectCompanyId = currentCompany.Id;
            this.companyList = this.companyList.filter(item => {
                return item.Id != currentCompany.Id && item.State != State.Individual
            });

            this.companyStates = results[1];

            this.isLoading = false;
        });
    }

    onSubmit(selected): void {

        console.log('To save');

        let selectedCompany = selected.value;
        this.model.DependentProjectCompanyId = this.companyList.find(c => c.Id == selectedCompany).Id;
        this.model.CompanyStatus = "STATUS";
        this.isLoading = true;

        this.companyService.createCompanyShare(this.model).then(result => {
            this.isLoading = false;
            this._router.navigate(['../' + result.Id + '/edit'], { relativeTo: this.route });
        });
    }
}



