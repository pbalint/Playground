import { Dish } from '../domain/dish';
import { WeekMenu } from '../domain/week-menu';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/observable';
import { of } from 'rxjs/observable/of';

@Injectable()
export class MenuService {

    constructor() { }

    getMenuForWeek( weekNumber: number ): Observable<WeekMenu> {
        return of( {
            weekNumber: weekNumber,
            dayMenus: [
                {
                    date: weekNumber + ' Hétfő / Monday',
                    dishes: new Map<String, Dish[]>( [
                        [ 'appetizers', [ { name: 'asd1', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { name: 'asd2', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { name: 'asd3', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { name: 'asd4', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'main courses', [ { name: 'asd2', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'desserts', [ { name: 'asd3', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'side dishes', [ { name: 'asd4', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group5', [ { name: 'asd5', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group6', [ { name: 'asd6', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group7', [ { name: 'asd7', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ]
                    ] )
                },
                {
                    date: weekNumber + ' Kedd / Tuesday',
                    dishes: new Map<String, Dish[]>( [
                        [ 'group1', [ { name: 'asd1', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { name: 'asd2', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { name: 'asd3', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { name: 'asd4', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group2', [ { name: 'asd2', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group3', [ { name: 'asd3', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group4', [ { name: 'asd4', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group5', [ { name: 'asd5', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group6', [ { name: 'asd6', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group7', [ { name: 'asd7', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ]
                    ] )
                }, {
                    date: weekNumber + ' Szerda / Wednesday',
                    dishes: new Map<String, Dish[]>( [
                        [ 'group1', [ { name: 'asd1', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { name: 'asd2', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { name: 'asd3', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { name: 'asd4', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group2', [ { name: 'asd2', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group3', [ { name: 'asd3', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group4', [ { name: 'asd4', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group5', [ { name: 'asd5', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group6', [ { name: 'asd6', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group7', [ { name: 'asd7', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ]
                    ] )
                }, {
                    date: weekNumber + ' Csütörtök / Thursday',
                    dishes: new Map<String, Dish[]>( [
                        [ 'group1', [ { name: 'asd1', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { name: 'asd2', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { name: 'asd3', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { name: 'asd4', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group2', [ { name: 'asd2', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group3', [ { name: 'asd3', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group4', [ { name: 'asd4', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group5', [ { name: 'asd5', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group6', [ { name: 'asd6', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group7', [ { name: 'asd7', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ]
                    ] )
                }, {
                    date: weekNumber + ' Péntek / Friday',
                    dishes: new Map<String, Dish[]>( [
                        [ 'group1', [ { name: 'asd1', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { name: 'asd2', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { name: 'asd3', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { name: 'asd4', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group2', [ { name: 'asd2', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group3', [ { name: 'asd3', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group4', [ { name: 'asd4', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group5', [ { name: 'asd5', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group6', [ { name: 'asd6', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group7', [ { name: 'asd7', price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ]
                    ] )
                } ]
        } );
    }
}
