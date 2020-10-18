import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SupportingDocumentsViewModel } from '../../company/models/supporting-documents.model';
import { NotificationOfParticipationService } from '../notification-of-participation.service';
import { NotificationOfParticipationViewModel } from '../models/notification-of-participation.model';

@Component({
    template: require('./notification-of-participation-supportingdocuments.component.html')
})
export class NotificationOfParticipationSupportingDocumentsComponent implements OnInit {
    projectId: number;
    notificationid: number;
    documents: Array<SupportingDocumentsViewModel>;
    isLoading: boolean;

    constructor(private dataService: NotificationOfParticipationService, private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.isLoading = true;
        let id: number;
        let type: number; // 0 - УУ, 1 - НД, 2 - УКИК 
        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
        if (this.route.snapshot.params['notificationid']) {
            id = parseInt(this.route.snapshot.params['notificationid'], 10);
            type = 0;
        } else if (this.route.snapshot.params['taxreturnid']) {
            id = parseInt(this.route.snapshot.params['taxreturnid'], 10);
            type = 1;
        } else if (this.route.snapshot.params['notificationofkikid']) {
            id = parseInt(this.route.snapshot.params['notificationofkikid'], 10);
            type = 2;
        }

        this.dataService.getSupportingDocumentsByNotificationId(id, type).then(result => {
            this.documents = result;
            this.isLoading = false;
        });
    }

    downloadFile(id: number): void {
        this.dataService.getSupportingDocById(id).then(blobContent => {
            const url = window.URL.createObjectURL(blobContent);
            window.open(url);
        });
    }
}