import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { RegisterType } from '../models/register-type.model';
import { Register1ViewModel } from '../models/register1.model';
import { Register1Service } from '../services/register1.service'

@Component({
    template: require('./register1.component.html'),
    providers: []
})
export class Register1Component implements OnInit {
    model: Register1ViewModel;
    projectId: number;
    companyId: number;
    registerId: number;
    shareId: number;
    year: number;
    isLoading: boolean;
    isNew: boolean;

    constructor(private route: ActivatedRoute, private router: Router, private registerService: Register1Service) { }

    ngOnInit(): void {
        this.isLoading = true;

        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
        this.companyId = parseInt(this.route.snapshot.params['companyid'], 10);
        this.shareId = parseInt(this.route.snapshot.params['shareid'], 10);
        this.year = parseInt(this.route.snapshot.params['year'], 10);
        this.registerId = parseInt(this.route.snapshot.params['registerid'], 10);
        
        if (this.registerId == 0) {
            this.model = new Register1ViewModel();
            this.model.Year = this.year;
            this.model.OwnerProjectCompanyId = this.shareId;
            this.model.Type = RegisterType.Register1;
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

