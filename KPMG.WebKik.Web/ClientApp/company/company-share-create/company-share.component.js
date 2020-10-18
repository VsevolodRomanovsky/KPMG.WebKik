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
var project_company_model_1 = require('../models/project-company.model');
var company_share_model_1 = require('../models/company-share.model');
var state_model_1 = require('../models/state.model');
var CompanyShareComponent = (function () {
    function CompanyShareComponent(companyService, route, _router) {
        this.companyService = companyService;
        this.route = route;
        this._router = _router;
        this.isAddedShare = false;
        this.isActive = true;
        this.isIndividualCompany = false;
    }
    CompanyShareComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.model = new company_share_model_1.ProjectCompanyShareViewModel();
        this.model.OwnerProjectCompany = new project_company_model_1.ProjectCompanyViewModel();
        this.model.DependentProjectCompany = new project_company_model_1.ProjectCompanyViewModel();
        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
        this.companyId = parseInt(this.route.snapshot.params['companyid'], 10);
        var loadDataTasks = [
            this.companyService.getCompaniesByProjectId(this.projectId),
            this.companyService.getCompanyStates()
        ];
        Promise.all(loadDataTasks).then(function (results) {
            _this.companyList = results[0];
            _this.companyStates = results[1];
            var currentCompany = _this.companyList.find(function (c) { return c.Id == _this.companyId; });
            _this.model.OwnerProjectCompany = currentCompany;
            _this.isIndividualCompany = _this.model.OwnerProjectCompany.State == state_model_1.State.Individual;
            _this.model.OwnerProjectCompanyId = currentCompany.Id;
            _this.companyList.splice(_this.companyList.indexOf(currentCompany), 1);
            _this.stateText = _this.companyStates.find(function (state) {
                return _this.model.OwnerProjectCompany.State == state.Key;
            }).Value;
            _this.isLoading = false;
        });
    };
    CompanyShareComponent.prototype.onSubmit = function (selected) {
        var _this = this;
        var selectedCompany = selected.value;
        var temp = this.companyList.find(function (c) { return c.Id == selectedCompany; });
        this.model.DependentProjectCompanyId = this.companyList.find(function (c) { return c.Id == selectedCompany; }).Id;
        this.model.CompanyStatus = "STATUS";
        this.isLoading = true;
        this.companyService.createCompanyShare(this.model).then(function (result) {
            _this.isLoading = false;
            _this.isAddedShare = true;
            _this.isActive = false;
            _this.model.Id = result.Id;
            _this._router.navigate(['../control'], { relativeTo: _this.route });
        });
    };
    CompanyShareComponent = __decorate([
        core_1.Component({
            template: require('./company-share.component.html'),
            providers: [company_service_1.CompanyService]
        }), 
        __metadata('design:paramtypes', [company_service_1.CompanyService, router_1.ActivatedRoute, router_1.Router])
    ], CompanyShareComponent);
    return CompanyShareComponent;
}());
exports.CompanyShareComponent = CompanyShareComponent;
//# sourceMappingURL=company-share.component.js.map