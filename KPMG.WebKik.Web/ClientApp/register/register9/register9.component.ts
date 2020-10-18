import { Component, OnInit, ViewChild, ViewChildren, AfterViewInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Register9ViewModel } from '../models/register9.model';
import { Register9DataViewModel } from '../models/register9data.model';
import { RegisterType } from '../models/register-type.model';
import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';


import { Register9Service } from '../services/register9.service';
import * as moment from 'moment';

@Component({
	template: require('./register9.component.html'),
	styles: [require('./register9.component.scss')],
	providers: []
})
export class Register9Component implements OnInit {
	@ViewChild('myModal') modal: ModalComponent;

	model: Register9ViewModel;
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
	register9DataItem: Register9DataViewModel;
    register9DataList: Array<Register9DataViewModel>;

	LastYearDividendSumAggr: number;
	CurrentYearTransitionalDividendSumAggr: number;
	CurrentYearDividendSumAgg: number;
	Summary: number;
	SummaryYear: number;
	dateOptions: any;
	format: any;

	constructor(private route: ActivatedRoute, private router: Router, private registerService: Register9Service) { }

	ngOnInit(): void {
		this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
		this.companyId = parseInt(this.route.snapshot.params['companyid'], 10);
		this.shareId = parseInt(this.route.snapshot.params['shareid'], 10);
		this.year = parseInt(this.route.snapshot.params['year'], 10);
		this.registerId = parseInt(this.route.snapshot.params['registerid'], 10);
		//this.dateOptions = {
		//	formatYear: 'yyyy',
		//	startingDay: 1
		//};
		this.format = 'dd.MM.yyyy';
		this.updateData();
	}

	updateData(): void {
		this.isLoading = true;
		//this.isNew = false;
		this.registerService.getRegisterById(this.registerId).then(result => {
			if (result) {
				this.model = result; 
				this.model.Summary = 0;
				this.model.SummaryYear = 0;
				this.register9DataItem = new Register9DataViewModel();
				this.calculateSummary();
				this.isLoading = false;
				this.isNew = false;
			} else {
				this.initializeModel();
				this.isLoading = false;
			}
		});
	}


	initializeModel(): void {
		this.model = new Register9ViewModel();
		this.model.Year = this.year;
		this.model.OwnerProjectCompanyId = this.shareId;
		this.model.Type = RegisterType.Register9;
		this.model.Summary = 0;
		this.model.SummaryYear = 0;
		this.isNew = true;
		
		this.model.Register9Data.length = 0;
		this.register9DataItem = new Register9DataViewModel();
		this.register9DataItem.CountryCodeId = 0;
		this.calculateSummary();
	}

	Save(): void {
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

	Back(): void {
		this.router.navigate(['../../../'], { relativeTo: this.route });
	}

	ModalClose(): void {
		this.modal.dismiss();
	}

	ModalSave(): void {
		let data = this.register9DataItem;
		if (!this.isCreate) {
			this.registerService.updateRegisterData(data).then(result => {
				this.register9DataItem = new Register9DataViewModel();
				//this.register9DataItem.CurrentYearDividendPaymentData = null;
				//this.register9DataItem.CurrentYearTransitionalDividendPaymentData = null;
				//this.register9DataItem.LastYearDividendPaymentData = null;
				this.updateData();
				this.modal.dismiss();
			});
		}
		else {
			this.registerService.createRegisterData(data).then(result => {
				this.register9DataItem = new Register9DataViewModel();
				//this.register9DataItem.CurrentYearDividendPaymentData = null;
				//this.register9DataItem.CurrentYearTransitionalDividendPaymentData = null;
				//this.register9DataItem.LastYearDividendPaymentData = null;
				this.updateData();
				this.modal.dismiss();
			});
		}
	}
	ModalOpen(dataIndex: number): void {
		if (dataIndex == null) {
			this.isCreate = true;
			this.register9DataItem = new Register9DataViewModel();
			this.register9DataItem.Register9Id = this.model.Id;
			this.modal.open();
		} else {
			this.isCreate = false;
			this.register9DataItem = this.model.Register9Data[dataIndex];

			//this.register9DataItem.LastYearDividendPaymentData = moment(this.model.Register9Data[dataIndex].LastYearDividendPaymentData).format('MM/DD/YYYY'); //moment().format('D MMM YYYY'); this.model.Register9Data[dataIndex].LastYearDividendPaymentData;
			//this.register9DataItem.CurrentYearDividendPaymentData = this.model.Register9Data[dataIndex].CurrentYearDividendPaymentData;
			//this.register9DataItem.CurrentYearTransitionalDividendPaymentData = this.model.Register9Data[dataIndex].CurrentYearDividendPaymentData;
			this.modal.open();
		}
	}

	Delete(dataIndex: number): void {
		if (dataIndex != null) {
			this.register9DataItem = this.model.Register9Data[dataIndex];
			this.registerService.deleteRegisterData(this.register9DataItem).then(result => {
				this.register9DataItem = new Register9DataViewModel();
				this.updateData();
				this.modal.dismiss();
			});

		}
	}

	calculateSummary(): void {
		let summary: number;
		let summaryYear: number;
		let currentYearDividendSumAggr: number;
		let lastYearDividendSumAggr: number;
		let currentYearTransitionalDividendSumAggr: number;
		summary = 0;
		summaryYear = 0;
		currentYearDividendSumAggr = 0;
		lastYearDividendSumAggr = 0;
		currentYearTransitionalDividendSumAggr = 0;

		this.model.Register9Data.forEach(function (item, i, arr) {
			if (item.LastYearDividendSum) {
				lastYearDividendSumAggr += +item.LastYearDividendSum;
			}
			if (item.CurrentYearDividendSum) {
				currentYearDividendSumAggr += +item.CurrentYearDividendSum;
			}
			if (item.CurrentYearTransitionalDividendSum) {
				currentYearTransitionalDividendSumAggr += +item.CurrentYearTransitionalDividendSum;
			}

			item.Summary = +item.CurrentYearDividendSum + +item.CurrentYearTransitionalDividendSum + +item.LastYearDividendSum;
			item.SummaryYear = +item.CurrentYearDividendSum + +item.CurrentYearTransitionalDividendSum;
			if (item.Summary != null) {
				summary += +item.Summary;
			}
			if (item.SummaryYear != null) {
				summaryYear += +item.SummaryYear;
			}
		});
		this.model.Summary = summary;
		this.model.SummaryYear = summaryYear;
		this.model.CurrentYearDividendSumAggr = currentYearDividendSumAggr;
		this.model.LastYearDividendSumAggr = lastYearDividendSumAggr;
		this.model.CurrentYearTransitionalDividendSumAggr = currentYearTransitionalDividendSumAggr;
	}
	

	Calculate(): void {
		this.calculateSummary();
	}
}