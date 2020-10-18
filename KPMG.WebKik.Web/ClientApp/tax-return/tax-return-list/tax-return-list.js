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
var tax_return_service_1 = require('../tax-return.service');
var tax_return_model_1 = require('../models/tax-return.model');
var TaxReturnListComponent = (function () {
    function TaxReturnListComponent(dataService, route) {
        this.dataService = dataService;
        this.route = route;
    }
    TaxReturnListComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.isLoading = true;
        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
        this.dataService.getByProjectId(this.projectId).then(function (result) {
            _this.notificationList = result.map(function (item) {
                return Object.assign(new tax_return_model_1.TaxReturnViewModel(), item);
            });
            _this.isLoading = false;
        });
    };
    TaxReturnListComponent = __decorate([
        core_1.Component({
            template: require('./notification-of-participation-list.component.html')
        }), 
        __metadata('design:paramtypes', [(typeof (_a = typeof tax_return_service_1.NotificationOfParticipationService !== 'undefined' && tax_return_service_1.NotificationOfParticipationService) === 'function' && _a) || Object, router_1.ActivatedRoute])
    ], TaxReturnListComponent);
    return TaxReturnListComponent;
    var _a;
}());
exports.TaxReturnListComponent = TaxReturnListComponent;
//# sourceMappingURL=tax-return-list.js.map