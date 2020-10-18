import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { NotificationOfKIKService } from '../notification-of-kik.service';
import { NotificationOfKIKViewModel } from '../models/notification-of-kik.model';

@Component({
    template: require('./notification-of-kik-edit.component.html'),
})
export class NotificationOfKIKEditComponent implements OnInit {
    model: NotificationOfKIKViewModel;
    notificationOfKIKId: number;
    projectId: number;
    companyId: number;
    isLoading: boolean;

    constructor(
        private dataService: NotificationOfKIKService,
        private router: Router,
        private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.isLoading = true;
        this.notificationOfKIKId = parseInt(this.route.snapshot.params['notificationofkikid'], 10);
        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
        let loadTask: Promise<NotificationOfKIKViewModel>;

        if (this.notificationOfKIKId == 0) {
            loadTask = Promise.resolve(new NotificationOfKIKViewModel());
        } else {
            loadTask = this.dataService.getById(this.notificationOfKIKId);
        }

        loadTask.then(result => {
            this.model = Object.assign(new NotificationOfKIKViewModel(), result);
            this.isLoading = false;
        });
    }

    onSubmit() {
        this.isLoading = true;
        const successCallback = (result) => {
            this.isLoading = false;
            if (result) {
                this.notificationOfKIKId = result.Id;
            }
            this.router.navigate(['../' + this.notificationOfKIKId], { relativeTo: this.route });
        };

        if (this.isNew) {
            this.dataService.create(this.model).then(successCallback);
        } else {
            this.dataService.update(this.model).then(successCallback);
        }
    }

    getFile() {
        this.dataService.getFileById(this.notificationOfKIKId).then(blobContent => {
            const url = window.URL.createObjectURL(blobContent);
            window.open(url);
        });
    }

    getXMLFile() {

        this.dataService.getXMLFileById(this.notificationOfKIKId).then(blobContent => {
            const url = window.URL.createObjectURL(blobContent);
            window.open(url);
        });
    }

    get title() { return this.isNew ? "Создание уведомления о КИК" : "Редактирование уведомления о КИК"; }
    get isNew() { return this.notificationOfKIKId == 0; }
}

