// src/app/pages/tournament-home/tournament-home.component.ts
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { TournamentService } from '../../core/services/tournament';

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  template: `
    <h1>🏆 Tournament Platform</h1>

    <section>
      <h3>🔐 Аккаунт</h3>
      <button routerLink="/login">Войти</button>
      <button routerLink="/register">Регистрация</button>
    </section>

    <hr />

    <section>
      <h3>👀 Просмотр турнира</h3>
      <input
        type="number"
        [(ngModel)]="tournamentId"
        placeholder="ID турнира"
      />
      <button (click)="openTournament()">Открыть</button>
    </section>

    <hr />

    <section>
      <h3>⚔️ Сравнение команд</h3>

      <input
        type="number"
        [(ngModel)]="teamAId"
        placeholder="ID команды A"
      />

      <input
        type="number"
        [(ngModel)]="teamBId"
        placeholder="ID команды B"
      />

      <button (click)="compare()">Сравнить</button>

      <pre *ngIf="compareResult">{{ compareResult | json }}</pre>
    </section>

    <hr />

    <section>
      <h3>➕ Команды</h3>
      <button routerLink="/create-team">Создать команду</button>
    </section>
  `
})
export class TournamentHomeComponent {
  tournamentId = 0;
  teamAId = 0;
  teamBId = 0;

  compareResult: any;

  constructor(
    private router: Router,
    private tournamentService: TournamentService
  ) {}

  openTournament() {
    if (this.tournamentId > 0) {
      this.router.navigate(['/tournament', this.tournamentId]);
    }
  }

  compare() {
    if (this.teamAId > 0 && this.teamBId > 0) {
      this.tournamentService
        .compareTeams(this.teamAId, this.teamBId)
        .subscribe(res => this.compareResult = res);
    }
  }
}
