import { AfterViewInit, Component, Inject, TemplateRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { DxReportDesignerModule, DxReportViewerModule } from 'devexpress-reporting-angular';
import { TemplateEngine } from 'devexpress-reporting-angular/dx-report-viewer';
import { CustomInput } from '../custominputcomponent/custom-input-component';

@Component({
  selector: 'report-viewer',
  encapsulation: ViewEncapsulation.None,
  templateUrl: './report-viewer.html',
  styleUrls: [
    "../../../node_modules/devextreme/dist/css/dx.material.blue.light.css",
    "../../../node_modules/@devexpress/analytics-core/dist/css/dx-analytics.common.css",
    "../../../node_modules/@devexpress/analytics-core/dist/css/dx-analytics.material.blue.light.css",
    "../../../node_modules/devexpress-reporting/dist/css/dx-webdocumentviewer.css"
  ],
  standalone: true,
  imports: [DxReportViewerModule, DxReportDesignerModule, CustomInput]
})
export class ReportViewer implements AfterViewInit {
    protected readonly reportUrl: string = 'CustomParameterReport';
  protected readonly invokeAction: string = '/DXXRDV';

  @ViewChild('myCustomInput') myCustomInput!: TemplateRef<{ data: any }>;

  ngAfterViewInit(): void {
    this._templateEngine.register('custom-parameter-text-editor', this.myCustomInput);
  }

  onCustomizeParameterEditors(event: any): void {
        const parameter = event.args.parameter;
        const info = event.args.info;
        if (parameter.type === 'CustomParameterType') {
            info.validationRules = info.validationRules || [];
            info.validationRules.push(
                { type: 'email', message: 'Email parameter value has invalid format.' });
            info.editor.header = "custom-parameter-text-editor";
        }
    }

  constructor(@Inject('BASE_URL') protected readonly hostUrl: string, private _templateEngine: TemplateEngine) { }
}
