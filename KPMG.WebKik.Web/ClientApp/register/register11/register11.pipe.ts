import { Component, OnInit, Pipe, PipeTransform, Injectable } from '@angular/core';

@Pipe({
	name: 'sectionGroup'
})

export class FilterPipe1 implements PipeTransform {
	transform(items: any[], field: number, value: number): any[] {
		if (!items) {
			return [];
		}
		else {
			let tmp = [items];
			return items.filter(it => (+it.Register11DataTypeId) === (+field));
		}

	}
}