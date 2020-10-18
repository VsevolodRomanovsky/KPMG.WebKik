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
var signatory_service_1 = require('../signatory.service');
var signatory_model_1 = require('../models/signatory.model');
var SignatoryEditComponent = (function () {
    function SignatoryEditComponent(signatoryService, router, route) {
        this.signatoryService = signatoryService;
        this.router = router;
        this.route = route;
    }
    SignatoryEditComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.isLoading = true;
        var id = parseInt(this.route.snapshot.params['signatoryid'], 10);
        var loadTask;
        if (id == 0) {
            this.isNew = true;
            loadTask = Promise.resolve(new signatory_model_1.SignatoryViewModel());
        }
        else {
            loadTask = this.signatoryService.getById(id);
        }
        loadTask.then(function (result) {
            _this.model = result;
            _this.model.ProjectCompanyId = parseInt(_this.route.snapshot.params['companyid'], 10);
            _this.isLoading = false;
        });
    };
    SignatoryEditComponent.prototype.onSubmit = function () {
        var _this = this;
        this.isLoading = true;
        var successCallback = function () {
            _this.isLoading = false;
            _this.router.navigate(['../'], { relativeTo: _this.route });
        };
        if (this.isNew) {
            this.signatoryService.create(this.model).then(successCallback);
        }
        else {
            this.signatoryService.update(this.model).then(successCallback);
        }
    };
    Object.defineProperty(SignatoryEditComponent.prototype, "title", {
        get: function () { return this.isNew ? "Создание подписанта" : "Редактирование подписанта"; },
        enumerable: true,
        configurable: true
    });
    SignatoryEditComponent = __decorate([
        core_1.Component({
            template: require('./signatory-edit.component.html'),
        }), 
        __metadata('design:paramtypes', [signatory_service_1.SignatoryService, router_1.Router, router_1.ActivatedRoute])
    ], SignatoryEditComponent);
    return SignatoryEditComponent;
}());
exports.SignatoryEditComponent = SignatoryEditComponent;
//# sourceMappingURL=signatory-edit.component.js.map