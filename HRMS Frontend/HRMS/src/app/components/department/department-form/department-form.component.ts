import { Router } from '@angular/router';
import { DepartmentService } from './../../../services/department.service';
import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-department-form',
  templateUrl: './department-form.component.html',
  styleUrls: ['./department-form.component.css']
})
export class DepartmentFormComponent {
  
  constructor(private departmentService:DepartmentService,private router:Router){}
  departmentForm: FormGroup= new FormGroup(
   { name:new FormControl("",Validators.required)}
  );
  
  submitForm(e:Event){
    e.preventDefault()
    if(this.departmentForm.invalid){
      this.departmentForm.get("name")?.markAsTouched
      return;
    }
    this.departmentService.AddDepartment({name:this.departmentForm.get("name")?.value,id:0}).subscribe({
      next: () => {
        this.router.navigate(["/department"])
      },
      error: () => {
        window.alert('Error in adding department');
      }
    });
  }

}
