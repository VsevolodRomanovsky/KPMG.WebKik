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
var notification_service_1 = require('../notification.service');
var notification_model_1 = require('../models/notification.model');
var NotificationEditComponent = (function () {
    function NotificationEditComponent(notificationService, router, route) {
        this.notificationService = notificationService;
        this.router = router;
        this.route = route;
    }
    NotificationEditComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.isLoading = true;
        var id = parseInt(this.route.snapshot.params['notificationid'], 10);
        var loadTask;
        if (id == 0) {
            this.isNew = true;
            loadTask = Promise.resolve(new notification_model_1.NotificationViewModel());
        }
        else {
            loadTask = this.notificationService.getById(id);
        }
        loadTask.then(function (result) {
            _this.model = result;
            _this.model.ProjectCompanyId = parseInt(_this.route.snapshot.params['companyid'], 10);
            _this.isLoading = false;
        });
    };
    NotificationEditComponent.prototype.onSubmit = function () {
        var _this = this;
        this.isLoading = true;
        var successCallback = function () {
            _this.isLoading = false;
            _this.router.navigate(['../'], { relativeTo: _this.route });
        };
        if (this.isNew) {
            this.notificationService.create(this.model).then(successCallback);
        }
        else {
            this.notificationService.update(this.model).then(successCallback);
        }
    };
    Object.defineProperty(NotificationEditComponent.prototype, "title", {
        get: function () { return this.isNew ? "Создание уведомления" : "Редактирование уведомления"; },
        enumerable: true,
        configurable: true
    });
    NotificationEditComponent = __decorate([
        core_1.Component({
            template: require('./signatory-edit.component.html'),
        }), 
        __metadata('design:paramtypes', [(typeof (_a = typeof notification_service_1.NotificationService !== 'undefined' && notification_service_1.NotificationService) === 'function' && _a) || Object, router_1.Router, router_1.ActivatedRoute])
    ], NotificationEditComponent);
    return NotificationEditComponent;
    var _a;
}());
exports.NotificationEditComponent = NotificationEditComponent;
//# sourceMappingURL=notification-edit.component.js.map