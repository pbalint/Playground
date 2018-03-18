import { Dish } from '../../domain/dish';
import { Component, OnInit, Input, OnDestroy, EventEmitter, Output } from '@angular/core';

@Component( {
    selector: 'app-dish-editor',
    templateUrl: './dish-editor.component.html',
    styleUrls: [ './dish-editor.component.css' ]
} )
export class DishEditorComponent implements OnInit, OnDestroy {
    @Input()
    dish: Dish;

    constructor() { }

    ngOnInit() {
    }

    ngOnDestroy() {
    }
}
