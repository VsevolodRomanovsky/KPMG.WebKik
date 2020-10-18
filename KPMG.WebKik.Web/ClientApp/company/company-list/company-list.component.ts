import { Component, ElementRef, Input, ViewChild  } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { CompanyService } from '../company.service';
import { ProjectCompanyViewModel } from '../models/project-company.model';
import { State } from '../models/state.model';



@Component({
    template: require('./company-list.component.html'),
    providers: [CompanyService],
    styles: [require('./company-list.scss')]
})
export class CompanyListComponent implements OnInit {
    projectId: number;
    companyList: Array<ProjectCompanyViewModel>;
    isLoading: boolean;
    stateText: string;


    constructor(private companyService: CompanyService, private route: ActivatedRoute, private router: Router) {
        
    }

    update(): void {
        this.companyService.getCompaniesByProjectId(this.projectId).then(result => {
            this.companyList = result;
            this.isLoading = false;
        });
    }

    ngOnInit(): void {
        this.isLoading = true;
        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);

        this.companyService.getCompaniesByProjectId(this.projectId).then(result => {
            this.companyList = result;
            this.isLoading = false;
        });
    }

    onCalculateBtnClick(): void {
        this.companyService.calculateProjectCompanyInfo(this.projectId).then(result => {
            this.isLoading = false;
        });
    }

    onBrowseFile(event: any) {
        alert("test");
    }

    //импорт компаний из Excel
    onLoadForeignCompanies(event: any) {
        let fileList: FileList = event.target.files;
        if (fileList.length > 0) {
            let file: File = fileList[0];
            let formData: FormData = new FormData();

            formData.append('projectId', this.projectId);
            formData.append('file', file);
            this.companyService.uploadForeignCompany(formData).then(result => {
                
            });
            
            this.isLoading = false;
            window.location.reload();
        }
    }

    onLoadForeignLightCompanies(event: any) {
        let fileList: FileList = event.target.files;
        if (fileList.length > 0) {
            let file: File = fileList[0];
            let formData: FormData = new FormData();

            formData.append('projectId', this.projectId);
            formData.append('file', file);
            this.companyService.uploadForeginLightCompany(formData).then(result => {});
            this.isLoading = false;
            window.location.reload();
        }
    }

    onLoadDomesticCompanies(event: any) {
        let fileList: FileList = event.target.files;
        if (fileList.length > 0) {
            let file: File = fileList[0];
            let formData: FormData = new FormData();

            formData.append('file', file);
            formData.append('projectId', this.projectId);
            this.companyService.uploadDomesticCompany(formData).then(result => {
                
            });
            this.isLoading = false;
            window.location.reload();
        }
    }

    onLoadIndividualCompanies(event: any) {
        let fileList: FileList = event.target.files;
        if (fileList.length > 0) {
            let file: File = fileList[0];
            let formData: FormData = new FormData();
            formData.append('projectId', this.projectId);

            formData.append('file', file);
            this.companyService.uploadIndividualCompany(formData).then(result => {
                //this.isLoading = false;
            });
            //this.router.navigate();
            this.isLoading = false;
            window.location.reload();
        }
    }
}

