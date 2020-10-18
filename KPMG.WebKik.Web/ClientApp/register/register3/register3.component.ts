import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Register3ViewModel } from '../models/register3.model';
import { RegisterType } from '../models/register-type.model';

import { Register3Service } from '../services/register3.service'


@Component({
    template: require('./register3.component.html'),
    providers: []
})
export class Register3Component implements OnInit {
    model: Register3ViewModel;
    projectId: number;
    companyId: number;
    registerId: number;
    shareId: number;
    year: number;
    isLoading: boolean;
    isNew: boolean;

    constructor(private route: ActivatedRoute, private router: Router, private registerService: Register3Service) { }

    ngOnInit(): void {
        this.isLoading = true;

        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
        this.companyId = parseInt(this.route.snapshot.params['companyid'], 10);
        this.shareId = parseInt(this.route.snapshot.params['shareid'], 10);
        this.year = parseInt(this.route.snapshot.params['year'], 10);
        this.registerId = parseInt(this.route.snapshot.params['registerid'], 10);

        if (this.registerId == 0) {
            this.model = new Register3ViewModel();
            this.model.Year = this.year;
            this.model.OwnerProjectCompanyId = this.shareId;
            this.model.Type = RegisterType.Register3;
            this.isNew = true;
            this.isLoading = false;
            return;
        }

        this.isNew = false;
        this.registerService.getRegisterById(this.registerId).then(result => {
            this.isLoading = false;
            this.model = result;
        });
    }

    Save(): void {
        this.isLoading = true;

        if (this.isNew) {
            this.registerService.createRegister(this.model).then(result => {
                this.isLoading = false;
            });
        }
        else {
            this.registerService.updateRegister(this.model).then(result => {
                this.isLoading = false;
            });
        }

        this.router.navigate(['../../../'], { relativeTo: this.route });
    }

    Calculate(): void {
        this.registerService.calculateRegister(this.model).then(result => {
            this.model = result;
        });
    }
}