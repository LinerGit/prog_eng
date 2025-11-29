import { Component, Input, signal } from '@angular/core';

import { Player } from '../../models/player';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { FormsModule } from '@angular/forms';
import { App } from '../../app';
import { AuthRegisterRequest } from '../../models/auth-register-request';
import { AuthLoginRequest } from '../../models/auth-login-request';



@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './auth.html',
  styleUrl: './auth.scss',
})

export class Auth {

  loginData: AuthLoginRequest = {
  login:'',
  password: ''
  };
  
  registerData: AuthRegisterRequest = {
    login:'',
    email:'',
    password:'',
    confirmPassword:''
  };

  email='';
  loginError = false;
  mode: 'login' | 'forgot' | 'register' = 'login';
  resetEmail = '';

  // Правила валидации email согласно EmailValidator
  private emailRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$/;

  // Правила валидации пароля согласно PasswordValidator
  private strongPasswordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{}|;:,.<>?]).{8,}$/;
  //@Input() login!:Player

  constructor(private authService: AuthService,private router: Router) {}

  onForgotPassword(): void {
    this.mode = 'forgot';
    
  }
  onBackToLogin(): void{
    this.mode = 'login'
  }
  onLogin(): void{
    this.loginError = false;
    if(this.authService.login(this.loginData.login,this.loginData.password)){
  
    } else{
      console.log('incorrect login or password')
      
    }
    console.log(this.loginData.login);
    console.log(this.loginData.password);
    
  }
  onRegister():void {
    this.mode = 'register';
    this.authService.register(this.registerData.login,this.registerData.email,this.registerData.password);
    
  }
}
