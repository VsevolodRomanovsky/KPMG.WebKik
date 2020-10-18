import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { Router, ActivatedRoute, Params } from '@angular/router';

import { SignatoryService } from '../signatory.service';
import { SignatoryViewModel } from '../models/signatory.model';

@Component({
    template: require('./signatory-list.component.html')
})
export class SignatoryListComponent implements OnInit {
    signatoryList: Array<SignatoryViewModel>;
    projectId: number;
    companyId: number;

    isLoading: boolean;

    constructor(private signatoryService: SignatoryService, private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
        this.companyId = parseInt(this.route.snapshot.params['companyid'], 10);
        this.isLoading = true;

        this.signatoryService.getByCompanyId(this.companyId).then(result => {
            this.signatoryList = result;
            this.isLoading = false;
        });
    }
}

