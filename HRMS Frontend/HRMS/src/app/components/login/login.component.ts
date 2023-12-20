import { HttpErrorResponse } from '@angular/common/http';
import { UserService } from './../../services/user-service.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup,FormControl,Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthenticationModel } from 'src/app/models/authentication-model';
import { LoginModel } from 'src/app/models/login-model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  constructor(private userService: UserService, private router: Router) {}

  loginForm = new FormGroup({
    username: new FormControl("",[Validators.required,Validators.pattern("^([a-zA-Z])([a-zA-Z0-9]{4,10})")]),
    password: new FormControl("",[Validators.required,Validators.minLength(10)])
  })

  get userName(){
    return this.loginForm.controls.username;
}

get password(){
    return this.loginForm.controls.password;
}

loginUserSubscription?: Subscription;

message?: string;


login(e: Event): void{
    e.preventDefault();
    
    if(this.loginForm.valid){
        let loginModel: LoginModel = this.loginForm.value as LoginModel;
        this.loginUserSubscription = this.userService.Login(loginModel).subscribe({
            next: (response: AuthenticationModel) =>{
                if(response.isAuthenticated){
                    const token = response.token;
                    localStorage.setItem("jwt", token);

                    this.router.navigate(['']);
                }

                this.message = response.message;
            },

            error: (err: HttpErrorResponse) =>{
                let errorObj = {
                    Status: err.status,
                    Header: err.headers,
                    Name: err.name,
                    Message: err.message
                }
                console.log(err);
            }
        });
    }
}

ngOnDestroy(): void {
    this.loginUserSubscription?.unsubscribe();
}
}
