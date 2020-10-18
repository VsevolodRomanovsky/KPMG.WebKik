import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { NotificationOfParticipationService } from '../notification-of-participation.service';
import { NotificationOfParticipationViewModel } from '../models/notification-of-participation.model';

@Component({
    template: require('./notification-of-participation-list.component.html')
})
export class NotificationOfParticipationListComponent implements OnInit {
    projectId: number;
    notificationList: Array<NotificationOfParticipationViewModel>;
    isLoading: boolean;

    constructor(private dataService: NotificationOfParticipationService, private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.isLoading = true;
        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);

        this.dataService.getByProjectId(this.projectId).then(result => {
            this.notificationList = result.map(item => {
                return Object.assign(new NotificationOfParticipationViewModel(), item);
            });
            this.isLoading = false;
        });
	}

	isShow(not: NotificationOfParticipationViewModel): boolean {
		return (not.ProjectCompany.SupportingDocuments && not.ProjectCompany.SupportingDocuments.length > 0) ;
	}
}

