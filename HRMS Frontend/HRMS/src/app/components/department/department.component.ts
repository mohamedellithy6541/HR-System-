import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { IDepartment } from 'src/app/models/IDepartment';
import { AuthGuardService } from 'src/app/services/auth-guard.service';
import { DepartmentService } from 'src/app/services/department.service';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent {
  departments:IDepartment[]=[];
  constructor(private departmentService:DepartmentService, private authGuard: AuthGuardService, private router:Router){
    this.getAlldepartments();
  }

  hasPermissions(permissions: string[]){
    return this.authGuard.hasPermission(permissions) || this.authGuard.hasRole(['HumanResource']);
  }


  deleteDepartment(id:number){
    this.departmentService.deleteDepartment(id).subscribe({
      next: () => {
        this.getAlldepartments();
      },
      error: () => {
      },
      complete:()=>{
        window.alert('department deleted successfully');
      }
    });
  }
  getAlldepartments(){
    this.departmentService.GetAllDepartments().subscribe({
      next: (response:any) => {
        this.departments=response;
      },
      error: () => {
        window.alert('Error in getting department');
      }
    });
  }
}
