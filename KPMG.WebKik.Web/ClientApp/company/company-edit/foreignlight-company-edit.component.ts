import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { CompanyService } from '../company.service';
import { ProjectCompanyViewModel } from '../models/project-company.model';
import { ForeignLightCompanyViewModel } from '../models/foreign-light-company.model';
import { Http, Headers, RequestOptions } from '@angular/http';

@Component({
    template: require('./foreignlight-company-edit.component.html'),
    providers: [CompanyService]
})
export class ForeignCompanyLightEditComponent implements OnInit {
    model: ProjectCompanyViewModel;
    isLoading: boolean;
    projectId: number;
	companyId: number;
	fileYear: string;
	isUU: boolean;
	isUKIK: boolean;
	isND: boolean;
	UKIKDocType: any;
    isDocAttached: boolean;
    d: Date;

    constructor(private route: ActivatedRoute, private router: Router, private companyService: CompanyService, private http: Http) { }

    ngOnInit(): void {
        this.isLoading = true;
        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
		this.companyId = parseInt(this.route.snapshot.params['companyid'], 10);
		this.isDocAttached = false;
        this.companyService.getCompanyById(this.companyId).then(result => {
            this.model = result;
            this.d = new Date(this.model.ForeignLightCompany.FoundDate);
            var utcDate = new Date(Date.UTC(this.d.getFullYear(), this.d.getMonth(), this.d.getDate()));
            this.model.ForeignLightCompany.FoundDate = utcDate;
            this.isLoading = false;
        });

    }

    onSubmit(): void {
        this.isLoading = true;

        this.companyService.updateCompany(this.model).then(result => {
            this.isLoading = false;
            this.router.navigate(['../../'], { relativeTo: this.route });
        });
	}

	uploadFile(event: any): void {
		this.isLoading = true;
		let fileList: FileList = event.target.files;
		let formData = new FormData();
		formData.append("year", this.fileYear);
		formData.append("companyType", this.model.State);
		formData.append("isUU", this.isUU ? this.isUU : false);
		formData.append("isUKIK", this.isUKIK ? this.isUKIK : false);
		formData.append("isND", this.isND ? this.isND : false);
		formData.append("UKIKDocType", this.UKIKDocType ? this.UKIKDocType : '');
		formData.append("companyId", this.model.Id);

		let headers = new Headers();
		headers.append('Accept', 'application/json');
		let options = new RequestOptions({ headers: headers });

		if (fileList.length > 0) {
			for (let i = 0; i < fileList.length; i++) {
				formData.append('file', fileList[i]);
			}
		}
		this.companyService.uploadFile(formData, options).then(result => {
			this.isLoading = false;
			this.isND = false;
			this.isUKIK = false;
			this.isUU = false;
			this.fileYear = '';
			this.isDocAttached = false;

			var element: HTMLInputElement = <HTMLInputElement>document.getElementById('fileAttached');
			element.value = '';
			element.files.length[0];

		});
	}
}

