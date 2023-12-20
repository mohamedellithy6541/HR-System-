import { WeekDay } from "@angular/common";
import { FormControl } from "@angular/forms";

export interface generalSettingData{
    Id:number,
    Bonus:number|null,
    Discount:number|null,
    EmployeeID:number|null,
    VacationDay1:string|null,
    VacationDay2:string|null,

}
