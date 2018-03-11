import { Week } from '../../domain/week';
import { WeekService } from '../../services/week.service';
import { Component, OnInit } from '@angular/core';

@Component( {
    selector: 'app-week-list',
    templateUrl: './week-list.component.html',
    styleUrls: [ './week-list.component.css' ]
} )
export class WeekListComponent implements OnInit {
    public weeks: Week[];
    constructor( private weekService: WeekService ) { }

    ngOnInit() {
        this.getWeeks();
    }

    public getWeeks(): void {
        this.weekService.getWeeks().subscribe( weeks => this.weeks = weeks );
    }

    public weekClicked( week: Week ): void {
        console.log( week.weekNumber );
    }
}
