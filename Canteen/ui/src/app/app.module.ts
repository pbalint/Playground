import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { WeekListComponent } from './components/week-list/week-list.component';
import { MenubarComponent } from './components/menubar/menubar.component';
import { WeekDetailComponent } from './components/week-detail/week-detail.component';
import { MenuService } from './services/menu.service';
import { WeekService } from './services/week.service';
import { DishEntriesPipe } from './pipes/map-values.pipe';
import { WeekEditorComponent } from './components/week-editor/week-editor.component';
import { DishSelectorComponent } from './components/dish-selector/dish-selector.component';
import { DayDetailComponent } from './components/day-detail/day-detail.component';
import { DishEditorComponent } from './components/dish-editor/dish-editor.component';
import { DialogHostDirective } from './directives/dialog-host.directive';
import { DialogHostComponent } from './components/dialog-host/dialog-host.component';
import { DialogService } from './services/dialog.service';

@NgModule( {
    declarations: [
        AppComponent,
        DishEntriesPipe,
        WeekListComponent,
        MenubarComponent,
        WeekDetailComponent,
        WeekEditorComponent,
        DishSelectorComponent,
        DayDetailComponent,
        DishEditorComponent,
        DialogHostDirective,
        DialogHostComponent
    ],
    imports: [
        AppRoutingModule,
        BrowserModule,
        FormsModule
    ],
    providers: [ WeekService, MenuService, DialogService ],
    bootstrap: [ AppComponent ],
    entryComponents: [ DishEditorComponent ]
} )
export class AppModule { }
