import { HttpErrorResponse } from "@angular/common/http";
import { LoginModel } from "../../../models/login-model";
import { Component, OnDestroy, OnInit } from "@angular/core";
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from "@angular/router";
import { Subscription } from 'rxjs';
import { AuthenticationModel } from "src/app/models/authentication-model";
import { UserService } from 'src/app/services/user-service.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnDestroy, OnInit{
    constructor(private userService: UserService, private router: Router) {}

    ngOnInit(): void {
      const token = localStorage.getItem("jwt") ?? "";

      if(token !== ""){
        this.router.navigate(['']);
      }
    }

    loginForm = new FormGroup({
        UserName: new FormControl('', [Validators.required]),
        Password: new FormControl('', [Validators.required])
    });

    loginFormTouched: boolean = false;

    get userName(){
        return this.loginForm.controls.UserName;
    }

    get password(){
        return this.loginForm.controls.Password;
    }

    loginUserSubscription?: Subscription;

    message?: string;


    login(e: Event): void{
        e.preventDefault();
        this.loginFormTouched = true;

        if(this.loginForm.valid){
            let loginModel: LoginModel = this.loginForm.value as LoginModel;
            this.loginUserSubscription = this.userService.Login(loginModel).subscribe({
                next: (response: AuthenticationModel) =>{
                    if(response.isAuthenticated){
                        const token = response.token;
                        localStorage.setItem("jwt", token);

                        this.router.navigate(['']);
                        return;
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
                    this.message = err.message;
                }
            });
        }
    }

    ngOnDestroy(): void {
        this.loginUserSubscription?.unsubscribe();
    }
}
