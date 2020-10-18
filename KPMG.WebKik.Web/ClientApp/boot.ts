import 'angular2-universal-polyfills/browser';
import { enableProdMode } from '@angular/core';
import { platformUniversalDynamic } from 'angular2-universal';
import { AppModule } from './app/app.module';

// Boot the application, either now or when the DOM content is loaded
const platform = platformUniversalDynamic();
const bootApplication = () => { platform.bootstrapModule(AppModule); };
if (document.readyState === 'complete') {
    bootApplication();
} else {
    document.addEventListener('DOMContentLoaded', bootApplication);
}

(<any>window).$ = require('jquery');
(<any>window).jQuery = (<any>window).$;
require('bootstrap');
require('bootstrap-datepicker');
require('primeng/primeng');
require('ng2-bs3-modal/ng2-bs3-modal');