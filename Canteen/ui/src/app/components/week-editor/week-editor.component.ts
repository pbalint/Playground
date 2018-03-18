import { DayMenu } from '../../domain/day-menu';
import { Dish } from '../../domain/dish';
import { WeekMenu } from '../../domain/week-menu';
import { MenuService } from '../../services/menu.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component( {
    selector: 'app-week-editor',
    templateUrl: './week-editor.component.html',
    styleUrls: [ './week-editor.component.css' ]
} )
export class WeekEditorComponent implements OnInit {
    menu: WeekMenu;

    constructor( private menuService: MenuService, private route: ActivatedRoute ) { }

    ngOnInit() {
        this.route.params.subscribe( params => {
            const yearNumber: number = parseInt( params[ 'year' ], 10 );
            const weekNumber: number = parseInt( params[ 'weekNumber' ], 10 );
            if ( !Number.isNaN( yearNumber ) && !Number.isNaN( weekNumber ) ) {
                this.getMenuForEditingForWeek( yearNumber, weekNumber );
            }
            else {
                this.getMenuForEditingForCurrentWeek();
            }
        } );
    }

    private getMenuForEditingForWeek( yearNumber: number, weekNumber: number ): void {
        this.menuService.getMenuForWeek( yearNumber, weekNumber ).subscribe( menu => {
            this.menu = this.expandDayMenus( menu );
        } );
    }

    private getMenuForEditingForCurrentWeek(): void {
        this.menuService.getMenuForWeek( 2018, 5 ).subscribe( menu => {
            this.menu = this.expandDayMenus( menu );
        } );
    }

    private expandDayMenus( weekMenu: WeekMenu ): WeekMenu {
        const expandedDayMenus = new Array<DayMenu>( 7 );
        weekMenu.dayMenus.forEach( dayMenu => expandedDayMenus[ dayMenu.dayNumber ] = dayMenu );
        weekMenu.dayMenus = expandedDayMenus;
        return weekMenu;
    }
}
