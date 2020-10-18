import { Component, OnInit, Input } from '@angular/core';

import { DirectoryService } from '../directory.service';
import { DirectoryViewModel } from '../models/directory.model';

@Component({
    selector: '[directory-options]',
    template: require('./directory-options.component.html')
})
export class DirectoryOptionsComponent implements OnInit {
    @Input('directory-options') typeName: string;
    @Input('ngModel') selectedId: number;

    directory: DirectoryViewModel;

    constructor(private directoryService: DirectoryService) {
        this.directory = new DirectoryViewModel();
    }

    ngOnInit(): void {
        this.directoryService
            .getByName(this.typeName)
            .then(result => {
                this.directory = result;
                this.directory.Entries.sort((a, b): number => {
                    if (a.Name < b.Name) return -1;
                    if (a.Name > b.Name) return 1;
                    return 0;
                });
            });
    }
}

