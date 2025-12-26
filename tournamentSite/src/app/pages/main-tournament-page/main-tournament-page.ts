import { Component } from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-main-tournament-page',
  imports: [RouterLink],
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
