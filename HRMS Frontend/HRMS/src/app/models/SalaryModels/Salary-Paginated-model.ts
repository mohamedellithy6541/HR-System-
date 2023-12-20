export class SalaryPaginatedModel {
  pageNumber: number;
  pageSize: number;
  search: string;
  month: string;
  year: string;

  constructor(
    pageNumber: number,  pageSize: number,  search: string, month: string, year: string
  ) {
    this.pageNumber = pageNumber;
    this.pageSize = pageSize;
    this.search = search;
    this.month = month;
    this.year = year;
  }
}
