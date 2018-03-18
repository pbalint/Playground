import { Dish } from '../../domain/dish';
import { DialogService } from '../../services/dialog.service';
import { DishEditorComponent } from '../dish-editor/dish-editor.component';
import { Component, OnInit } from '@angular/core';

const DISHES: Dish[] = [
    { id: 1, dishType: 'newGroup1', name: 'asd1', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
    { id: 2, dishType: 'newGroup1', name: 'asd2', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
    { id: 3, dishType: 'newGroup2', name: 'asd3', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
    { id: 4, dishType: 'appetizers', name: 'asd12', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
    { id: 5, dishType: 'appetizers', name: 'asd22', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
    { id: 6, dishType: 'appetizers', name: 'asd32', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
    { id: 7, dishType: 'appetizers', name: 'asd42', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
    { id: 8, dishType: 'appetizers', name: 'asd52', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
    { id: 9, dishType: 'appetizers', name: 'asd62', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
    { id: 10, dishType: 'newGroup7', name: 'asd72', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
    { id: 11, dishType: 'newGroup6', name: 'asd82', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
    { id: 12, dishType: 'newGroup5', name: 'asd892', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
    { id: 13, dishType: 'newGroup4', name: 'asd2ö', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
    { id: 14, dishType: 'newGroup3', name: 'asd2ü', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
    { id: 15, dishType: 'newGroup2', name: 'asd2t', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
    { id: 16, dishType: 'newGroup7', name: 'asdg2', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
    { id: 17, dishType: 'newGroup6', name: 'asd2', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
    { id: 18, dishType: 'newGroup8', name: 'asttrred2', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
    { id: 19, dishType: 'newGroup9', name: 'asd2g', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
    { id: 20, dishType: 'newGroup9', name: 'asdfg2', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
    { id: 21, dishType: 'newGroup9', name: 'asd4', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }];

@Component( {
    selector: 'app-dish-selector',
    templateUrl: './dish-selector.component.html',
    styleUrls: [ './dish-selector.component.css' ]
} )
export class DishSelectorComponent implements OnInit {
    searchTerm: string;
    dishes = DISHES;

    constructor( private dialogService: DialogService ) { }

    ngOnInit() {
    }

    public onEditClick( dishToEdit: Dish ): void {
        this.dialogService.openDialog( DishEditorComponent, component => {
            component.dish = dishToEdit;
        } );
    }

    public updateSearch(): void {
        const lowerCaseSearchTerm = this.searchTerm.toLowerCase();
        this.dishes = DISHES.filter( dish => dish.name.toLowerCase().indexOf( lowerCaseSearchTerm ) > -1 );
    }

    public onDragStart( event: DragEvent, dish: Dish ): void {
        event.dataTransfer.setData( 'dish', JSON.stringify( dish ) );
    }
}

