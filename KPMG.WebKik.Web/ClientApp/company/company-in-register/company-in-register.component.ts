import { Component, Input, OnInit } from '@angular/core';

import { CompanyService } from '../company.service';
import { ProjectCompanyViewModel } from '../models/project-company.model';

@Component({
    selector: 'company-in-register',
    template: require('./company-in-register.component.html'),
    providers: [CompanyService]
})
export class CompanyInRegisterComponent implements OnInit {
    @Input('company-id') companyId: number;

    model: ProjectCompanyViewModel;
    countryName: string;

    constructor(private companyService: CompanyService) { }

    ngOnInit(): void {
        this.companyService.getCompanyById(this.companyId).then(result => {
            this.model = result;
            if (typeof this.model !== "undefined") {

                this.countryName = this.model.ForeignCompany !== null
                    ? this.model.ForeignCompany.CountryCode.Name
                    : this.model.ForeignLightCompany.CountryCode.Name;
            }
            console.log(this.countryName);
            console.log(this.model.ForeignCompany.CountryCode.Name);
            console.log(this.model.ForeignLightCompany.CountryCode.Name);
        });
    }
}

