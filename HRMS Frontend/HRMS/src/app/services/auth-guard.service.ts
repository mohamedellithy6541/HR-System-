import { Injectable, inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
    providedIn: 'root'
})
export class AuthGuardService {

    constructor(private jwtHelper: JwtHelperService, private router: Router) { }

    isAuthenticated(token: string):boolean{
        if(token && token !== "null" && token.length != 0 && !this.jwtHelper.isTokenExpired(token)){
            return true;
        }

        return false;
    }

    hasRole(allowedRoles: string[]){
        const token = localStorage.getItem("jwt") ?? "";
        const decodedToken = this.jwtHelper.decodeToken(token);

        if(allowedRoles === undefined)
          return true;

        for(let key in decodedToken){
            if(key.includes("role")){
                return allowedRoles.some(role => decodedToken[key].includes(role));
            }
        }
        return false;
    }

    hasPermission(allowedPermissions: string[], exact: boolean = true){
      const token = localStorage.getItem("jwt") ?? "";
      const decodedToken = this.jwtHelper.decodeToken(token);

      if(allowedPermissions === undefined)
        return true;

      for(let key in decodedToken){
        if(key.includes("permission")){
            if(exact){
              return allowedPermissions.some(permission => decodedToken[key].includes(permission));
            }else{
              return allowedPermissions.some(permission => {
                if(typeof decodedToken[key] === "object"){
                  return decodedToken[key].some((_permission: string) => _permission.includes(permission))
                }else{
                  return decodedToken[key].includes(permission);
                }
              });
            }
        }
      }
      return false;
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Promise<boolean> {
        const roles = route.data['allowedRoles'];
        const permissions = route.data['allowedPermissions'];
        const exactMatch: boolean = route.data['exact'];

        exactMatch === undefined ? true : exactMatch;

        const token = localStorage.getItem("jwt") ?? "";

        if(!this.isAuthenticated(token)){
            return this.router.navigate(['login']);
        }

        if((this.hasRole(roles) && this.hasPermission(permissions, exactMatch)) || this.hasRole(['HumanResource'])){
            return true;
        }

        return this.router.navigate(['AccessDenied']);
    }
}

export const AuthGuard: CanActivateFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Promise<boolean> => {
    return inject(AuthGuardService).canActivate(route, state);
}
