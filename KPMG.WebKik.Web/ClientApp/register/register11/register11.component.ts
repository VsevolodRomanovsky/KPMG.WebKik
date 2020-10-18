import { Component, OnInit, Pipe, PipeTransform, ViewChild, ViewChildren, AfterViewInit} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Register11ViewModel } from '../models/register11.model';
import { Register11DataViewModel } from '../models/register11data.model ';

import { RegisterType } from '../models/register-type.model';
import { ModalComponent } from 'ng2-bs3-modal/ng2-bs3-modal';

import { Register11Service } from '../services/register11.service';
import { KeyValue } from '../../shared/models/key-value.model';
import { Section } from '../models/registerSection.model';
import { FilterPipe1 } from './register11.pipe';

import * as moment from 'moment';

@Component({
	template: require('./register11.component.html'),
	styles: [require('./register11.component.scss')],
	providers: []
})
export class Register11Component implements OnInit {
	@ViewChild('myModal') modal: ModalComponent;

	model: Register11ViewModel;
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
	register11DataItem: Register11DataViewModel;
	register11DataList: Array<Register11ViewModel>;
	sectionList: Array<Section>;
	selectedSection: Section;

	dateOptions: any;
	format: any;

	constructor(private route: ActivatedRoute, private router: Router, private registerService: Register11Service) {

	}

	ngOnInit(): void {
		this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
		this.companyId = parseInt(this.route.snapshot.params['companyid'], 10);
		this.shareId = parseInt(this.route.snapshot.params['shareid'], 10);
		this.year = parseInt(this.route.snapshot.params['year'], 10);
		this.registerId = parseInt(this.route.snapshot.params['registerid'], 10);

		this.format = 'dd.MM.yyyy';

		this.sectionList = [
			new Section(1, "I. Ценные бумаги", 0),
			new Section(2, "II.1 Имущественные права, в том числе доли", 0),
			new Section(3, "II.2 Паи", 0),
			new Section(4, "II.3 Иные имущественные права", 0)];

		this.selectedSection = new Section(null, null, 0);

		this.updateData();
	}

	updateData(): void {
		this.isLoading = true;
		//this.isNew = false;
		this.registerService.getRegisterById(this.registerId).then(result => {
			if (result) {
				this.model = result;
			//	this.model.Summary = 0;
				this.register11DataItem = new Register11DataViewModel();
				this.register11DataItem.Register11DataTypeId = this.selectedSection.id;
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
		this.model = new Register11ViewModel();
		this.model.Year = this.year;
		this.model.OwnerProjectCompanyId = this.shareId;
		this.model.Type = RegisterType.Register9;
		//this.model.Summary = 0;
		this.isNew = true;
		
		this.model.Register11Data.length = 0;
		this.register11DataItem = new Register11DataViewModel();
		this.calculateSummary();
	}

	addRow(sectionId) {
	//	let t = this.calculateGroupTotal1();
		this.modalOpen(null, sectionId);
	}

	modalOpen(dataIndex: number, sectionId: number): void {
		if (dataIndex == null) {
			let s = this.sectionList.filter(s => s.id === (+sectionId))[0];
			this.selectedSection = new Section(s.id, s.name, 0);

			this.isCreate = true;

			this.modal.open();
		} else {
			this.isCreate = false;
			this.register11DataItem = this.model.Register11Data[dataIndex];
			this.modal.open();
		}
	}

	modalSave(): void {
		let data = this.register11DataItem;
		data.Register11DataTypeId = this.selectedSection.id;
		data.Register11Id = this.model.Id;
		//this.addItem(1, "");
		if (this.isCreate) {
			this.registerService.createRegisterData(data).then(result => {
				this.exit();
				
			});
		}
		else 
		{
			this.registerService.updateRegisterData(data).then(result => {
				this.exit();
			});
		}
	}

	modalClose(): void {
		this.modal.close(null);
	}

	delete(dataIndex: number): void {
		if (dataIndex != null) {
			this.register11DataItem = this.model.Register11Data[dataIndex];
			this.registerService.deleteRegisterData(this.register11DataItem).then(result => {
				this.register11DataItem = new Register11DataViewModel();
				this.updateData();
			});

		}
	}

	exit(): void {
		this.register11DataItem = new Register11DataViewModel();
		this.updateData();
		this.modal.dismiss();
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

	calculateGroupTotal1(sectionId: number): number {
		if (this.model.Register11Data.length !== 0) {
			if (sectionId == -1)
				return this.model.Register11Data.map(c => c.IncomeFromRealizationOfAssetSummary).reduce((sum, current) => (+sum) + (+current));
			return this.model.Register11Data.map(c => c.Register11DataTypeId === sectionId ? c.IncomeFromRealizationOfAssetSummary : 0).reduce((sum, current) => (+sum) + (+current));
		}
		return 0;
	}

	calculateGroupTotal2(sectionId: number): number {
		if (this.model.Register11Data.length !== 0) {
		if (sectionId == -1)
			return this.model.Register11Data.map(c => c.IncomeFromRealizationOfAssetSellPrice).reduce((sum, current) => (+sum) + (+current));
		return this.model.Register11Data.map(c => c.Register11DataTypeId === sectionId ? c.IncomeFromRealizationOfAssetSellPrice : 0).reduce((sum, current) => (+sum) + (+current));
		}
		return 0;
	}

	calculateGroupTotal3(sectionId: number): number {
		if (this.model.Register11Data.length !== 0) {
		if (sectionId == -1)
			return this.model.Register11Data.map(c => c.IncomeFromRealizationOfAssetOthers).reduce((sum, current) => (+sum) + (+current));
		return this.model.Register11Data.map(c => c.Register11DataTypeId === sectionId ? c.IncomeFromRealizationOfAssetOthers : 0).reduce((sum, current) => (+sum) + (+current));
		}
		return 0;
	}

	calculateGroupTotal4(sectionId: number): number {
		if (this.model.Register11Data.length !== 0) {
		if (sectionId == -1)
			return this.model.Register11Data.map(c => c.MarketValue).reduce((sum, current) => (+sum) + (+current));
		return this.model.Register11Data.map(c => c.Register11DataTypeId === sectionId ? c.MarketValue : 0).reduce((sum, current) => (+sum) + (+current));
		}
		return 0;
		}


	calculateGroupTotal5(sectionId: number): number {
		if (this.model.Register11Data.length !== 0) {
		if (sectionId == -1)
			return this.model.Register11Data.map(c => c.CostForTransitionOfPropertyRightDateSummary).reduce((sum, current) => (+sum) + (+current));
		return this.model.Register11Data.map(c => c.Register11DataTypeId === sectionId ? c.CostForTransitionOfPropertyRightDateSummary : 0).reduce((sum, current) => (+sum) + (+current));
		}
		return 0;
		}


	calculateGroupTotal6(sectionId: number): number {
		if (this.model.Register11Data.length !== 0) {
		if (sectionId == -1)
			return this.model.Register11Data.map(c => c.CostForTransitionOfPropertyRightDateAcquisitionPrice).reduce((sum, current) => (+sum) + (+current));
		return this.model.Register11Data.map(c => c.Register11DataTypeId === sectionId ? c.CostForTransitionOfPropertyRightDateAcquisitionPrice : 0).reduce((sum, current) => (+sum) + (+current));
		}
		return 0;
		}


	calculateGroupTotal7(sectionId: number): number {
		if (this.model.Register11Data.length !== 0) {
		if (sectionId == -1)
			return this.model.Register11Data.map(c => c.CostForTransitionOfPropertyRightDateRevaluationSummary).reduce((sum, current) => (+sum) + (+current));
		return this.model.Register11Data.map(c => c.Register11DataTypeId === sectionId ? c.CostForTransitionOfPropertyRightDateRevaluationSummary : 0).reduce((sum, current) => (+sum) + (+current));
		}
		return 0;
		}


	calculateGroupTotal8(sectionId: number): number {
		if (this.model.Register11Data.length !== 0) {
		if (sectionId == -1)
			return this.model.Register11Data.map(c => c.CostForTransitionOfPropertyRightDateRevaluationForCurrentYear).reduce((sum, current) => (+sum) + (+current));
		return this.model.Register11Data.map(c => c.Register11DataTypeId === sectionId ? c.CostForTransitionOfPropertyRightDateRevaluationForCurrentYear : 0).reduce((sum, current) => (+sum) + (+current));
		}
		return 0;
		}

	calculateIncomeExcludedFromProfitLoss(id: number) {
		if (id)
		{
			let item = this.model.Register11Data.filter(it => (+it.Id) === (+id))[0]; //[index];
			if (item.IncomeFromRealizationOfAssetSellPrice && item.CostForTransitionOfPropertyRightDateSummary && item.MarketValue) {
				if (item.IncomeFromRealizationOfAssetSellPrice == item.CostForTransitionOfPropertyRightDateSummary &&
					item.IncomeFromRealizationOfAssetSellPrice <= item.MarketValue) {
					return item.IncomeFromRealizationOfAssetSellPrice;
				}
			}
		}
		return;
	}

	calculateExpenseExcludedFromProfitLoss(id: number) {
		if (id) {
			let item = this.model.Register11Data.filter(it => (+it.Id) === (+id))[0];
			if (item.IncomeFromRealizationOfAssetSellPrice && item.CostForTransitionOfPropertyRightDateSummary && item.MarketValue) {
				if (item.IncomeFromRealizationOfAssetSellPrice == item.CostForTransitionOfPropertyRightDateSummary &&
					item.IncomeFromRealizationOfAssetSellPrice <= item.MarketValue) {
					return item.CostForTransitionOfPropertyRightDateAcquisitionPrice;
				}
			}
		}
		return;
	}

	calculateIncomeExcludedFromProfitLossTotal(sectionId: number) {
		let items;
		if (sectionId == -1) {
			items = this.model.Register11Data;
		} else {
			items = this.model.Register11Data.filter(it => (+it.Register11DataTypeId) === (+sectionId));
		}
		let result: number;
		result = 0;
		items.forEach(function (item, i, arr) {
			if (item.IncomeFromRealizationOfAssetSellPrice && item.CostForTransitionOfPropertyRightDateSummary && item.MarketValue) {
				if (item.IncomeFromRealizationOfAssetSellPrice == item.CostForTransitionOfPropertyRightDateSummary &&
					item.IncomeFromRealizationOfAssetSellPrice <= item.MarketValue) {
					result += +item.IncomeFromRealizationOfAssetSellPrice;
				}
			}
		});
		return result != 0 ? result : 'X';
	}

	calculateExpenseExcludedFromProfitLossTotal(sectionId: number) {
		let items;
		if (sectionId == -1) {
			items = this.model.Register11Data;
		} else {
			items = this.model.Register11Data.filter(it => (+it.Register11DataTypeId) === (+sectionId));;
		}
		let result: number;
		result = 0;
		items.forEach(function (item, i, arr) {
			if (item.IncomeFromRealizationOfAssetSellPrice && item.CostForTransitionOfPropertyRightDateSummary && item.MarketValue) {
				if (item.IncomeFromRealizationOfAssetSellPrice == item.CostForTransitionOfPropertyRightDateSummary &&
					item.IncomeFromRealizationOfAssetSellPrice <= item.MarketValue) {
					result += +item.CostForTransitionOfPropertyRightDateAcquisitionPrice;
				}
			}
		});
		return result != 0 ? result : 'X';;
	}

	calculateSummary(): void {
	}
	

	Calculate(): void {
		this.calculateSummary();
	}
}