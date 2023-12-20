import { SalaryDataModel } from './Salary-Data-model';
export type PaginatedDTOModel = {
  salaryDTOs: SalaryDataModel[];
  pageCount: number;
};
