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
    weekNumber: number;
    menu: WeekMenu;

    constructor( private menuService: MenuService, private route: ActivatedRoute ) { }

    ngOnInit() {
        this.route.params.subscribe( params => {
            const weekNumber: number = parseInt( params[ 'weekNumber' ], 10 );
            if ( !Number.isNaN( weekNumber ) ) {
                this.displayMenuForWeek( weekNumber );
            }
            else {
                this.displayMenuForCurrentWeek();
            }
        } );
    }

    private displayMenuForWeek( weekNumber: number ): void {
        this.weekNumber = weekNumber;
        this.menuService.getMenuForWeek( weekNumber ).subscribe( menu => {
            this.menu = menu;
        } );
    }

    private displayMenuForCurrentWeek(): void {
        this.weekNumber = -1;
        this.menuService.getMenuForWeek( this.weekNumber ).subscribe( menu => {
            this.menu = menu;
        } );
    }

}
