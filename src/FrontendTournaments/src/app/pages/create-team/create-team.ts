// src/app/pages/create-team/create-team.component.ts
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TournamentService } from '../../core/services/tournament';

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <h2>Create Team</h2>

    <input [(ngModel)]="teamName" placeholder="Team name">
    <input type="number" [(ngModel)]="tournamentId">
    <input type="number" [(ngModel)]="playerId">

    <button (click)="create()">Create</button>
  `
})
export class CreateTeamComponent {
  teamName = '';
  tournamentId = 0;
  playerId = 0;

  constructor(private service: TournamentService) {}

  create() {
    this.service.createTeamByPlayer(this.teamName, this.tournamentId, this.playerId)
      .subscribe(console.log);
  }
}
