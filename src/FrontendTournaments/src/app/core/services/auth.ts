// src/app/core/services/auth.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = 'https://localhost:5001/api/tournament';

  constructor(private http: HttpClient) {}

  login(email: string, password: string) {
    return this.http.post<{ token: string }>(
      `${this.apiUrl}/login`,
      { email, password }
    ).pipe(
      tap(res => localStorage.setItem('token', res.token))
    );
  }

  register(username: string, email: string, password: string) {
    return this.http.post<{ token: string }>(
      `${this.apiUrl}/register`,
      { username, email, password }
    ).pipe(
      tap(res => localStorage.setItem('token', res.token))
    );
  }

  get token(): string | null {
    return localStorage.getItem('token');
  }

  logout() {
    localStorage.removeItem('token');
  }
}
