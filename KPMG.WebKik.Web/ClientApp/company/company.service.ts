import { Component, Injectable } from '@angular/core';

import 'rxjs/add/operator/toPromise';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/map';

import { URLSearchParams, QueryEncoder } from '@angular/http';

import { ProjectCompanyViewModel } from './models/project-company.model';
import { ProjectCompanyShareViewModel } from './models/company-share.model';
import { ProjectCompanyFactShareViewModel } from './models/company-fact-share.model';
import { State } from './models/state.model';
import { GroundsType } from './models/grounds-type.model';
import { BaseService } from '../shared/base.service';
import { KeyValue } from '../shared/models/key-value.model';
import { TaxExemptionResult } from './models/tax-exemption-result.model'
import { TaxExemtionsViewModel } from './models/tax-exemption.model'
import { Http, Headers, RequestOptions } from '@angular/http';

@Injectable()
export class CompanyService {

    private baseUrl: string = 'api/companies/';
    private projectCompanyShareUrl: string = "api/shares/";
    private projectCompanyConttolUrl: string = "api/companies/control";

	constructor(private baseService: BaseService, private http: Http) {
    }

    getCompaniesByProjectId(projectId: number): Promise<Array<ProjectCompanyViewModel>> {
        return this.baseService.get<Array<ProjectCompanyViewModel>>(this.baseUrl + 'project/' + projectId);
    }

    calculateProjectCompanyInfo(projectId: number): Promise<number> {
        return this.baseService.get<number>(this.baseUrl + 'project/' + projectId + '/calculate');
    }

    getCompanyById(companyId: number): Promise<ProjectCompanyViewModel> {
        return this.baseService.get<ProjectCompanyViewModel>(this.baseUrl + companyId);
    }

    getCompaniesForNotification(projectId: number): Promise<Array<ProjectCompanyViewModel>> {
        return this.baseService.get<ProjectCompanyViewModel>(this.baseUrl + 'project/' + projectId + '/companies-for-notification');
    }

    getGroundsTypes(): Promise<Array<KeyValue<GroundsType>>> {
        return this.baseService.get<Array<KeyValue<GroundsType>>>(this.baseUrl + 'groundstypes');
    }

    getCompanyStates(): Promise<Array<KeyValue<State>>> {
        return this.baseService.get<Array<KeyValue<State>>>(this.baseUrl + '/states');
    }

    updateCompany(company: ProjectCompanyViewModel): Promise<any> {
        return this.baseService.put(this.baseUrl, company);
    }

    createCompany(company: ProjectCompanyViewModel): Promise<ProjectCompanyViewModel> {
        return this.baseService.post(this.baseUrl, company);
    }

    createCompanyShare(companyShare: ProjectCompanyShareViewModel): Promise<ProjectCompanyShareViewModel> {
        return this.baseService.post(this.projectCompanyShareUrl, companyShare);
    }

    updateCompanyShare(company: ProjectCompanyShareViewModel): Promise<any> {
        return this.baseService.put(this.projectCompanyShareUrl, company);
    }

    getTaxStatus(companyId: number, companyToId: number, year: number): Promise<TaxExemtionsViewModel> {
        return this.baseService.get(this.baseUrl + companyId + '/tax-exemptions/' + companyToId + '/' + year);
    }

    defineTaxStatus(data: TaxExemtionsViewModel): Promise<TaxExemptionResult> {
        return this.baseService.post(this.baseUrl + '/definestatus', data);
    }

    getSharesByProjecCompanytId(companyId: number): Promise<Array<ProjectCompanyShareViewModel>> {
        return this.baseService.get<Array<ProjectCompanyShareViewModel>>(this.projectCompanyShareUrl + 'company/' + companyId);
    }

    getFactSharesByProjecCompanytId(companyId: number, date: Date): Promise<Array<ProjectCompanyFactShareViewModel>> {
        let params: URLSearchParams = new URLSearchParams();
        if (date) params.set('date', date.toJSON());

        return this.baseService.get<Array<ProjectCompanyShareViewModel>>(this.projectCompanyShareUrl + 'company/' + companyId + '/fact', { search: params });
    }

    getKIKByProjecCompanytId(companyId: number): Promise<Array<ProjectCompanyShareViewModel>> {
        return this.baseService.get<Array<ProjectCompanyShareViewModel>>(this.projectCompanyShareUrl + 'company/' + companyId + '/kik');
    }

    getKikDocument(companyId: number): Promise<Blob> {
        return this.baseService.getBlob(this.baseUrl + companyId + '/kikdocument');
    }

    getProjectCompanyShareById(projectCompanyShareId: number): Promise<ProjectCompanyShareViewModel> {
        return this.baseService.get<ProjectCompanyShareViewModel>(this.projectCompanyShareUrl + projectCompanyShareId);
    }

    removeProjectCompanyShare(projectCompanyShareId: number): Promise<any> {
        return this.baseService.delete(this.projectCompanyShareUrl + projectCompanyShareId);
    }

	uploadFile(formData: FormData, options: RequestOptions): Promise<any> {
		return this.http.post(this.baseUrl + 'upload', formData, options).toPromise();
	}
    //импорт компаний из Excel
    uploadForeginLightCompany(formData: FormData): Promise<any> {
        return this.http.post(this.baseUrl + 'foreignLightCompanyImport', formData).toPromise();
    }

    uploadIndividualCompany(formData: FormData): Promise<any> {
        return this.http.post(this.baseUrl + 'individualCompanyImport', formData).toPromise();}

    uploadForeignCompany(formData: FormData): Promise<any> {
        return this.http.post(this.baseUrl + 'foreignCompanyImport', formData).toPromise();
    }

    uploadDomesticCompany(formData: FormData): Promise<any> {
        return this.http.post(this.baseUrl + 'domesticCompanyImport', formData).toPromise();
    }

    uploadKikRegisters(formData: FormData): Promise<any> {
        return this.http.post(this.baseUrl + 'uploadKikRegisters', formData).toPromise();
    }

    uploadKlRegisters(formData: FormData): Promise<any> {
        return this.http.post(this.baseUrl + 'uploadKlRegisters', formData).toPromise();
    }
}

export class TransferDataService {
    static isCreating: Boolean = false;
    static instance: TransferDataService;
    private company: ProjectCompanyViewModel;

    static getInstance() {
        if (TransferDataService.instance == null) {
            TransferDataService.isCreating = true;
            TransferDataService.instance = new TransferDataService();
            TransferDataService.isCreating = false;
        }

        return TransferDataService.instance;
    }

    setCompany(_company: ProjectCompanyViewModel) {
        this.company = _company;
    }

    getCompany() {
        return this.company;
    }
}