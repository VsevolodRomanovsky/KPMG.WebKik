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
var NotificationListComponent = (function () {
    function NotificationListComponent(notificationService, route) {
        this.notificationService = notificationService;
        this.route = route;
    }
    NotificationListComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.isLoading = true;
        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
        this.notificationService.getByProjectId(this.projectId).then(function (result) {
            _this.notificationList = result;
            _this.isLoading = false;
        });
    };
    NotificationListComponent = __decorate([
        core_1.Component({
            template: require('./notification-list.component.html')
        }), 
        __metadata('design:paramtypes', [notification_service_1.NotificationService, router_1.ActivatedRoute])
    ], NotificationListComponent);
    return NotificationListComponent;
}());
exports.NotificationListComponent = NotificationListComponent;
//# sourceMappingURL=notification-list.component.js.map