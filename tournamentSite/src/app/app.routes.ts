import { Routes } from '@angular/router';
import { MainTournamentPage } from './pages/main-tournament-page/main-tournament-page';
import { PageNotFound } from './pages/page-not-found/page-not-found';
import { Auth } from './pages/auth/auth';


export const routes: Routes = [
    {path: 'login', component: Auth},
    { path: '**', redirectTo: '',component: PageNotFound},
    {path: 'management-page', component: MainTournamentPage}
];
