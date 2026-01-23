import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login';
import { CreateTeamComponent } from './pages/create-team/create-team';
import { TournamentHomeComponent } from './pages/tournament-home-component/tournament-home-component';
import { RegisterComponent } from './pages/register/register';
import { TournamentViewComponent } from './pages/tournament-view/tournament-view';

export const routes: Routes = [
  { path: '', component: TournamentHomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'tournament/:id', component: TournamentViewComponent },
  { path: 'create-team', component: CreateTeamComponent },
  { path: 'register', component: RegisterComponent },
];
