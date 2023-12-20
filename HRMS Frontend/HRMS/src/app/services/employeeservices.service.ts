import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IEmployee } from '../models/iemployee';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class EmployeeservicesService {
  constructor(public http: HttpClient) {}

  get tokenGetter() {
    return localStorage.getItem("jwt");
  }

  GetAllEmployees(): any
  {
   return this.http.get("https://localhost:7073/api/Employee",{
    headers: new HttpHeaders({ "Content-Type": "application/json", 'Authorization': `Bearer ${this.tokenGetter}`}),
  })
  }

  GetEmployee(id: number): any {
    return this.http.get('https://localhost:7073/api/Employee/' + id,{
      headers: new HttpHeaders({ "Content-Type": "application/json", 'Authorization': `Bearer ${this.tokenGetter}`}),
    });
  }

  AddEmployee(AddEmployee: IEmployee): any {
    return this.http.post('https://localhost:7073/api/Employee', AddEmployee,{
      headers: new HttpHeaders({ "Content-Type": "application/json", 'Authorization': `Bearer ${this.tokenGetter}`}),
    });
  }
  deleteEmployee(employeeid: number) {
    return this.http.delete(
      'https://localhost:7073/api/Employee/' + employeeid,{
        headers: new HttpHeaders({ "Content-Type": "application/json", 'Authorization': `Bearer ${this.tokenGetter}`}),
      }
    );
  }
  editEmployee(employee: IEmployee) {
    return this.http.put(
      'https://localhost:7073/api/Employee/' + employee.id,
      employee,{
        headers: new HttpHeaders({ "Content-Type": "application/json", 'Authorization': `Bearer ${this.tokenGetter}`}),
      }
    );
  }
}
