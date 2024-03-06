import { Component, Input, OnInit } from '@angular/core';
import { IEditorViewModel } from '@devexpress/analytics-core/analytics-widgets-native';

@Component({
    selector: 'custom-input-component',
    templateUrl: 'custom.input.component.html'
})
export class CustomInputComponent {
    @Input('data') data!: IEditorViewModel;
}
