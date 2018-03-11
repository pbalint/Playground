import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { WeekDetailComponent } from './components/week-detail/week-detail.component';

const routes: Routes = [
    { path: '', redirectTo: '/weeks/', pathMatch: 'full' },
    { path: 'weeks/:weekNumber', component: WeekDetailComponent }
];

@NgModule( {
    imports: [ RouterModule.forRoot( routes ) ],
    exports: [ RouterModule ]
} )
export class AppRoutingModule { }
