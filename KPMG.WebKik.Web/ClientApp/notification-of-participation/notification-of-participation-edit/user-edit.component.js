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
var user_service_1 = require('../user.service');
var role_service_1 = require('../role.service');
var user_model_1 = require('../models/user.model');
var UserEditComponent = (function () {
    function UserEditComponent(userService, roleService, router, route) {
        this.userService = userService;
        this.roleService = roleService;
        this.router = router;
        this.route = route;
    }
    UserEditComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.isLoading = true;
        var id = parseInt(this.route.snapshot.params['userid'], 10);
        var loadUserTask;
        if (id == 0) {
            this.isNew = true;
            loadUserTask = Promise.resolve(new user_model_1.UserViewModel());
        }
        else {
            loadUserTask = this.userService.getUserById(id);
        }
        var loadDataTasks = [
            loadUserTask,
            this.roleService.getRoles()
        ];
        Promise.all(loadDataTasks).then(function (results) {
            _this.roles = results[1];
            _this.model = results[0];
            _this.model.Role = _this.roles.find(function (role) { return role.Id == _this.model.RoleId; });
            _this.isLoading = false;
        });
    };
    UserEditComponent.prototype.onUserLoginChange = function () {
        this.hasUserExistException = false;
    };
    UserEditComponent.prototype.onSubmit = function () {
        var _this = this;
        this.isLoading = true;
        this.hasUserExistException = false;
        this.model.RoleId = this.model.Role.Id;
        var successCallback = function () {
            _this.isLoading = false;
            _this.router.navigate(['../../'], { relativeTo: _this.route });
        };
        var errorCallback = function (error) {
            if (error.status == 400 && error.statusText === "UpdateException") {
                _this.hasUserExistException = true;
                _this.isLoading = false;
            }
        };
        if (this.isNew) {
            this.userService.createUser(this.model).then(successCallback).catch(errorCallback);
        }
        else {
            this.userService.updateUser(this.model).then(successCallback).catch(errorCallback);
        }
    };
    Object.defineProperty(UserEditComponent.prototype, "title", {
        get: function () { return this.isNew ? "Создание пользователя" : "Редактирование пользователя"; },
        enumerable: true,
        configurable: true
    });
    UserEditComponent = __decorate([
        core_1.Component({
            template: require('./user-edit.component.html'),
        }), 
        __metadata('design:paramtypes', [user_service_1.UserService, role_service_1.RoleService, router_1.Router, router_1.ActivatedRoute])
    ], UserEditComponent);
    return UserEditComponent;
}());
exports.UserEditComponent = UserEditComponent;
//# sourceMappingURL=user-edit.component.js.map