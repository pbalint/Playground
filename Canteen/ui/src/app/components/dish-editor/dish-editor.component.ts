import { Dish } from '../../domain/dish';
import { DialogService } from '../../services/dialog.service';
import { Component, OnInit, Input, OnDestroy, EventEmitter, Output, ViewChild, ElementRef } from '@angular/core';

@Component( {
    selector: 'app-dish-editor',
    templateUrl: './dish-editor.component.html',
    styleUrls: [ './dish-editor.component.css' ]
} )
export class DishEditorComponent implements OnInit, OnDestroy {
    @Input()
    dish: Dish;

    @ViewChild( 'dishType' )
    private dishTypeInput: ElementRef;

    @ViewChild( 'dishName' )
    private dishNameInput: ElementRef;

    @ViewChild( 'englishName' )
    private englishNameInput: ElementRef;

    @ViewChild( 'price' )
    private priceInput: ElementRef;

    @ViewChild( 'imageUrl' )
    private imageUrlInput: ElementRef;

    constructor( private dialogService: DialogService ) { }

    ngOnInit() {
    }

    ngOnDestroy() {
    }

    public closeDialog( save: boolean ): void {
        if ( save ) {
            this.dish.dishType = this.dishTypeInput.nativeElement.value;
            this.dish.name = this.dishNameInput.nativeElement.value;
            this.dish.englishName = this.englishNameInput.nativeElement.value;
            this.dish.price = this.priceInput.nativeElement.value;
            this.dish.imageUrl = this.imageUrlInput.nativeElement.value;
        }
        this.dialogService.closeDialog();
    }
}
