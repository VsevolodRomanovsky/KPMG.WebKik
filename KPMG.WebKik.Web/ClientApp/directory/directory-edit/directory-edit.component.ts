import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { DirectoryService } from '../directory.service';
import { DirectoryViewModel } from '../models/directory.model';
import { DirectoryEntryViewModel } from '../models/directory-entry.model';

@Component({
    template: require('./directory-edit.component.html')
})
export class DirectoryEditComponent implements OnInit {
    directoryTypeName: string;
    directory: DirectoryViewModel;
    newDirectoryEntry: DirectoryEntryViewModel;
    isLoading: boolean;

    constructor(private directoryService: DirectoryService, private route: ActivatedRoute) {
        this.newDirectoryEntry = new DirectoryEntryViewModel();
    }

    ngOnInit(): void {
        this.isLoading = true;
        this.directoryTypeName = this.route.snapshot.params['directory'];
        this.directoryService
            .getByName(this.directoryTypeName)
            .then(directory => {
                this.directory = directory;
                this.isLoading = false;
            });
    }

    addNewDirectoryEntry() {
        this.isLoading = true;
        this.newDirectoryEntry.Id = 0;
        this.directoryService
            .createDirectoryEntry(this.directoryTypeName, this.newDirectoryEntry)
            .then(() => {
                this.directory.Entries.push(this.newDirectoryEntry);
                this.newDirectoryEntry = new DirectoryEntryViewModel();
                this.isLoading = false;
            });
    }
}

