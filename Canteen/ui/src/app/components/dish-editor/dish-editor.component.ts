import { Dish } from '../../domain/dish';
import { DialogService } from '../../services/dialog.service';
import { Component, OnInit, Input, OnDestroy, EventEmitter, Output } from '@angular/core';

@Component( {
    selector: 'app-dish-editor',
    templateUrl: './dish-editor.component.html',
    styleUrls: [ './dish-editor.component.css' ]
} )
export class DishEditorComponent implements OnInit, OnDestroy {
    @Input()
    dish: Dish;

    constructor( private dialogService: DialogService ) { }

    ngOnInit() {
    }

    ngOnDestroy() {
        console.log('ondestroy!');
    }

    public closeDialog(): void {
        this.dialogService.closeDialog();
    }
}
