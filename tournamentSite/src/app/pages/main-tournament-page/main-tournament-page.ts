import { Component } from '@angular/core';
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-main-tournament-page',
  imports: [],
  templateUrl: './main-tournament-page.html',
  styleUrl: './main-tournament-page.scss',
})
export class MainTournamentPage {
  constructor(public authService: AuthService){}

  tournamentIds: number[] = [0,1,2];

  logout(){
    this.authService.logout();
  }
}
