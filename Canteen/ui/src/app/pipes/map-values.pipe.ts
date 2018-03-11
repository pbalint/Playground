import { Dish } from '../domain/dish';
import { Pipe, PipeTransform } from '@angular/core';

export class DishEntry {
    key: String;
    value: Dish[];
}

@Pipe( { name: 'dishEntries' } )
export class DishEntriesPipe implements PipeTransform {
    transform( value: any, args?: any[] ): DishEntry[] {
        const returnArray = [];

        value.forEach( ( entryVal, entryKey ) => {
            returnArray.push( {
                key: entryKey,
                value: entryVal
            } );
        } );

        return returnArray;
    }
}

