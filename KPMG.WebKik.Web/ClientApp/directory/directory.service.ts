import { Injectable } from '@angular/core';

import { DirectoryViewModel } from './models/directory.model';
import { DirectoryEntryViewModel } from './models/directory-entry.model';
import { BaseService } from '../shared/base.service';

@Injectable()
export class DirectoryService {
    private apiUrl: string = 'api/directories';

    constructor(private baseService: BaseService) {
    }

    getDirectoryList(): Promise<Array<{ Key: string, Value: string }>> {
        return this.baseService.get<DirectoryViewModel>(this.apiUrl);
    }

    getByName(directoryTypeName: string): Promise<DirectoryViewModel> {
        return this.baseService.get<DirectoryViewModel>(this.apiUrl + '/' + directoryTypeName);
    }

    createDirectoryEntry(directoryTypeName: string, directoryEntry: DirectoryEntryViewModel): Promise<DirectoryEntryViewModel> {
        return this.baseService.post(this.apiUrl + '/' + directoryTypeName, directoryEntry);
    }
}