import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TournamentService } from '../../core/services/tournament';

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <h2>Tournament View</h2>

    <input type="number" [(ngModel)]="id">
    <button (click)="load()">Load</button>

    <pre>{{ tournament | json }}</pre>
  `
})
export class TournamentViewComponent {
  id = 0;
  tournament: any;

  constructor(private service: TournamentService) {}

  load() {
    this.service.getTournamentView(this.id).subscribe(res => this.tournament = res);
  }
}
