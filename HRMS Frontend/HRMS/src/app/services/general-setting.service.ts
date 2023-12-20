import { generalSettingData } from './../models/iGeneralSetting-model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GeneralSettingService {
  api:string = "https://localhost:7073/";
  constructor(public http :HttpClient) { }

  get tokenGetter() {
    return localStorage.getItem("jwt");
  }

  getGeneralSettings():Observable <any>{
    return this.http.get(`${this.api}api/GeneralSettings`,{
      headers: new HttpHeaders({ "Content-Type": "application/json", 'Authorization': `Bearer ${this.tokenGetter}`}),
  }).pipe(
      catchError((error) => {

        return throwError(error);

      })
    );
  }
  createGeneralSettings(generalData:generalSettingData):Observable<any>{
    return this.http.post(`${this.api}General/SaveNew`,generalData,{
      headers: new HttpHeaders({ "Content-Type": "application/json", 'Authorization': `Bearer ${this.tokenGetter}`}),
  }).pipe(
      catchError((error) => {

        return throwError(error);

      })
    )
  }
  editGeneralSettings(generalEditedData:generalSettingData):Observable<any> {
   return this.http.put(`${this.api}Setting/EditGeneral`,generalEditedData,{
    headers: new HttpHeaders({ "Content-Type": "application/json", 'Authorization': `Bearer ${this.tokenGetter}`}),
}).pipe(
    catchError((error) => {

      return throwError(error);

    })
   )
  }
  getGeneralSettingByEmpID(id:number): Observable<any>{
    return this.http.get(`${this.api}GeneralSetting/getbyempid/${id}`,{
      headers: new HttpHeaders({ "Content-Type": "application/json", 'Authorization': `Bearer ${this.tokenGetter}`}),
  }).pipe(
      catchError((error) => {

        return throwError(error);

      })
     )
  }
  getGeneralSettingByID(id:number): Observable<any>{
    return this.http.get(`${this.api}GeneralSetting/getbyid/${id}`,{
      headers: new HttpHeaders({ "Content-Type": "application/json", 'Authorization': `Bearer ${this.tokenGetter}`}),
  }).pipe(
      catchError((error) => {

        return throwError(error);

      })
     )
  }
}
