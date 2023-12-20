import { EmployeeservicesService } from './../../services/employeeservices.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IEmployee } from 'src/app/models/iemployee';
import { AuthGuardService } from 'src/app/services/auth-guard.service';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {
  employeeForm :FormGroup | undefined;
  selectedEmployee: IEmployee | null = null;
  employees: IEmployee[] = [];
  Employee:any;
  constructor(private _Employeeservices:EmployeeservicesService, private authGuard: AuthGuardService, private router:Router) {}

  ngOnInit() : void {
      this._Employeeservices.GetAllEmployees().subscribe({
        next: (response:any)=>{
          this.employees=response;
        },
        error:()=>{},
        complete:()=>{},
      });
    };

  hasPermissions(permissions: string[]){
    return this.authGuard.hasPermission(permissions) || this.authGuard.hasRole(['HumanResource']);
  }

  deleteEmployee(EmployeeId:number)
  {
    const userConfirmed = window.confirm('Do you really want to delete this?');

    if (userConfirmed)
     {
    this._Employeeservices.deleteEmployee(EmployeeId).subscribe(
    {
      next: ()=>{
        this.employees=this.employees.filter(
          (Employee:any)=>Employee.id!=EmployeeId);
      },

    })};
  };
  editEmployee(id:number)
  {
    this.router.navigate([`/employee/${id}`]);
  };
}



