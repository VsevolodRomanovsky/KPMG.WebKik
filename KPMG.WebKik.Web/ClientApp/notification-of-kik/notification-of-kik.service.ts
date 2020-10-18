import { Injectable } from '@angular/core';

import { NotificationOfKIKViewModel } from './models/notification-of-kik.model';
import { BaseService } from '../shared/base.service';

@Injectable()
export class NotificationOfKIKService {
    private apiUrl: string = 'api/NotificationOfKIK';

    constructor(private baseService: BaseService) {
    }

    getByProjectId(projectId: number): Promise<Array<NotificationOfKIKViewModel>> {
        return this.baseService.get<NotificationOfKIKViewModel>(this.apiUrl + '/project/' + projectId);
    }

    getById(id: number): Promise<NotificationOfKIKViewModel> {
        return this.baseService.get<NotificationOfKIKViewModel>(this.apiUrl + '/' + id);
    }

    getFileById(id: number): Promise<Blob> {
        return this.baseService.getBlob(this.apiUrl + '/' + id + '/file');
    }

    update(notificationofkik: NotificationOfKIKViewModel): Promise<any> {
        return this.baseService.put(this.apiUrl, notificationofkik);
    }

    create(signatory: NotificationOfKIKViewModel): Promise<NotificationOfKIKViewModel> {
        return this.baseService.post(this.apiUrl, signatory);
    }

    getXMLFileById(id: number): Promise<Blob> {
        return this.baseService.getBlob(this.apiUrl + '/' + id + '/xmlfile');
    }
}