"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var router_1 = require('@angular/router');
var company_service_1 = require('../company.service');
var CompanyListComponent = (function () {
    function CompanyListComponent(companyService, route) {
        this.companyService = companyService;
        this.route = route;
    }
    CompanyListComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.isLoading = true;
        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
        this.companyService.getCompaniesByProjectId(this.projectId).then(function (result) {
            _this.companyList = result;
            _this.isLoading = false;
        });
    };
    CompanyListComponent.prototype.onCalculateBtnClick = function () {
        var _this = this;
        this.companyService.calculateProjectCompanyInfo(this.projectId).then(function (result) {
            _this.isLoading = false;
        });
    };
    CompanyListComponent = __decorate([
        core_1.Component({
            template: require('./company-list.component.html'),
            providers: [company_service_1.CompanyService]
        }), 
        __metadata('design:paramtypes', [company_service_1.CompanyService, router_1.ActivatedRoute])
    ], CompanyListComponent);
    return CompanyListComponent;
}());
exports.CompanyListComponent = CompanyListComponent;
//# sourceMappingURL=company-list.component.js.map