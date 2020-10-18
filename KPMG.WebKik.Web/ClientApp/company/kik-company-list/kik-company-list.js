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
var CompanyKikListComponent = (function () {
    function CompanyKikListComponent(companyService, route, _router) {
        this.companyService = companyService;
        this.route = route;
        this._router = _router;
    }
    CompanyKikListComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.isLoading = true;
        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
        this.companyId = parseInt(this.route.snapshot.params['companyid'], 10);
        Promise.all([
            this.companyService.getSharesByProjecCompanytId(this.companyId),
            this.companyService.getCompanyById(this.companyId)
        ]).then(function (results) {
            _this.shareList = results[0];
            _this.company = results[1];
            _this.isLoading = false;
        });
    };
    CompanyKikListComponent.prototype.removeShare = function (id) {
        var _this = this;
        this.companyService.removeProjectCompanyShare(id)
            .then(function (share) {
            var index = _this.shareList.indexOf(share);
            _this.shareList.splice(index, 1);
        });
    };
    CompanyKikListComponent = __decorate([
        core_1.Component({
            template: require('./company-kik-list.component.html'),
            providers: [company_service_1.CompanyService]
        }), 
        __metadata('design:paramtypes', [company_service_1.CompanyService, router_1.ActivatedRoute, router_1.Router])
    ], CompanyKikListComponent);
    return CompanyKikListComponent;
}());
exports.CompanyKikListComponent = CompanyKikListComponent;
//# sourceMappingURL=kik-company-list.js.map