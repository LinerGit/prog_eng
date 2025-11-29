import { isPlatformBrowser } from '@angular/common';
import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { BehaviorSubject, delay, Observable, of, scheduled, using } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly AUTH_KEY = 'is_authenticated';
  private readonly MOCK_USER = { username: 'admin', password: '123'};

  private registerData = { username:'', email:'', password:'' }

  private loggedIn = new BehaviorSubject<boolean>(this.hasToken());
  private isBrowser: Boolean;

  constructor(@Inject(PLATFORM_ID) private platformId: Object) {
    this.isBrowser = isPlatformBrowser(platformId);
  }

  private setAuthFlag(isAuth: boolean): void {
    if (this.isBrowser) {
      localStorage.setItem(this.AUTH_KEY, isAuth ? 'true' : 'false');
    }
  }

  setToken(token:string) {
    localStorage.setItem('token', token);
    this.loggedIn.next(true);
  }

  isLoggedIn(): Observable<boolean> {
    return this.loggedIn.asObservable();
  }

  register(username:string, email:string, password:string){
    this.registerData.username = username;
    this.registerData.email = email;
    this.registerData.password = password;
  }

  login(username: string, password: string): boolean {
  if (username === 'admin' && password === '1234') {
      localStorage.setItem(this.AUTH_KEY, 'fake-jwt-token');
      this.setAuthFlag(true);
      return true;
    }
    return false;
  }

  logout(): void {
    localStorage.removeItem('token');
    this.loggedIn.next(false);
  }

  private hasToken(): boolean {
    return !!localStorage.getItem('token');
  }
}


