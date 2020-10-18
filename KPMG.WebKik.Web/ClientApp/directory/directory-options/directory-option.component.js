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
var directory_service_1 = require('../directory.service');
var directory_model_1 = require('../models/directory.model');
var DirectoryOptionsComponent = (function () {
    function DirectoryOptionsComponent(directoryService) {
        this.directoryService = directoryService;
        this.directory = new directory_model_1.DirectoryViewModel();
    }
    DirectoryOptionsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.directoryService
            .getByName(this.typeName)
            .then(function (result) {
            _this.directory = result;
            _this.directory.Entries.sort(function (a, b) {
                if (a.Name < b.Name)
                    return -1;
                if (a.Name > b.Name)
                    return 1;
                return 0;
            });
        });
    };
    __decorate([
        core_1.Input('directory-options'), 
        __metadata('design:type', String)
    ], DirectoryOptionsComponent.prototype, "typeName", void 0);
    DirectoryOptionsComponent = __decorate([
        core_1.Component({
            selector: '[directory-options]',
            template: require('./directory-option.component.html')
        }), 
        __metadata('design:paramtypes', [directory_service_1.DirectoryService])
    ], DirectoryOptionsComponent);
    return DirectoryOptionsComponent;
}());
exports.DirectoryOptionsComponent = DirectoryOptionsComponent;
//# sourceMappingURL=directory-option.component.js.map