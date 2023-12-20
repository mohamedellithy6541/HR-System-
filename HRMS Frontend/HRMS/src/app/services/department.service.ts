import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IDepartment } from '../models/IDepartment';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  constructor(public http :HttpClient) { }

  get tokenGetter() {
    return localStorage.getItem("jwt");
  }

  GetAllDepartments():any
  {
   return this.http.get("https://localhost:7073/api/Department", {
    headers: new HttpHeaders({ "Content-Type": "application/json", 'Authorization': `Bearer ${this.tokenGetter}`}),

   })
  }

  GetDepartment(id:number):any
  {
   return this.http.get("https://localhost:7073/api/Department/"+id, {
    headers: new HttpHeaders({ "Content-Type": "application/json", 'Authorization': `Bearer ${this.tokenGetter}`}),

   })
  }

  AddDepartment(department:IDepartment):any
  {
   return this.http.post("https://localhost:7073/api/Department",department, {
    headers: new HttpHeaders({ "Content-Type": "application/json", 'Authorization': `Bearer ${this.tokenGetter}`}),

   })
  }
  deleteDepartment(id:number):any
  {
   return this.http.delete("https://localhost:7073/api/Department/"+id, {
    headers: new HttpHeaders({ "Content-Type": "application/json", 'Authorization': `Bearer ${this.tokenGetter}`}),

   })
  }
}
