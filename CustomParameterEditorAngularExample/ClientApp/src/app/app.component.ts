import { AfterViewInit, Component, TemplateRef, ViewChild } from '@angular/core';
import { TemplateEngine } from 'devexpress-reporting-angular/dx-report-viewer';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html'
})
export class AppComponent implements AfterViewInit {
	title = 'report-viewer-app';
	@ViewChild('myCustomInput') myCustomInput!: TemplateRef<{ data: any }>;
	constructor(private _templateEngine: TemplateEngine) {
	}
	ngAfterViewInit(): void {
		this._templateEngine.register('custom-parameter-text-editor', this.myCustomInput);
	}
}
