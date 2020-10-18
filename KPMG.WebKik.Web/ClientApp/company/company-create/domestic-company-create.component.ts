import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { CompanyService } from '../company.service';
import { ProjectCompanyViewModel } from '../models/project-company.model';
import { DomesticCompanyViewModel } from '../models/domestic-company.model';
import { TransferDataService } from '../company.service';

@Component({
    template: require('./domestic-company-create.component.html')
})
export class DomesticCompanyCreateComponent implements OnInit {
    company: ProjectCompanyViewModel;
    model: DomesticCompanyViewModel;
    cm: string;
    isLoading: boolean;


    constructor(private route: ActivatedRoute, private tService: TransferDataService, private _router: Router,
        private companyService: CompanyService) { }

    ngOnInit(): void {
        this.model = new DomesticCompanyViewModel();
        this.company = TransferDataService.getInstance().getCompany();
    }

    onSubmit(): void {
        this.isLoading = true;
        this.company.DomesticCompany = this.model; 
        this.companyService.createCompany(this.company).then(result => {
            this.isLoading = true;
            this._router.navigate(['../../'], { relativeTo: this.route });
        });
    }
}

