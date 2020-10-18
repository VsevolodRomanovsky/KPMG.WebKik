import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyService } from '../company.service';
import { IndividualCompanyViewModel } from '../models/individual-company.model';
import { ProjectCompanyViewModel } from '../models/project-company.model';
import { TransferDataService } from '../company.service';


@Component({
    template: require('./individual-company-create.component.html')
})
export class IndividualCompanyCreateComponent implements OnInit {
    model: ProjectCompanyViewModel;
    isLoading: boolean;

    constructor(private route: ActivatedRoute, private companyService: CompanyService, private router: Router) { }

    ngOnInit(): void {
        this.model = TransferDataService.getInstance().getCompany();
        this.model.IndividualCompany = new IndividualCompanyViewModel();
    }

    onSubmit(): void {
        this.isLoading = true;

        this.companyService.createCompany(this.model).then(result => {
            this.isLoading = true;
            this.router.navigate(['../../'], { relativeTo: this.route });
        });
    }
}

