import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Register8ViewModel } from '../models/register8.model';
import { Register8ReservesViewModel } from '../models/register8reserves.model';
import { RegisterType } from '../models/register-type.model';

import { Register8Service } from '../services/register8.service';

@Component({
	template: require('./register8.component.html'),
	providers: []
})
export class Register8Component implements OnInit {
	model: Register8ViewModel;
	projectId: number;
	companyId: number;
	registerId: number;
	shareId: number;

	year: number;
	title: string;
	isLoading: boolean;
	isNew: boolean;
	sales: any[];
	cols: any[];
	rows: any[];
	columns: any[];

	constructor(private route: ActivatedRoute, private router: Router, private registerService: Register8Service) { }

	ngOnInit(): void {
		this.isLoading = true;

		this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
		this.companyId = parseInt(this.route.snapshot.params['companyid'], 10);
		this.shareId = parseInt(this.route.snapshot.params['shareid'], 10);
		this.year = parseInt(this.route.snapshot.params['year'], 10);
		this.registerId = parseInt(this.route.snapshot.params['registerid'], 10);

		this.isNew = false;
		this.registerService.getRegisterById(this.registerId).then(result => {
			this.isLoading = false;
			if (result) {
				this.model = result;
				let titles = ['Резерв на гарантийсное обслуживание', 'Резерв под обесценение дебиторской задолженности', 'Резерв по неиспользованным отпускам', 'Резерв на премиальные выплаты',
					'Резерв на пенсионное обеспечение', 'Резерв под судебные разбирательства', 'Резерв по проведению экологических мероприятий', 'Резерв по выводу из эксплуатации обьектов основных средств',
					'Резерв по обременительным договорам', 'Начисление на оплату назначенного штрафа', 'Прочие резервы, учтенные в финансовой отчетности', 'Итого'];
				result.Register8Data.forEach(function (item, i, arr) {
					item.ReserveTitle = titles[i];
					item.Register8DataTypeId = i;
				});
				this.addItem(12, 'Итого');
				this.Calculate();
			} else {
				this.initializeModel();
			}
		});
	}

	initializeModel(): void {
		this.model = new Register8ViewModel();
		this.model.Year = this.year;
		this.model.OwnerProjectCompanyId = this.shareId;
		this.model.Type = RegisterType.Register8;
		this.isNew = true;
		this.isLoading = false;
		this.model.Register8Data.length = 0;
		this.addItem(1, 'Резерв на гарантийсное обслуживание');
		this.addItem(2, 'Резерв под обесценение дебиторской задолженности');
		this.addItem(3, 'Резерв по неиспользованным отпускам');
		this.addItem(4, 'Резерв на премиальные выплаты');
		this.addItem(5, 'Резерв на пенсионное обеспечение');
		this.addItem(6, 'Резерв под судебные разбирательства');
		this.addItem(7, 'Резерв по проведению экологических мероприятий');
		this.addItem(8, 'Резерв по выводу из эксплуатации обьектов основных средств');
		this.addItem(9, 'Резерв по обременительным договорам');
		this.addItem(10, 'Начисление на оплату назначенного штрафа');
		this.addItem(11, 'Прочие резервы, учтенные в финансовой отчетности');
		this.addItem(12, 'Итого');
	}

	Save(): void {
		this.isLoading = true;

		if (this.isNew) {
			this.registerService.createRegister(this.model).then(result => {
				this.isLoading = false;
			});
		}
		else {
			this.registerService.updateRegister(this.model).then(result => {
				this.isLoading = false;
			});
		}

		this.router.navigate(['../../../'], { relativeTo: this.route });
	}

	Calculate(): void {
		let reserve1 = 0;
		let reserve2 = 0;
		let reserve3 = 0;
		let reserve4 = 0;
		var length = this.model.Register8Data.length;
		this.model.Register8Data.forEach(function (item, i, arr) {
			if (item.ExpensesFormationOfReserve && item.ExpensesReducedOfReserve) {
					item.ExpensesNotConsideredInProfit = item.ExpensesFormationOfReserve - item.ExpensesReducedOfReserve;
			}
			if (i < (length - 1)) {
				if (item.ExpensesFormationOfReserve) {
					reserve1 += +item.ExpensesFormationOfReserve;
				}
				if (item.ExpensesReducedOfReserve) {
					reserve2 += +item.ExpensesReducedOfReserve;
				}
				if (item.ExpensesNotConsideredInProfit) {
					reserve3 += +item.ExpensesNotConsideredInProfit;
				}
				if (item.IncomeFromRecoveryOfReserve) {
					reserve4 += +item.IncomeFromRecoveryOfReserve;
				}
			}
		});
		this.model.Register8Data[this.model.Register8Data.length - 1].ExpensesFormationOfReserve = reserve1;
		this.model.Register8Data[this.model.Register8Data.length - 1].ExpensesReducedOfReserve = reserve2;
		this.model.Register8Data[this.model.Register8Data.length - 1].ExpensesNotConsideredInProfit = reserve3;
		this.model.Register8Data[this.model.Register8Data.length - 1].IncomeFromRecoveryOfReserve = reserve4;
	}


	addItem(index, title): void {
		var item = new Register8ReservesViewModel();
		item.Register8DataTypeId = index;
		item.ReserveTitle = title;

		this.model.Register8Data.push(item);
	}


}