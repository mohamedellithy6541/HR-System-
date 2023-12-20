import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IRoleCliamsModel } from '../models/roleModel';
import { HttpHeaders } from '@angular/common/http';


@Injectable({
  providedIn: 'root',
})
export class RoleService {
  private apiUrl = 'https://localhost:7073/api';

  constructor(private http: HttpClient) {}
  get tokenGetter() {
    return localStorage.getItem("jwt");
}

  createRoleWithPermissions(data:IRoleCliamsModel): Observable<any> {

    return this.http.post("https://localhost:7073/api/AddRolesWithClaims", data,{
      headers: new HttpHeaders({ "Content-Type": "application/json", 'Authorization': `Bearer ${this.tokenGetter}`}),
  });
  }

}
