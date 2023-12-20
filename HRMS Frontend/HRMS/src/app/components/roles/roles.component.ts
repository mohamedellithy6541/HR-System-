import { Component } from '@angular/core';
import { IClaims, IRoleCliamsModel } from 'src/app/models/roleModel';
import { RoleService } from 'src/app/services/role.service';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
})

export class RolesComponent {
  roleName:string='';
  claimss:IClaims[]=[]
  data:IRoleCliamsModel = {roleName:this.roleName,claims:this.claimss};

  pages = [
    {
      name: 'Employee',
      permissions: [ {add: false}, {edit: false}, {delete: false},{ update: false}, {view: false }]
    },
    {
      name: 'GeneralSettings',
      permissions: [ {add: false}, {edit: false}, {delete: false},{ update: false}, {view: false }]
    },
    {
      name: 'Attendance',
      permissions: [ {add: false}, {edit: false}, {delete: false},{ update: false}, {view: false }]
    },
    {
      name: 'Salary',
      permissions: [ {add: false}, {edit: false}, {delete: false},{ update: false}, {view: false }]
    },
    {
      name: 'Department',
      permissions: [ {add: false}, {edit: false}, {delete: false},{ update: false}, {view: false }]
    },
    {
      name: 'SeasonalVacations',
      permissions: [ {add: false}, {edit: false}, {delete: false},{ update: false}, {view: false }]
    },
    // Add more pages as needed
  ];


  constructor(private roleService: RoleService) {
    console.log(this.pages[0].permissions[0]);

  }

  createRole() {
    console.log(this.roleName);

    for(let page of this.pages){
     if(page.permissions[0].add==true)
       this.claimss.push({pageName:page.name,permission: 'Create'})
     if(page.permissions[1].edit==true)
     this.claimss.push({pageName:page.name,permission: 'Edit'})
    if(page.permissions[2].delete==true)
    this.claimss.push({pageName:page.name,permission: 'Delete'})
    if(page.permissions[3].view==true)
    this.claimss.push({pageName:page.name,permission: 'View'})
    }
    this.data={roleName:this.roleName,claims:this.claimss}
    console.log(this.data);

   this.roleService.createRoleWithPermissions(this.data).subscribe({
    next: (response)=>{
      alert('Created Successfully')
    },
    error: (error)=>{
      alert(error.message)
      console.log(error);

    },
    complete: ()=>{
    }
   })
   this.roleName='';
   for(let page of this.pages){
    if(page.permissions[0].add==true)
    page.permissions[0].add=false;
    if(page.permissions[1].edit==true)
    page.permissions[1].edit=false;
   if(page.permissions[2].delete==true)
   page.permissions[2].delete=false;
   if(page.permissions[3].view==true)
   page.permissions[3].view=false;
  }
      }
    }


