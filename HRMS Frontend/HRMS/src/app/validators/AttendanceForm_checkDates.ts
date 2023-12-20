import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function checkDates():ValidatorFn{
    return (control:AbstractControl):ValidationErrors|null=>{
                let dateFromControl =control.get("dateF");
                let dateToControl =control.get("dateT");
                let dateF = new Date(dateFromControl?.value) 
                let dateT = new Date(dateToControl?.value) 
                if(dateT<dateF || (dateToControl?.value && !dateFromControl?.value)){
                    return {checkDate:true}
                }
            return null;
        }
    
}