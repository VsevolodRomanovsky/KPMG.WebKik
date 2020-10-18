import { OnInit, Component } from '@angular/core';
import { Response } from '@angular/http';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'preloader',
    template: require('./preloader.component.html'),
    styles: [require('./preloader.component.scss')]
})

export class PreloaderComponent implements OnInit {
    constructor() {

    }
    ngOnInit() {
    }
}
