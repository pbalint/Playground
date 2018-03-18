import { DayMenu } from '../../domain/day-menu';
import { Dish } from '../../domain/dish';
import { Component, OnInit, Input } from '@angular/core';

@Component( {
    selector: 'app-daydetail',
    templateUrl: './day-detail.component.html',
    styleUrls: [ './day-detail.component.css' ]
} )
export class DayDetailComponent implements OnInit {
    @Input()
    dayMenu: DayMenu;

    dishTypes: Set<string> = new Set<string>();

    @Input()
    editMode: boolean;

    constructor() { }

    ngOnInit() {
        if ( this.dayMenu && this.dayMenu.dishes ) {
            this.dishTypes = new Set<string>( Array.from( this.dayMenu.dishes.keys() ) );
        }
    }

    public onDragOver( event: DragEvent ): void {
        event.preventDefault();
    }

    public onDrop( event: DragEvent, dishType: string, targetDish: Dish ): void {
        const dish: Dish = JSON.parse( event.dataTransfer.getData( 'dish' ) );

        let dishes: Dish[];
        if ( !this.dayMenu.dishes.has( dish.dishType ) ) {
            dishes = new Array<Dish>();
            this.dayMenu.dishes.set( dish.dishType, dishes );
            this.dishTypes.add( dish.dishType );
        }
        else {
            dishes = this.dayMenu.dishes.get( dish.dishType );
        }
        if ( targetDish !== undefined && targetDish !== null && dish.dishType === dishType) {
            const targetIndex = dishes.indexOf( targetDish );
            dishes.splice( targetIndex, 0, dish );
        }
        else {
            dishes.push( dish );
        }
    }

    public removeDish( dishId: number ): void {
        this.dayMenu.dishes.forEach( ( dishesForType: Dish[], dishType: string ) => {
            const indexToRemove = dishesForType.findIndex( dish => dish.id === dishId );
            if ( indexToRemove > -1 ) {
                dishesForType.splice( indexToRemove, 1 );
            }
            if ( dishesForType.length === 0 ) {
                this.dayMenu.dishes.delete( dishType );
                this.dishTypes.delete( dishType );
            }
        } );
    }
}
