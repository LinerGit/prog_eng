// src/app/core/services/tournament.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({ providedIn: 'root' })
export class TournamentService {
  private apiUrl = 'https://localhost:5001/api/tournament';

  constructor(private http: HttpClient) {}

  createTournament(dto: any) {
    return this.http.post(`${this.apiUrl}`, dto);
  }

  updateMatchResult(dto: any) {
    return this.http.put(`${this.apiUrl}/matches/results`, dto);
  }

  compareTeams(teamAId: number, teamBId: number) {
    return this.http.get(`${this.apiUrl}/compare-teams`, {
      params: { teamAId, teamBId }
    });
  }

  getTournamentView(id: number) {
    return this.http.get(`${this.apiUrl}/${id}/view`);
  }

  createTeamByPlayer(teamName: string, tournamentId: number, playerId: number) {
    return this.http.post(`${this.apiUrl}/teams/create-by-player`, {
      teamName,
      tournamentId,
      playerId
    });
  }
}
