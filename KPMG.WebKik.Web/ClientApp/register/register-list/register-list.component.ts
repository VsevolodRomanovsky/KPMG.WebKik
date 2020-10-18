import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { RegisterListDto } from '../models/register-list.model';
import { KeyValue } from '../../shared/models/key-value.model';
import { Year } from '../../shared/models/year.model';

import { CompanyService } from '../../company/company.service';

import { RegisterService } from '../services/register.service';

@Component({
    template: require('./register-list.component.html'),
    styles: [require('./register-list.component.scss')],
    providers: [CompanyService]
})
export class RegisterListComponent implements OnInit {
    isLoading: boolean;
    projectId: number;
    companyId: number;
    shareId: number;
    registerList: Array<RegisterListDto>;
    years: Array<number>;
    selectedYear: number = 2016;

    constructor(private route: ActivatedRoute, private registerService: RegisterService, private companyService: CompanyService, ) { }
        
    ngOnInit(): void {
        this.isLoading = true;
        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
        this.companyId = parseInt(this.route.snapshot.params['companyid'], 10);
        this.shareId = parseInt(this.route.snapshot.params['shareid'], 10);

        Promise.all([
            this.registerService.getRegistersByCompanyIdAndYear(this.shareId, this.selectedYear),
            this.registerService.getYears()
        ]).then((results: any[]) => {
            this.registerList = results[0];
            this.years = results[1];
            this.isLoading = false;
        });
    }

    onSelected(year: number): void {
        this.selectedYear = +year;
        this.registerService.getRegistersByCompanyIdAndYear(this.shareId, this.selectedYear)
            .then(result => {
                this.registerList = result;
            });
    }


    onLoadKik(event:any): void {
        let fileList: FileList = event.target.files;
        if (fileList.length > 0) {
            let file: File = fileList[0];
            let formData: FormData = new FormData();

            formData.append('sharedId', this.shareId);
            formData.append('year', this.selectedYear);
            formData.append('file', file);

            this.companyService.uploadKikRegisters(formData).then(result => {
                //  this.isLoading = false;
            });
            //this.update();
            this.isLoading = false;
            window.location.reload();
        }
    }

    onLoadKl(event: any): void {
        let fileList: FileList = event.target.files;
        if (fileList.length > 0) {
            let file: File = fileList[0];
            let formData: FormData = new FormData();

            formData.append('sharedId', this.shareId);
            formData.append('year', this.selectedYear);
            formData.append('file', file);

            this.companyService.uploadKlRegisters(formData).then(result => {
                //  this.isLoading = false;
            });
            //this.update();
            this.isLoading = false;
            window.location.reload();
        }
    }
}

