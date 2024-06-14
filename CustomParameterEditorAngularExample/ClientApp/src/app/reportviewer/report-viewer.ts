import { Component, Inject, ViewEncapsulation } from '@angular/core';

@Component({
    selector: 'report-viewer',
    encapsulation: ViewEncapsulation.None,
    templateUrl: './report-viewer.html',
    styleUrls: [
        "../../../node_modules/devextreme/dist/css/dx.material.blue.light.css",
        "../../../node_modules/@devexpress/analytics-core/dist/css/dx-analytics.common.css",
        "../../../node_modules/@devexpress/analytics-core/dist/css/dx-analytics.material.blue.light.css",
        "../../../node_modules/devexpress-reporting/dist/css/dx-webdocumentviewer.css"
    ]
})
export class ReportViewerComponent {
    reportUrl: string = "CustomParameterReport";
    invokeAction: string = '/DXXRDV';

    onCustomizeParameterEditors(event): void {
        const parameter = event.args.parameter;
        const info = event.args.info;
        if (parameter.type === 'CustomParameterType') {
            info.validationRules = info.validationRules || [];
            info.validationRules.push(
                { type: 'email', message: 'Email parameter value has invalid format.' });
            info.editor.header = "custom-parameter-text-editor";
        }
    }

    constructor(@Inject('BASE_URL') public hostUrl: string) { }
}