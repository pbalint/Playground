import { Dish } from '../domain/dish';
import { WeekMenu } from '../domain/week-menu';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/observable';
import { of } from 'rxjs/observable/of';

@Injectable()
export class MenuService {

    constructor() { }

    getMenuForWeek( yearNumber: number, weekNumber: number ): Observable<WeekMenu> {
        return of( {
            yearNumber: yearNumber,
            weekNumber: weekNumber,
            dayMenus: [
                {
                    date: 'Hétfő / Monday',
                    dayNumber: 0,
                    dishes: new Map<string, Dish[]>( [
                        [ 'appetizers', [ { id: 1, dishType: 'appetizers', name: 'asd1', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { id: 2, dishType: 'appetizers', name: 'asd2', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { id: 3, dishType: 'appetizers', name: 'asd3', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { id: 4, dishType: 'appetizers', name: 'asd4', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'main courses', [ { id: 5, dishType: 'main courses', name: 'asd2', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'desserts', [ { id: 6, dishType: 'desserts', name: 'asd3', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'side dishes', [ { id: 7, dishType: 'side dishes', name: 'asd4', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group5', [ { id: 8, dishType: 'group5', name: 'asd5', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group6', [ { id: 9, dishType: 'group6', name: 'asd6', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group7', [ { id: 10, dishType: 'group7', name: 'asd7', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ]
                    ] )
                },
                {
                    date: 'Kedd / Tuesday',
                    dayNumber: 1,
                    dishes: new Map<string, Dish[]>( [
                        [ 'appetizers', [ { id: 1, dishType: 'appetizers', name: 'asd1', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { id: 2, dishType: 'appetizers', name: 'asd2', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { id: 3, dishType: 'appetizers', name: 'asd3', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { id: 4, dishType: 'appetizers', name: 'asd4', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'main courses', [ { id: 5, dishType: 'main courses', name: 'asd2', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'desserts', [ { id: 6, dishType: 'desserts', name: 'asd3', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'side dishes', [ { id: 7, dishType: 'side dishes', name: 'asd4', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group5', [ { id: 8, dishType: 'group5', name: 'asd5', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group6', [ { id: 9, dishType: 'group6', name: 'asd6', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group7', [ { id: 10, dishType: 'group7', name: 'asd7', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ]
                    ] )
                },
                {
                    date: 'Szerda / Wednesday',
                    dayNumber: 2,
                    dishes: new Map<string, Dish[]>( [
                        [ 'appetizers', [ { id: 1, dishType: 'appetizers', name: 'asd1', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { id: 2, dishType: 'appetizers', name: 'asd2', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { id: 3, dishType: 'appetizers', name: 'asd3', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { id: 4, dishType: 'appetizers', name: 'asd4', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'main courses', [ { id: 5, dishType: 'main courses', name: 'asd2', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'desserts', [ { id: 6, dishType: 'desserts', name: 'asd3', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'side dishes', [ { id: 7, dishType: 'side dishes', name: 'asd4', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group5', [ { id: 8, dishType: 'group5', name: 'asd5', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group6', [ { id: 9, dishType: 'group6', name: 'asd6', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group7', [ { id: 10, dishType: 'group7', name: 'asd7', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ]
                    ] )
                },
                {
                    date: 'Csütörtök / Thursday',
                    dayNumber: 3,
                    dishes: new Map<string, Dish[]>( [
                        [ 'appetizers', [ { id: 1, dishType: 'appetizers', name: 'asd1', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { id: 2, dishType: 'appetizers', name: 'asd2', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { id: 3, dishType: 'appetizers', name: 'asd3', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { id: 4, dishType: 'appetizers', name: 'asd4', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'main courses', [ { id: 5, dishType: 'main courses', name: 'asd2', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'desserts', [ { id: 6, dishType: 'desserts', name: 'asd3', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'side dishes', [ { id: 7, dishType: 'side dishes', name: 'asd4', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group5', [ { id: 8, dishType: 'group5', name: 'asd5', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group6', [ { id: 9, dishType: 'group6', name: 'asd6', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group7', [ { id: 10, dishType: 'group7', name: 'asd7', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ]
                    ] )
                },
                {
                    date: 'Péntek / Friday',
                    dayNumber: 4,
                    dishes: new Map<string, Dish[]>( [
                        [ 'appetizers', [ { id: 1, dishType: 'appetizers', name: 'asd1', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { id: 2, dishType: 'appetizers', name: 'asd2', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { id: 3, dishType: 'appetizers', name: 'asd3', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' },
                        { id: 4, dishType: 'appetizers', name: 'asd4', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'main courses', [ { id: 5, dishType: 'main courses', name: 'asd2', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'desserts', [ { id: 6, dishType: 'desserts', name: 'asd3', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'side dishes', [ { id: 7, dishType: 'side dishes', name: 'asd4', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group5', [ { id: 8, dishType: 'group5', name: 'asd5', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group6', [ { id: 9, dishType: 'group6', name: 'asd6', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ],
                        [ 'group7', [ { id: 10, dishType: 'group7', name: 'asd7', englishName: 'English ' + name, price: 111, imageUrl: 'https://images.pexels.com/photos/461198/pexels-photo-461198.jpeg?w=1260&h=750&auto=compress&cs=tinysrgb' }] ]
                    ] )
                },
            ]
        } );
    }
}
