import { SalaryPaginatedModel } from './../models/SalaryModels/Salary-Paginated-model';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaginatedDTOModel } from '../models/SalaryModels/Paginated-DTO-Model';

@Injectable({
  providedIn: 'root',
})
export class SalaryApiService {
  constructor(public http: HttpClient) {}

  get tokenGetter() {
    return localStorage.getItem("jwt");
  }

  getSalaries(
    salaryPaginatedModel: SalaryPaginatedModel
  ): Observable<PaginatedDTOModel> {
    return this.http.get<PaginatedDTOModel>(
      'https://localhost:7073/api/Salary/Get?pageNumber=' +
        salaryPaginatedModel.pageNumber +
        '&pageSize=' +
        salaryPaginatedModel.pageSize +
        '&search=' +
        salaryPaginatedModel.search +
        '&month=' +
        salaryPaginatedModel.month +
        '&year=' +
        salaryPaginatedModel.year
        ,{
          headers: new HttpHeaders({ "Content-Type": "application/json", 'Authorization': `Bearer ${this.tokenGetter}`}),
        });
  }
}
