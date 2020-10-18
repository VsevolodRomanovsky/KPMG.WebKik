import { Component, OnInit, Input } from '@angular/core';

import { CompanyService } from '../company.service';
import { ProjectCompanyViewModel } from '../models/project-company.model';

@Component({
    selector: '[notification-companies-options]',
    template: require('./notification-companies-options.component.html')
})
export class NotificationCompaniesOptionsComponent implements OnInit {
    @Input('ngModel') selectedId: number;
    @Input('notification-companies-options') projectId: number;

    companyList: Array<ProjectCompanyViewModel>;

    constructor(private companyService: CompanyService) {
        this.companyList = [];
    }

    ngOnInit(): void {
        this.companyService
            .getCompaniesForNotification(this.projectId)
            .then(result => {
                this.companyList = result;
                this.companyList.sort((a, b): number => {
                    if (a.Name < b.Name) return -1;
                    if (a.Name > b.Name) return 1;
                    return 0;
                });
            });
    }
}

