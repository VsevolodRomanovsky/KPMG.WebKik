import { Component } from '@angular/core';
@Component({
	selector: 'register9-modal',
	templateUrl: './register9.component.html'
})
export class Register9Modal {
	private ErrorMsg: string;
	public ErrorMessageIsVisible: boolean;

	showErrorMessage(msg: string) {
		this.ErrorMsg = msg;
		this.ErrorMessageIsVisible = true;
	}

	hideErrorMsg() {
		this.ErrorMessageIsVisible = false;
	}
}