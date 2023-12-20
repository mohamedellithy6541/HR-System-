import { IAttendanceModel } from './../../models/iattendance-model';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AttendanceAPIService } from 'src/app/services/attendance-api.service';
import { emptyForm } from 'src/app/validators/AttendanceForm_Empty';
import { checkDates } from 'src/app/validators/AttendanceForm_checkDates';

@Component({
  selector: 'app-attendance',
  templateUrl: './attendance.component.html',
  styleUrls: ['./attendance.component.css']
})
export class AttendanceComponent implements OnInit {

  PageNumber:number=0;
  next:boolean=false;
  AllEmployees:{id:number,firstName:string,lastName:string}[]=[];

  FormValidationState:boolean=false;
  SearchForm: FormGroup;
  attendanceForm=new FormGroup({
    empID:new FormControl(0,Validators.required),
    arrivalTime: new FormControl("",Validators.required),
    departureTime: new FormControl("",Validators.required),
    attendanceDate: new FormControl("",Validators.required)
  });

  DataModel:IAttendanceModel[]=[];
  AttendanceModel:IAttendanceModel=this.initializeAttendanceModel();

  constructor(private api : AttendanceAPIService,private fb: FormBuilder){
    this.SearchForm = this.fb.group(({
      name: new FormControl(""),
      dateF: new FormControl(""),
      dateT: new FormControl("")
    }), {
      validators: [checkDates(),emptyForm()]
    });
  }
  ngOnInit(): void {
    this.api.getEmployees().subscribe({
        next:(response:any)=>{
          this.AllEmployees = response;
        },
        error:()=>{
          window.alert("error in loading employees data")
        },
        complete:()=>{}
      });
  }

  getDataSubmit(e:Event){
    e.preventDefault()
    this.PageNumber=0;
    this.getData()
  }
  getData(){
    if(this.SearchForm.invalid){
      this.FormValidationState = true;
      return;
    }
    this.FormValidationState=false;

    console.log(this.SearchForm.get("dateF")?.value);
    console.log(this.SearchForm.get("dateT")?.value);


    if(this.SearchForm.get("name")?.value){
      if (this.SearchForm.get("dateF")?.value && this.SearchForm.get("dateT")?.value) {
        this.api.getAllAttendanceByNameByDates(this.SearchForm.get("name")?.value,
        new Date(this.SearchForm.get("dateF")?.value),
        new Date(this.SearchForm.get("dateT")?.value),this.PageNumber).subscribe({
            next:(response:any)=>{
              this.DataModel = response.body;
              this.next= response.headers.get("next")==="true"?true:false;
            },
            error:()=>{
              window.alert("error in retrieving data")
            },
            complete:()=>{
            }
          });
      }
      else{
        this.api.getAllAttendanceByName(this.SearchForm.get("name")?.value,this.PageNumber).subscribe({
            next:(response:any)=>{
              this.DataModel = response.body;
              this.next= response.headers.get("next")==="true"?true:false;
            },
            error:()=>{
              window.alert("error in retrieving data")
            },
            complete:()=>{
            }
          });
      }
    }
    else{
      if (this.SearchForm.get("dateF")?.value && this.SearchForm.get("dateT")?.value) {

        this.api.getAllAttendanceByDates(new Date(this.SearchForm.get("dateF")?.value),
        new Date(this.SearchForm.get("dateT")?.value),this.PageNumber).subscribe({
            next:(response:any)=>{
              this.DataModel = response.body;
              this.next= response.headers.get("next")==="true"?true:false;
            },
            error:()=>{
              window.alert("error in retrieving data")
            },
            complete:()=>{
            }
        });
      }
      else{
        this.api.getAllAttendance(this.PageNumber).subscribe({
            next:(response:any)=>{
              this.DataModel = response.body;
              this.next= response.headers.get("next")==="true"?true:false;
            },
            error:()=>{
              window.alert("error in retrieving data")
            },
            complete:()=>{
            }
        });
      }
    }
  }

  // filterDataByDate(){
  //   const data:IAttendanceModel[] = this.DataModel;
  //   this.DataModel=[];
  //   for (const key of data) {
  //     if (new Date(key.attendanceDate).toISOString().split('T')[0]
  //     >=new Date(this.SearchForm.get("dateF")?.value).toISOString().split('T')[0]
  //     && new Date(key.attendanceDate).toISOString().split('T')[0]
  //     <=new Date(this.SearchForm.get("dateT")?.value).toISOString().split('T')[0])
  //     {
  //       this.DataModel.push(key)
  //     }
  //   }
  // }
  addAttendance(){

    this.AttendanceModel=this.initializeAttendanceModel();

    this.attendanceForm.get("arrivalTime")?.setValue("");
    this.attendanceForm.get("departureTime")?.setValue("");
    this.attendanceForm.get("attendanceDate")?.setValue("");

    this.attendanceForm.markAsUntouched();
  }
  editAttendance(DTOModel:IAttendanceModel){
    this.AttendanceModel=DTOModel;

    let arrivalTime:String=this.AttendanceModel.arrivalTime.toString().split('T')[1];
    let departureTime:String=this.AttendanceModel.departureTime.toString().split('T')[1];

    this.attendanceForm.get("arrivalTime")?.setValue(arrivalTime.substring(0,arrivalTime.lastIndexOf(':')));
    this.attendanceForm.get("departureTime")?.setValue(departureTime.substring(0,departureTime.lastIndexOf(':')));
    this.attendanceForm.get("attendanceDate")?.setValue(this.AttendanceModel.attendanceDate.toString().split('T')[0]);
    this.attendanceForm.get("empID")?.setValue(this.AllEmployees.find(x=>x.id==this.AttendanceModel.emp_ID)?.id??0 )
  }
  deleteAttendance(attendanceID:any){
    const userConfirmed = window.confirm('Do you really want to delete this?');

    if (userConfirmed) {
      this.api.deleteAttendance(attendanceID).subscribe({
        next:(response:any)=>{
        },
        error:()=>{
          window.alert("Can't delete since error is existed");
        },
        complete:()=>{
          window.alert("the attendance is deleted successfully");
          this.getData()
        }
      });
    }
  }

  initializeAttendanceModel():IAttendanceModel{
    return {
      id: 0,
      emp_ID: 0,
      emp_Name: "",
      dept_Name: "",
      arrivalTime: new Date(),
      departureTime: new Date(),
      attendanceDate: new Date()
    }
  }

  attendanceModelFormFunction(e:Event){

    e.preventDefault()

    if(this.attendanceForm.invalid)
      return;

    let empID:any =this.attendanceForm.get("empID")?.value;
    this.AttendanceModel.emp_ID= parseInt(empID);

    this.AttendanceModel.arrivalTime = this.convertTimeToDate(this.attendanceForm.get('arrivalTime')?.value);
    this.AttendanceModel.departureTime = this.convertTimeToDate(this.attendanceForm.get('departureTime')?.value)
    let date:any = this.attendanceForm.get('attendanceDate')?.value;
    this.AttendanceModel.attendanceDate = new Date(date);

    if (!this.AttendanceModel.dept_Name ) {
      this.AttendanceModel.id=null;
      this.api.addAttendance(this.AttendanceModel).subscribe({
        next:()=>{},
        error:()=>{
          window.alert("error in adding")
        },
        complete:()=>{
          window.alert("successfully added")
          this.getData()
        }
      });
    }
    else {
      this.api.editAttendance(this.AttendanceModel).subscribe({
        next:()=>{},
        error:()=>{
          window.alert("error in updating")
        },
        complete:()=>{
          window.alert("successfully updated")
          this.getData()
        }
      });
    }
  }
  convertTimeToDate(time:any):Date{
    let date:Date=new Date();
    date.setUTCHours(parseInt(time?.split(":")[0]));
    date.setUTCMinutes(parseInt(time.split(":")[1]));
    return date;
  }
  getOtherPage(PNumber:number){
    PNumber>0 ? this.PageNumber++ : this.PageNumber--;
    this.getData()
  }
}
