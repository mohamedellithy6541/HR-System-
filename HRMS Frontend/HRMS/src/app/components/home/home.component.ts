import { Component } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent {
    constructor(private jwtHelper: JwtHelperService) {}
    isAuthenticatd(): boolean{
        const token = localStorage.getItem("jwt");
        if(token && !this.jwtHelper.isTokenExpired(token)){
            return true;
        }

        return false;
    }

    topFunction() {
      document.documentElement.scrollTo({top: 0, behavior: 'smooth'})
    }
}
