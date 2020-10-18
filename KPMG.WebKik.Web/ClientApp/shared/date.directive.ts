import { Input, Output, EventEmitter, Directive, ElementRef, OnInit } from '@angular/core';
import * as moment from 'moment';

@Directive({
    selector: '.date',
    host: {
        '(change)': 'onDateChange($event.target.value)'
    }
})
export class DateDirective implements OnInit {
    private _date: string;
    private _dateFormatMap = { 'DD.MM.YYYY': 'dd.mm.yyyy' };

    @Input() dateFormat = 'DD.MM.YYYY';

    @Input() set date(d: Date) {
        this._date = d ? this.toDateString(d) : null;
    }

    @Output() dateChange: EventEmitter<Date>;

    constructor(private elementRef: ElementRef) {
        this.date = new Date();
        this.dateChange = new EventEmitter<Date>();
    }

    ngOnInit() {
        $(this.elementRef.nativeElement).datepicker({
            format: this._dateFormatMap[this.dateFormat],
            autoclose: true,
            todayHighlight: true,
            clearBtn: true,
            orientation: 'auto',
            keyboardNavigation: false,
            forceParse: false,
        }).on("changeDate", (e) => {
            this.onDateChange(this.toDateString(e.date));
        });

        if (this._date)
            $(this.elementRef.nativeElement).datepicker('update', this._date);
    }

    private toDateString(date: Date): string {
        if (!date)
            return null;
        var result = moment(date).format(this.dateFormat);
        return result;
    }

    private parseDateString(date: string): Date {
        if (!date)
            return null;
        var result = moment(date, this.dateFormat).toDate();

        if (isNaN(result.getTime()))
            return null;        // date is not valid

        result = new Date(result.getTime() - result.getTimezoneOffset() * 60000);
        return result;
    }

    private onDateChange(value: string): void {
        if (value != this._date) {
            var parsedDate = this.parseDateString(value);
            if (!value || parsedDate) {
                this._date = value;
                this.dateChange.emit(parsedDate);
            }
        }
    }
}