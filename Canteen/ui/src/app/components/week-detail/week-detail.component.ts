import { WeekMenu } from '../../domain/week-menu';
import { MenuService } from '../../services/menu.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component( {
    selector: 'app-week-detail',
    templateUrl: './week-detail.component.html',
    styleUrls: [ './week-detail.component.css' ]
} )
export class WeekDetailComponent implements OnInit {
    menu: WeekMenu;

    constructor( private menuService: MenuService, private route: ActivatedRoute ) { }

    ngOnInit() {
        this.route.params.subscribe( params => {
            const yearNumber: number = parseInt( params[ 'year' ], 10 );
            const weekNumber: number = parseInt( params[ 'weekNumber' ], 10 );
            if ( !Number.isNaN( yearNumber ) && !Number.isNaN( weekNumber ) ) {
                this.displayMenuForWeek( yearNumber, weekNumber );
            }
            else {
                this.displayMenuForCurrentWeek();
            }
        } );
    }

    private displayMenuForWeek( yearNumber: number, weekNumber: number ): void {
        this.menuService.getMenuForWeek( yearNumber, weekNumber ).subscribe( menu => {
            this.menu = menu;
        } );
    }

    private displayMenuForCurrentWeek(): void {
        this.menuService.getMenuForWeek( 2018, 5 ).subscribe( menu => {
            this.menu = menu;
        } );
    }
}
