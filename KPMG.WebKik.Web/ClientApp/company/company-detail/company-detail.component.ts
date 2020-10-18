import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { CompanyService } from '../company.service';
import { ProjectCompanyViewModel } from '../models/project-company.model';
import { ProjectCompanyShareViewModel } from '../models/company-share.model';
import { State } from '../models/state.model';

@Component({
    template: require('./company-detail.component.html'),
    providers: [CompanyService]
})
export class CompanyDetailComponent implements OnInit {
    shareList: Array<ProjectCompanyShareViewModel>;
    company: ProjectCompanyViewModel;
    projectId: number;
    companyId: number;
    isLoading: boolean;
    editUrl: string;
    stateName: string;

    constructor(private companyService: CompanyService, private route: ActivatedRoute, private _router: Router) { }

    ngOnInit(): void {
        this.isLoading = true;

        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
        this.companyId = parseInt(this.route.snapshot.params['companyid'], 10);

        Promise.all([
            this.companyService.getSharesByProjecCompanytId(this.companyId),
            this.companyService.getCompanyById(this.companyId)
        ]).then((results: any[]) => {
            this.shareList = results[0];
            this.company = results[1];
            const stateName = State[this.company.State];
            this.editUrl = this._router.url + '/edit/' + stateName.toLowerCase();
            this.isLoading = false;
        });
    }
    getKikDocument() {
        this.companyService.getKikDocument(this.companyId).then(blobContent => {
            const url = window.URL.createObjectURL(blobContent);
            window.open(url);
        });
    }

    removeShareFromCompany(id: number) {
        this.companyService.removeProjectCompanyShare(id)
            .then(share => {
                let index = this.shareList.indexOf(share);
                this.shareList.splice(index, 1);
            });
    }
}

