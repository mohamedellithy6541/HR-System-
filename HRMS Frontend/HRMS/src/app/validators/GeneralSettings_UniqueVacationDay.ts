import { AbstractControl } from '@angular/forms';

export function uniqueValueValidator(control: AbstractControl) {
    const formGroup = control.parent;
    if (formGroup) {
      const uniqueValue = control.value;

      if(uniqueValue!=='null'){
        let otherControl = formGroup.get('vacationDay1Control');
        if(otherControl === control)
        {
          otherControl = formGroup.get('vacationDay2Control');
        }
        if (uniqueValue === otherControl?.value &&uniqueValue!='null') {
          return { notUnique: true };
        }
      }
    }
    return null;
  }

