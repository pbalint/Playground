import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { WeekDetailComponent } from './components/week-detail/week-detail.component';
import { WeekEditorComponent } from './components/week-editor/week-editor.component';

const routes: Routes = [
    { path: 'menu', component: WeekDetailComponent },
    { path: 'menu/:year/:weekNumber', component: WeekDetailComponent },
    { path: 'menu/:year/:weekNumber/edit', component: WeekEditorComponent },
    { path: '**', redirectTo: '/menu', pathMatch: 'full' }
];

@NgModule( {
    imports: [ RouterModule.forRoot( routes ) ],
    exports: [ RouterModule ]
} )
export class AppRoutingModule { }
