import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function emptyForm():ValidatorFn{
    return (control:AbstractControl):ValidationErrors|null=>{
                let dateFromControl = control.get("dateF")?.value;
                let dateToControl = control.get("dateT")?.value;
                let nameControl = control.get("name")?.value ;
                if(!nameControl && !dateToControl && !dateFromControl){
                    return {EmptyForm:true}
                }
            return null;
        }
    
}