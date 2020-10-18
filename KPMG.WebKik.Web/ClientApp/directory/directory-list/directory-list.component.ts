import { Component, OnInit } from '@angular/core';

import { DirectoryService } from '../directory.service';
import { DirectoryViewModel } from '../models/directory.model';
import { DirectoryEntryViewModel } from '../models/directory-entry.model';

@Component({
    template: require('./directory-list.component.html')
})
export class DirectoryListComponent implements OnInit {
    isLoading: boolean;
    directoryList: Array<{ Key: string, Value: string }>;

    constructor(private directoryService: DirectoryService) { }

    ngOnInit(): void {
        this.isLoading = true;
        this.directoryService
            .getDirectoryList()
            .then(directoryList => {
                this.directoryList = directoryList;
                this.isLoading = false;
            });
    }
}

