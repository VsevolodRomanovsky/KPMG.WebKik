import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { NotificationOfParticipationService } from '../notification-of-participation.service';
import { NotificationOfParticipationViewModel } from '../models/notification-of-participation.model';

@Component({
    template: require('./notification-of-participation-edit.component.html'),
})
export class NotificationOfParticipationEditComponent implements OnInit {
    model: NotificationOfParticipationViewModel;
    notificationId: number;
    projectId: number;
    companyId: number;
    isLoading: boolean;

    constructor(
        private dataService: NotificationOfParticipationService,
        private router: Router,
        private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.isLoading = true;
        this.notificationId = parseInt(this.route.snapshot.params['notificationid'], 10);
        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
        let loadTask: Promise<NotificationOfParticipationViewModel>;

        if (this.notificationId == 0) {
            loadTask = Promise.resolve(new NotificationOfParticipationViewModel());
        } else {
            loadTask = this.dataService.getById(this.notificationId);
        }

        loadTask.then(result => {
            this.model = Object.assign(new NotificationOfParticipationViewModel(), result);
            this.isLoading = false;
        });
    }

    onSubmit() {
        this.isLoading = true;
        const successCallback = (result) => {
            this.isLoading = false;
            if (result && result.hasOwnProperty("Id")) {
                this.notificationId = result.Id;
            } else {
                this.notificationId = parseInt(this.route.snapshot.params['notificationid'], 10);
            }
            this.router.navigate(['../' + this.notificationId], { relativeTo: this.route });
        };

        
        if (this.isNew) {
            this.dataService.create(this.model).then(successCallback);
        } else {
            this.model.Id = this.notificationId;
            this.dataService.update(this.model).then(successCallback);
        }
    }

    getFile() {
        this.dataService.getFileById(this.notificationId).then(blobContent => {
            const url = window.URL.createObjectURL(blobContent);
            window.open(url);
        });
    }

    getXMLFile() {

        this.dataService.getXMLFileById(this.notificationId).then(blobContent => {
            const url = window.URL.createObjectURL(blobContent);
            window.open(url);
        });
    }

    get title() { return this.isNew ? "Создание уведомления" : "Редактирование уведомления"; }
    get isNew() { return this.notificationId == 0; }
}

