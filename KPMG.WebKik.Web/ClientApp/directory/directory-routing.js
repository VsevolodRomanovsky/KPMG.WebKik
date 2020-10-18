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
var directory_list_component_1 = require('./directory-list/directory-list.component');
var directory_edit_component_1 = require('./directory-edit/directory-edit.component');
var directory_auth_service_1 = require('./directory-auth.service');
var routes = [
    {
        path: '',
        component: directory_list_component_1.DirectoryListComponent,
        canActivate: [directory_auth_service_1.DirectoryAuthService],
        children: [
            {
                path: ':directory',
                canActivateChild: [directory_auth_service_1.DirectoryAuthService],
                component: directory_edit_component_1.DirectoryEditComponent
            }
        ]
    }
];
var DirectoryRoutingModule = (function () {
    function DirectoryRoutingModule() {
    }
    DirectoryRoutingModule = __decorate([
        core_1.NgModule({
            imports: [router_1.RouterModule.forChild(routes)],
            exports: [router_1.RouterModule]
        }), 
        __metadata('design:paramtypes', [])
    ], DirectoryRoutingModule);
    return DirectoryRoutingModule;
}());
exports.DirectoryRoutingModule = DirectoryRoutingModule;
//# sourceMappingURL=directory-routing.js.map