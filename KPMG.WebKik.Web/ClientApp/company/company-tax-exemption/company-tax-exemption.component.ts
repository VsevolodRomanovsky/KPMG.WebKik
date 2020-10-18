import { Component, OnInit, Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { CompanyService } from '../company.service';
import { ProjectCompanyShareViewModel } from '../models/company-share.model';
import { RationalyType } from '../models/rationaly-type.model';
import { TaxExemtionsViewModel } from '../models/tax-exemption.model';
import { TaxExemptionResult } from '../models/tax-exemption-result.model'

@Component({
    template: require('./company-tax-exemption.component.html'),
    providers: [CompanyService]
})
export class CompanyTaxExemptionComponent implements OnInit {
    model: TaxExemtionsViewModel;
    isLoading: boolean;
    projectId: number;
    companyId: number;
    shareId: number;
    projectCompanyShareId: number;
    year: number;
    expression = {};
    status: string;
    rationalyList: Array<RationalyListItem> = [
        new RationalyListItem("Некоммерческая организация, которая в соответствии со своим личным законом не распределяет полученную прибыль (доход) между акционерами (участниками, учредителями) или иными лицами", RationalyType.NonProfitOrganization, false),
        new RationalyListItem("Оператор нового морского месторождения углеводородного сырья", RationalyType.OffshoreFieldOperator, false),
        new RationalyListItem("Непосредственный акционер (участник) оператора нового морского месторождения углеводородного сырья", RationalyType.OffshoreFieldAuctioneer, false),
        new RationalyListItem("Член ЕвразЭС", RationalyType.EurAsECMember, false),
        new RationalyListItem("Страхования организация, осуществляющая деятельность со своим личным законом, на основании лицензии или иного специального разрешения", RationalyType.InsuranceAgencyWithLexPersonalis, false),
        new RationalyListItem("Банк, осуществляющий деятельность в соответствии со своим личным законом, на основании лицензии или иного специального разрешения", RationalyType.BankWithLexPersonalis, false),
        new RationalyListItem("Эмитент обращающихся облигаций", RationalyType.TradedBondsIssuer, false),
        new RationalyListItem("Организация, которой были уступлены права и обязанности по выпущенным обращающимся облигациям", RationalyType.CededRightsOrganization, false),
        new RationalyListItem("Участник проектов по добыче полезных ископаемых, осуществляемых на основании заключаемых с иностранным государством (территорией) или с уполномоченными правительством такого государства (территории) организациями СРП, концессионных соглашений, лицензионных соглашений или сервисных соглашений (контрактов), аналогичных СРП, либо на основании иных аналогичных соглашений, заключаемых на условиях распределения риска", RationalyType.ProjectMemberMining, false),
        new RationalyListItem("По «ЭСНП»", RationalyType.ByESPN, false),
        new RationalyListItem("Активная иностранная компания", RationalyType.ActiveForeignCompany, false),
        new RationalyListItem("Активная холдинговая компания", RationalyType.ActiveHoldingCompany, false),
        new RationalyListItem("Активная субхолдинговая компания", RationalyType.ActiveSubholdingCompany, false)]
    years: Array<number> = [2015, 2016, 2017];
    selectedYear: number = 2016;
    isExpected: boolean;
    isNotEnoughData: boolean;
    result: Object;

    constructor(private companyService: CompanyService, private route: ActivatedRoute, private _router: Router) {

    }

    ngOnInit(): void {
        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
        this.companyId = parseInt(this.route.snapshot.params['companyid'], 10);
        this.shareId = parseInt(this.route.snapshot.params['shareid'], 10);

        this.reload();
    }

    onSubmit(): void {
        console.log('onSubmit');
        this.updateTaxExemptionList();
        this.companyService.defineTaxStatus(this.model).then(result => {
            this.isExpected = result.IsExempted;
            this.isNotEnoughData = result.IsNotEnoughData;
            this.result = this.isNotEnoughData ? { text: "Недостаточно данных для расчета", style: "warning" } 
                : this.isExpected ? { text: "Освобожден", style: "success" } : { text: "Не освобожден", style: "danger" };
            
        });
    }

    onChange(value, event) {
        this.rationalyList.find(x => x.Type == value.Type).IsChecked = event.target.checked;
    }

    updateTaxExemptionList(): void {
        this.model.Rationaly = this.rationalyList.filter(x => x.IsChecked).map(x => x.Type);
    }

    reload(): void {
        this.isLoading = true;
        this.result = null;
        this.companyService.getTaxStatus(this.companyId, this.shareId, this.selectedYear).then(result => {
            this.model = result;
            for (var item of this.rationalyList) {
                item.IsChecked = this.model.Rationaly.find(x => x == item.Type) != null;
            }
            this.isLoading = false;
        });
    }

    onSelected(value): void {
        this.selectedYear = value;
        this.reload();
    }

}

export
    class RationalyListItem {
    public Name: string;
    public Type: RationalyType;
    public IsChecked: boolean;

    constructor(name: string, type: RationalyType, isChecked: boolean) {
        this.Name = name;
        this.Type = type;
        this.IsChecked = isChecked;
    }
}



