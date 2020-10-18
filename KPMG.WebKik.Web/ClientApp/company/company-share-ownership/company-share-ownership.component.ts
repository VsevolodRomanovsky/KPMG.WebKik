import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { CompanyService } from '../company.service';
import { ProjectCompanyViewModel } from '../models/project-company.model';
import { ProjectCompanyShareViewModel } from '../models/company-share.model';
import { ProjectCompanyFactShareViewModel } from '../models/company-fact-share.model';
import { State } from '../models/state.model';

@Component({
    template: require('./company-share-ownership.component.html'),
    providers: [CompanyService]
})
export class CompanyShareOwnershipComponent implements OnInit {
    shareList: Array<ProjectCompanyShareViewModel>;    
    company: ProjectCompanyViewModel;
    projectId: number;
    companyId: number;
    isLoading: boolean;
    editUrl: string;
    stateName: string;

    factShares: Array<ProjectCompanyFactShareViewModel>;
    factDate: Date;

    constructor(private companyService: CompanyService, private route: ActivatedRoute, private _router: Router) { }

    ngOnInit(): void {
        this.isLoading = true;

        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
        this.companyId = parseInt(this.route.snapshot.params['companyid'], 10);

        Promise.all([
            this.companyService.getSharesByProjecCompanytId(this.companyId),
            this.companyService.getCompanyById(this.companyId),
            this.loadFactShare()
        ]).then((results: any[]) => {
            this.shareList = results[0];
            this.company = results[1];
            this.isLoading = false;
        });
    }   

    loadFactShare() {
        return this.companyService.getFactSharesByProjecCompanytId(this.companyId, this.factDate)
            .then(share => {
                this.factShares = share;
            });
    }
}

