import { Injectable } from '@angular/core';
import { Week } from '../domain/week';
import { Observable } from 'rxjs/observable';
import { of } from 'rxjs/observable/of';

@Injectable()
export class WeekService {

    constructor() { }

    getWeeks(): Observable<Week[]> {
        return of( [
            { yearNumber: 2018, weekNumber: 1, start: 'a', end: 'b' },
            { yearNumber: 2018, weekNumber: 2, start: 'aaa', end: 'dgffb' },
            { yearNumber: 2018, weekNumber: 3, start: 'dasda', end: 'gfbg' },
            { yearNumber: 2018, weekNumber: 4, start: 'eqwea', end: 'gdfgb' },
            { yearNumber: 2018, weekNumber: 5, start: 'eqwea', end: 'bcvbb' },
        ] );
    }
}
