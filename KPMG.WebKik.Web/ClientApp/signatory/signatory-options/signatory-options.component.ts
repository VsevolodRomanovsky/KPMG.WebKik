import { Component, OnInit, Input } from '@angular/core';

import { SignatoryService } from '../signatory.service';
import { SignatoryViewModel } from '../models/signatory.model';

@Component({
    selector: '[signatory-options]',
    template: require('./signatory-options.component.html')
})
export class SignatoryOptionsComponent implements OnInit {
    @Input('ngModel') selectedId: number;
    @Input('signatory-options') companyId: number;

    previousCompanyId: number;

    signatoryList: Array<SignatoryViewModel>;

    constructor(private signatoryService: SignatoryService) {
        this.signatoryList = [];
    }

    ngOnInit(): void {
    }

    ngOnChanges() {
        if (!this.companyId || this.previousCompanyId == this.companyId) {
            return;
        }

        this.previousCompanyId = this.companyId;

        this.signatoryService
            .getByCompanyId(this.companyId)
            .then(result => {
                this.signatoryList = result.map(item => { return new SignatoryViewModel(item); });
                this.signatoryList.sort((a, b): number => {
                    if (a.LastName < b.LastName) return -1;
                    if (a.LastName > b.LastName) return 1;
                    return 0;
                });
            });
    }

}

