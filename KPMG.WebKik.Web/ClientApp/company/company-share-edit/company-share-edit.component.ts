import { Component, OnInit, Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { CompanyService } from '../company.service';
import { ProjectCompanyViewModel } from '../models/project-company.model';
import { ProjectCompanyShareViewModel } from '../models/company-share.model';
import { KeyValue } from '../../shared/models/key-value.model';
import { GroundsType } from '../models/grounds-type.model';
import { State } from '../models/state.model';
@Component({
    template: require('./company-share-edit.component.html'),
    providers: [CompanyService]
})
export class CompanyShareEditComponent implements OnInit {
    model: ProjectCompanyShareViewModel;
    groundTypes: Array<KeyValue<GroundsType>>;
    isLoading: boolean;
    projectId: number;
    companyId: number;
    shareId: number;
    isDomesticOrIndividualCompany: boolean;
    isForeignOrIndividualCompany: boolean;
    isForeignLightCompany: boolean;
    isDependentForeignToDomestic: boolean;
    isDependentForeignToIndividual: boolean;
    isIndividualCompany: boolean;
    factControlTitle: string;
    controlGroundsTitle: string;

    constructor(private companyService: CompanyService, private route: ActivatedRoute, private _router: Router) {

    }

    ngOnInit(): void {
        //this.model = new ProjectCompanyShareViewModel();
        //this.model.OwnerProjectCompany = new ProjectCompanyViewModel();
        //this.model.DependentProjectCompany = new ProjectCompanyViewModel();

        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
        this.companyId = parseInt(this.route.snapshot.params['companyid'], 10);
        this.shareId = parseInt(this.route.snapshot.params['shareid'], 10);

        Promise.all([
            this.companyService.getProjectCompanyShareById(this.shareId),
            this.companyService.getGroundsTypes()
        ]).then((results: any[]) => {
            this.model = results[0];
            
            this.configureFlags(this.model.OwnerProjectCompany.State);
            this.configureDependencyFlags(this.model.DependentProjectCompany.State, this.model.OwnerProjectCompany.State);
            this.groundTypes = results[1];
            this.isLoading = false;
        });
    }

    onSubmit(): void {

        this.companyService.updateCompanyShare(this.model).then(result => {
            this._router.navigate(['../../../share'], { relativeTo: this.route });
        });
    }

    removeShare() {
        if (confirm("Удалить владение?")) {
            this.companyService.removeProjectCompanyShare(this.shareId)
                .then(share => this._router.navigate(['../../../share'], { relativeTo: this.route }));
        }
    }
    configureDependencyFlags(projectCompanyState: State, ownerCompanyState: State): void {
        this.isDependentForeignToDomestic = (projectCompanyState == State.Foreign && ownerCompanyState == State.Domestic);
        this.isDependentForeignToIndividual = (projectCompanyState == State.Foreign && ownerCompanyState == State.Individual);
        if (projectCompanyState == State.Foreign && ownerCompanyState == State.Individual) {
            this.factControlTitle = 'Осуществляет контроль';
            this.controlGroundsTitle = 'Основание (наименование и реквизиты документа)';
        } else {
            this.factControlTitle = 'Осуществляет фактический контроль';
            this.controlGroundsTitle = 'Основания для контроля';
        }
    }
    configureFlags(projectCompanyState: State): void {
        this.isDomesticOrIndividualCompany = projectCompanyState == State.Domestic || projectCompanyState == State.Individual;
        this.isForeignOrIndividualCompany = projectCompanyState == State.Foreign || projectCompanyState == State.Individual;
        this.isForeignLightCompany = projectCompanyState == State.ForeignLight;
        this.isIndividualCompany = projectCompanyState == State.Individual;
    }
}





