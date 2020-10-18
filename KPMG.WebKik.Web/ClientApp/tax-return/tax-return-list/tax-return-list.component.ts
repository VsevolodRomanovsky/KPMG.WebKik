import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { TaxReturnService } from '../tax-return.service';
import { TaxReturnViewModel } from '../models/tax-return.model';

@Component({
    template: require('./tax-return-list.component.html')
})
export class TaxReturnListComponent implements OnInit {
    projectId: number;
    taxreturnList: Array<TaxReturnViewModel>;
    isLoading: boolean;

    constructor(private dataService: TaxReturnService, private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.isLoading = true;
        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);

        this.dataService.getByProjectId(this.projectId).then(result => {
            this.taxreturnList = result.map(item => {
                return Object.assign(new TaxReturnViewModel(), item);
            });
            this.isLoading = false;
        });
    }
}