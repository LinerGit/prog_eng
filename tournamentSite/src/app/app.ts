import { Component, Injectable, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Auth } from "./pages/auth/auth";
import { MainTournamentPage } from './pages/main-tournament-page/main-tournament-page';
import { AuthService } from './services/auth-service';



@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Auth, MainTournamentPage],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('account_manager');

  constructor(public authService:AuthService){}
}
