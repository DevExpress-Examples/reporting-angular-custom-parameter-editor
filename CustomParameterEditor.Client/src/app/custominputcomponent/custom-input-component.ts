import { Component, Input } from '@angular/core';
import { IEditorViewModel } from '@devexpress/analytics-core/analytics-widgets-native';
import { DxTextBoxModule, DxValidatorModule } from 'devextreme-angular';

@Component({
    selector: 'custom-input-component',
    templateUrl: 'custom-input-component.html',
    standalone: true,
    imports: [DxTextBoxModule, DxValidatorModule]
})
export class CustomInput {
    @Input('data') data!: IEditorViewModel;
}
