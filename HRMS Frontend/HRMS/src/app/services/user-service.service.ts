import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LoginModel } from '../models/login-model';
import { AuthenticationModel } from '../models/authentication-model';
import { RegisterModel } from '../models/register-model';


@Injectable({
    providedIn: 'root'
})
export class UserService {

    baseURL = "https://localhost:7073/api/User"

    token?: string;

    constructor(private httpClient: HttpClient) { }

    get tokenGetter() {
        return localStorage.getItem("jwt");
    }

    private headers = () => new HttpHeaders({
        'Authorization': `Bearer ${this.tokenGetter}` // Add the JWT token to the "Authorization" header
    });

    Login(loginModel: LoginModel){

        return this.httpClient.post<AuthenticationModel>(`${this.baseURL}/login`, loginModel,{
            headers: new HttpHeaders({ "Content-Type": "application/json"})
        });
    }

    Register(registerModel: RegisterModel){
        return this.httpClient.post(`${this.baseURL}/Register`, registerModel,{
            headers: new HttpHeaders({ "Content-Type": "application/json", 'Authorization': `Bearer ${this.tokenGetter}`}),
            responseType: "text"
        });
    }

    GetRoles(){
      return this.httpClient.get(`${this.baseURL}/Roles`,{
        headers: new HttpHeaders({ "Content-Type": "application/json", 'Authorization': `Bearer ${this.tokenGetter}`}),
      });
    }
}
