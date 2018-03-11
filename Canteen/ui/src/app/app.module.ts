import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { WeekListComponent } from './components/week-list/week-list.component';
import { MenubarComponent } from './components/menubar/menubar.component';
import { WeekDetailComponent } from './components/week-detail/week-detail.component';
import { MenuService } from './services/menu.service';
import { WeekService } from './services/week.service';
import { DishEntriesPipe } from './pipes/map-values.pipe';


@NgModule({
  declarations: [
    AppComponent,
    WeekListComponent,
    MenubarComponent,
    WeekDetailComponent,
    DishEntriesPipe
  ],
  imports: [
    AppRoutingModule,
    BrowserModule
  ],
  providers: [WeekService, MenuService],
  bootstrap: [AppComponent]
})
export class AppModule { }
