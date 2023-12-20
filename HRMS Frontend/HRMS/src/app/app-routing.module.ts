import { DepartmentFormComponent } from './components/department/department-form/department-form.component';
import { NgModule, Component } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddEmpComponent } from './components/employee/AddEmployee/add-emp/add-emp.component';
import { EmployeeComponent } from './components/employee/employee.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/user/login/login.component';
import { RegisterComponent } from './components/user/register/register.component';
import { AuthGuard } from './services/auth-guard.service';
import { AttendanceComponent } from './components/attendance/attendance.component';
import { VacationsComponent } from './components/vacations/vacations.component';
import { DepartmentComponent } from './components/department/department.component';
import { SalaryComponent } from './components/salary/salary.component';
import { GeneralSettingComponent } from './components/general-setting/general-setting.component';
import { RolesComponent } from './components/roles/roles.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { AccessDeniedComponent } from './components/access-denied/access-denied.component';

const routes: Routes = [
    {path:'', redirectTo: 'home', pathMatch: 'full'},
    {path:'home', component: HomeComponent},
    {path:'employee',component:EmployeeComponent, data: {allowedPermissions: ['Employee'], exact: false}, canActivate: [AuthGuard]},
    {path:'employee/:id',component:AddEmpComponent, data: {allowedPermissions: ['Employee.View'], exact: true}, canActivate: [AuthGuard]},
    {path:'login', component: LoginComponent},
    {path:'register', component: RegisterComponent, data: {allowedRoles: ['HumanResource']}, canActivate: [AuthGuard]},
    {path:'attendance',component: AttendanceComponent, data: {allowedPermissions: ['Attendance'], exact: false}, canActivate: [AuthGuard]},
    {path:'vacations',component: VacationsComponent, data: {allowedPermissions: ['SeasonalVacations'], exact: false}, canActivate: [AuthGuard]},
    {path:'departmentForm',component: DepartmentFormComponent, data: {allowedPermissions: ['Department.Create'], exact: true}, canActivate: [AuthGuard]},
    {path:'department',component: DepartmentComponent, data: {allowedPermissions: ['Department'], exact: false}, canActivate: [AuthGuard]},
    {path:'salary',component: SalaryComponent, data: {allowedPermissions: ['Salary'], exact: false}, canActivate: [AuthGuard]},
    {path:'GeneralSettings',component:GeneralSettingComponent, data: {allowedPermissions: ['GeneralSettings'], exact: false}, canActivate: [AuthGuard]},
    {path:'roles',component:RolesComponent, data: {allowedRoles: ['HumanResource']}, canActivate: [AuthGuard]},
    {path: 'AccessDenied', component: AccessDeniedComponent},
    {path:'**',component: NotFoundComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
