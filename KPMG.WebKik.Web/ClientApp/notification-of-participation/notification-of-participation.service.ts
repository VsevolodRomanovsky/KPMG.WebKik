import { Injectable } from '@angular/core';

import { NotificationOfParticipationViewModel } from './models/notification-of-participation.model';
import { BaseService } from '../shared/base.service';
import { SupportingDocumentsViewModel } from '../company/models/supporting-documents.model';
import { Http, RequestOptions, Request, RequestMethod, Headers } from '@angular/http';

@Injectable()
export class NotificationOfParticipationService {
	private apiUrl: string = 'api/notificationsofparticipation';
	private apiDocs: string = 'api/supportingDocuments';

	constructor(private http: Http, private baseService: BaseService) {
    }

    getByProjectId(projectId: number): Promise<Array<NotificationOfParticipationViewModel>> {
        return this.baseService.get<NotificationOfParticipationViewModel>(this.apiUrl + '/project/' + projectId);
    }

    getById(id: number): Promise<NotificationOfParticipationViewModel> {
        return this.baseService.get<NotificationOfParticipationViewModel>(this.apiUrl + '/' + id);
	}

	getSupportingDocById(id: number): Promise<Blob> {
		return this.baseService.getBlob(this.apiDocs + '/' + id + '/file');
	}


    getFileById(id: number): Promise<Blob> {
        return this.baseService.getBlob(this.apiUrl + '/' + id + '/file');
    }

    getXMLFileById(id: number): Promise<Blob> {
        return this.baseService.getBlob(this.apiUrl + '/' + id + '/xmlfile');
    }

    update(notification: NotificationOfParticipationViewModel): Promise<any> {
        return this.baseService.put(this.apiUrl, notification);
	}

	getSupportingDocumentsByNotificationId(id: number, type: number): Promise<Array<SupportingDocumentsViewModel>> {
		return this.baseService.get<Array<SupportingDocumentsViewModel>>(this.apiDocs + '/' + id + '/type/' + type);
	}

    create(signatory: NotificationOfParticipationViewModel): Promise<NotificationOfParticipationViewModel> {
        return this.baseService.post(this.apiUrl, signatory);
    }
}