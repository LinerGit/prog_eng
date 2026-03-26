// src/app/pages/register/register.component.ts
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../core/services/auth';

@Component({
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <h2>Регистрация</h2>

    <input
      [(ngModel)]="username"
      placeholder="Username"
    />

    <input
      [(ngModel)]="email"
      placeholder="Email"
      type="email"
    />

    <input
      [(ngModel)]="password"
      placeholder="Password"
      type="password"
    />

    <select [(ngModel)]="roleName">
      <option value="User">User</option>
      <option value="Player">Player</option>
      <option value="Organisator">Organisator</option>
    </select>

    <button (click)="register()">Register</button>

    <p *ngIf="error" style="color:red">{{ error }}</p>
  `
})
export class RegisterComponent {
  username = '';
  email = '';
  password = '';
  roleName = 'User';
  error = '';

  constructor(private auth: AuthService) {}

  register() {
    this.auth
      .register(this.username, this.email, this.password)
      .subscribe({
        next: () => {
          console.log('Регистрация успешна');
        },
        error: err => {
          this.error = err.error?.message ?? 'Ошибка регистрации';
        }
      });
  }
}
