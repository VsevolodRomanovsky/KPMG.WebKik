import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { NotificationOfKIKService } from '../notification-of-kik.service';
import { NotificationOfKIKViewModel } from '../models/notification-of-kik.model';

@Component({
    template: require('./notification-of-kik-list.component.html')
})
export class NotificationOfKIKListComponent implements OnInit {
    projectId: number;
    notificationofkikList: Array<NotificationOfKIKViewModel>;
    isLoading: boolean;

    constructor(private dataService: NotificationOfKIKService, private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.isLoading = true;
        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);

        this.dataService.getByProjectId(this.projectId).then(result => {
            this.notificationofkikList = result.map(item => {
                return Object.assign(new NotificationOfKIKViewModel(), item);
            });
            this.isLoading = false;
        });
    }
}