import { Component, OnInit, Pipe, PipeTransform, Injectable, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Register10ViewModel } from '../models/register10.model';
import { Register10SectionViewModel } from '../models/register10section.model';
import { RegisterType } from '../models/register-type.model';
import { Section } from '../models/registerSection.model';

import { Register10Service } from '../services/register10.service';
import { FilterPipe } from './register10.pipe';

import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';

@Component({
    template: require('./register10.component.html'),
    styles: [require('./register10.component.scss')],
    providers: []
})


export class Register10Component implements OnInit {
    @ViewChild('Register10Modal') modal: ModalComponent;

    model: Register10ViewModel;

    projectId: number;
    companyId: number;
    registerId: number;
    shareId: number;

    year: number;
    title: string;
    isLoading: boolean;
    isNew: boolean;
    isCreate: boolean;
    dataIndex: number;

    titles: string[];

    selectedSection: Section;
    sectionList: Array<Section>;


    register10DataItem: Register10SectionViewModel;

    LastYearDividendSumAggr: number;
    CurrentYearTransitionalDividendSumAggr: number;
    CurrentYearDividendSumAgg: number;

    Summary: number;
    SummaryYear: number;

    dateOptions: any;
    format: any;


    sumNum1: number;

    constructor(private route: ActivatedRoute, private router: Router, private registerService: Register10Service) {
        this.isLoading = true;
        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);

        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
        this.companyId = parseInt(this.route.snapshot.params['companyid'], 10);
        this.shareId = parseInt(this.route.snapshot.params['shareid'], 10);
        this.year = parseInt(this.route.snapshot.params['year'], 10);
        this.registerId = parseInt(this.route.snapshot.params['registerid'], 10);


        this.register10DataItem = new Register10SectionViewModel();
        this.sectionList = [
            new Section(1, "I. Доли в уставном (складочном) капитале (фонде)*", 0),
            new Section(2, "II. Паи в паевых фондах кооперативов и паевых инвестиционных фондах*", 0),
            new Section(3, "III. Ценные бумаги *", 0),
            new Section(4, "IV. Производные финансовые инструменты", 0)];
        this.selectedSection = new Section(null, null, 0);
        this.isNew = false;

    }

    ngOnInit(): void {
        var self = this;
        this.registerService.getRegisterById(this.registerId).then(result => {
            this.isLoading = false;
            this.model = result;
            if (result) {
                this.model = result;
                this.isCreate = true;
                /* result.Register10Data.forEach((item, i, arr) => {
                     item.SectionId = i;
                     item.SectionTitle = self.titles[i];
                 });*/
                // this.addItem(12, 'Итого');
            } else {
                this.initializeModel();
            }
        });
    }
    getSum()
    { }

    initializeModel(): void {
        this.model = new Register10ViewModel();
        this.model.Year = this.year;
        this.model.OwnerProjectCompanyId = this.shareId;
        this.model.Type = RegisterType.Register10;
        //this.model.Currency = this.C

        this.isNew = true;

        this.model.Register10Data.length = 0;
        this.register10DataItem = new Register10SectionViewModel();
        this.register10DataItem.SectionId = this.selectedSection.id;
    }



    addRow(sectionId) {
        this.modalOpen(null, sectionId);
    }

    addItem(index: number, title: string): void {
        this.register10DataItem = new Register10SectionViewModel();
        this.register10DataItem.SectionTitle = title;
        this.register10DataItem.SectionId = +index;
        this.register10DataItem.AssetType = "";
        this.register10DataItem.CauseDisposal = "";
        this.register10DataItem.Num1 = 0;
        this.register10DataItem.Num2 = 0;
        this.register10DataItem.Num3 = 0;
        this.register10DataItem.Num4 = 0;
        this.register10DataItem.Register10Id = this.model.Id;
        this.model.Register10Data = [...this.model.Register10Data, this.register10DataItem];
    }


    updateData(): void {
        this.isLoading = true;
        this.isNew = false;
        this.registerService.getRegisterById(this.registerId).then(result => {
            if (result) {
                this.model = result;
                this.register10DataItem = new Register10SectionViewModel();
                //this.calculateSummary();
                this.isLoading = false;
                this.isNew = false;
            } else {
                this.initializeModel();
                this.isLoading = false;
            }
        });
    }

    back(): void {
        this.router.navigate(['../../../'], { relativeTo: this.route });
    }

    modalOpen(itemId: number, sectionId: number): void {

        let s = this.sectionList.filter(s => s.id === (+sectionId))[0];
        this.selectedSection = new Section(s.id, s.name, 0);
        if (itemId == null) {
            this.isCreate = true;
            //this.addItem(this.selectedSection.id, this.selectedSection.name);
            this.modal.open();
        } else {
            this.isCreate = false;
            this.register10DataItem = this.model.Register10Data.filter(s => s.Id === itemId)[0];

            //this.register9DataItem.LastYearDividendPaymentData = moment(this.model.Register9Data[dataIndex].LastYearDividendPaymentData).format('MM/DD/YYYY'); //moment().format('D MMM YYYY'); this.model.Register9Data[dataIndex].LastYearDividendPaymentData;
            //this.register9DataItem.CurrentYearDividendPaymentData = this.model.Register9Data[dataIndex].CurrentYearDividendPaymentData;
            //this.register9DataItem.CurrentYearTransitionalDividendPaymentData = this.model.Register9Data[dataIndex].CurrentYearDividendPaymentData;
            this.modal.open();
        }
    }

    modalSave(): void {

        let data = this.register10DataItem;
        data.SectionId = this.selectedSection.id;
        data.Register10Id = this.model.Id;
        if (!this.isCreate) {
            this.registerService.updateRegisterData(data).then(result => {
                this.register10DataItem = new Register10SectionViewModel();
                //this.register9DataItem.CurrentYearDividendPaymentData = null;
                //this.register9DataItem.CurrentYearTransitionalDividendPaymentData = null;
                //this.register9DataItem.LastYearDividendPaymentData = null;
                this.updateData();
                this.modal.dismiss();
            });
        }
        else {
            this.registerService.createRegisterData(data).then(result => {
                this.register10DataItem = new Register10SectionViewModel();
                //this.register9DataItem.CurrentYearDividendPaymentData = null;
                //this.register9DataItem.CurrentYearTransitionalDividendPaymentData = null;
                //this.register9DataItem.LastYearDividendPaymentData = null;
                this.updateData();
                this.modal.dismiss();
            });
        }

    }


    modalClose(): void {
        this.modal.dismiss();
    }

    delete(itemId: number): void {
        if (itemId != null) {
            this.register10DataItem = this.model.Register10Data.filter(s => s.Id === itemId)[0];
            this.registerService.deleteRegisterData(this.register10DataItem).then(result => {
                this.register10DataItem = new Register10SectionViewModel();
                this.updateData();
                this.modal.dismiss();
            });

        }
    }

    save(): void {
        this.isLoading = true;

        if (this.isNew) {
            this.registerService.createRegister(this.model).then(result => {
                this.isLoading = false;
                this.isNew = false;
                this.model = result;
                this.registerId = result.Id;
            });
        }
        else {
            this.registerService.updateRegister(this.model).then(result => {
                this.isLoading = false;
            });
        }
    }


    round(value:number, precision:number):number {
        let multiplier = Math.pow(10, precision || 0);
        return Math.round(value * multiplier) / multiplier;
}

    //todo: REFACTORING
    calculateGroupTotal1(sectionId: number): number {
        if (this.model.Register10Data.length !== 0) {
            if (sectionId === -1)
                return this.model.Register10Data.map(c => c.Num1).reduce((sum, current) => (+sum) + (+current));
            return this.model.Register10Data.map(c => c.SectionId === sectionId ? c.Num1 : 0).reduce((sum, current) => (+sum) + (+current));
        }
        return 0;
    }

    calculateGroupTotal2(sectionId: number): number {
        if (this.model.Register10Data.length !== 0) {
            if (sectionId === -1)
                    this.model.Register10Data.map(c => c.Num2).reduce((sum, current) => (+sum) + (+current));
            return this.model.Register10Data.map(c => c.SectionId === sectionId ? c.Num2 : 0)
                .reduce((sum, current) => (+sum) + (+current));
        }
        return 0;
    }

    calculateGroupTotal3(sectionId: number): number {
        if (this.model.Register10Data.length !== 0) {
            if (sectionId === -1)
                return this.model.Register10Data.map(c => c.Num3).reduce((sum, current) => (+sum) + (+current));
            return this.model.Register10Data.map(c => c.SectionId === sectionId ? c.Num3 : 0).reduce((sum, current) => (+sum) + (+current));
        }
        return 0;
    }

    calculateGroupTotal4(sectionId: number): number {
        if (this.model.Register10Data.length !== 0) {
            if (sectionId === -1)
                return this.model.Register10Data.map(c => c.Num4).reduce((sum, current) => (+sum) + (+current));
            return this.model.Register10Data.map(c => c.SectionId === sectionId ? c.Num4 : 0).reduce((sum, current) => (+sum) + (+current));
        }
        return 0;
    }


    /*   addNewRow(sectionId: number) {
          let item = new Register10SectionViewModel();
          item.SectionTitle = "";
          item.SectionId = sectionId;
          item.AssetType = null;
          item.CauseDisposal = null;
          item.Num1 = 0;
          item.Num2 = 0;
          item.Num3 = 0;
          item.Num4 = 0;
  
          this.model.Register10Data.push(item);
      }*/
}