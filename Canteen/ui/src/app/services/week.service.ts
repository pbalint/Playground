import { Injectable } from '@angular/core';
import { Week } from '../domain/week';
import { Observable } from 'rxjs/observable';
import { of } from 'rxjs/observable/of';

@Injectable()
export class WeekService {

    constructor() { }

    getWeeks(): Observable<Week[]> {
        return of( [
            { weekNumber: 1, start: 'a', end: 'b' },
            { weekNumber: 2, start: 'aaa', end: 'dgffb' },
            { weekNumber: 3, start: 'dasda', end: 'gfbg' },
            { weekNumber: 4, start: 'eqwea', end: 'gdfgb' },
            { weekNumber: 5, start: 'eqwea', end: 'bcvbb' },
        ] );
    }
}
