import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CompanyService } from '../company.service';

import { ProjectCompanyViewModel } from '../models/project-company.model';
import { IndividualCompanyViewModel } from '../models/individual-company.model';
import { TransferDataService } from '../company.service';
import { Http, Headers, RequestOptions } from '@angular/http';

@Component({
    template: require('./individual-company-edit.component.html'),
    providers: [CompanyService, TransferDataService]
})
export class IndividualCompanyEditComponent implements OnInit {
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
    //doo: Date

	constructor(private route: ActivatedRoute, private companyService: CompanyService, private router: Router, private http: Http) { }

    getUtcDate(doo:Date): Date
    {
        console.log("before - " + doo);
        console.log("after - " + new Date(Date.UTC(doo.getFullYear(), doo.getMonth(), doo.getDate())));
        return new Date(Date.UTC(doo.getFullYear(), doo.getMonth(), doo.getDate()));
    }

    ngOnInit(): void {
        this.isLoading = true;
        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
		this.companyId = parseInt(this.route.snapshot.params['companyid'], 10);
		this.isDocAttached = false;
        this.companyService.getCompanyById(this.companyId).then(result => {
            this.model = result;

            this.model.IndividualCompany.BirthDate = this.getUtcDate(new Date(this.model.IndividualCompany.BirthDate));
         
            this.model.IndividualCompany.VerifedPersonalityDocInfo.IssueDate =
                this.getUtcDate(new Date(this.model.IndividualCompany.VerifedPersonalityDocInfo.IssueDate));

            this.model.IndividualCompany.ConfirmedPersonalityDocInfo.IssueDate =
                this.getUtcDate(new Date(this.model.IndividualCompany.ConfirmedPersonalityDocInfo.IssueDate));

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

