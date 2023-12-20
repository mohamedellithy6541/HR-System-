import { Component } from '@angular/core';
import { SalaryDataModel } from 'src/app/models/SalaryModels/Salary-Data-model';
import { SalaryPaginatedModel } from 'src/app/models/SalaryModels/Salary-Paginated-model';
import { SalaryApiService } from 'src/app/services/Salary-Api.service';
import { AuthGuardService } from 'src/app/services/auth-guard.service';

@Component({
  selector: 'app-salary',
  templateUrl: './salary.component.html',
  styleUrls: ['./salary.component.css'],
})
export class SalaryComponent {
  years: number[];
  search: string;
  month: string;
  year: string;
  salaryPaginatedList: SalaryDataModel[];
  pageSize: number;
  pageNumber: number;
  pageCount: number;
  constructor(private service: SalaryApiService, private authGuard: AuthGuardService) {
    // Create an array of years for the last two years
    this.years = [];
    this.search = '';
    this.month = '';
    this.year = '';
    this.pageSize = 5;
    this.pageNumber = 1;
    this.pageCount = 0;
    this.salaryPaginatedList = [];
    const currentYear = new Date().getFullYear();
    for (let year = currentYear - 20; year <= currentYear; year++) {
      this.years.push(year);
    }
  }

  ngOnInit(): void {
      this.getData();
  }

  hasPermissions(permissions: string[]){
    return this.authGuard.hasPermission(permissions) || this.authGuard.hasRole(['HumanResource']);
  }

  getData(): void {
    this.service
      .getSalaries(
        new SalaryPaginatedModel(
          this.pageNumber,
          this.pageSize,
          this.search,
          this.month,
          this.year
        )
      )
      .subscribe((res) => {
        this.salaryPaginatedList = res.salaryDTOs;
        this.pageCount = res.pageCount;
      });
  }

  onSearch(): void {
    this.pageNumber = 1;
    this.getData();
  }
  setSearch(value: string): void {
    this.search = value;
  }

  setMonth(value: string): void {
    this.month = value;
  }

  setYear(value: string): void {
    this.year = value;
  }

  next(): void {
    if (this.pageNumber < this.pageCount) {
      this.pageNumber++;
      this.getData();
    }
  }

  previous(): void {
    if (this.pageNumber > 1) {
      this.pageNumber--;
      this.getData();
    }
  }
}
