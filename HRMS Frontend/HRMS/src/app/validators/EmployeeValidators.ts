import { AbstractControl, ValidatorFn } from '@angular/forms';
import * as moment from 'moment';

export function empAge(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: boolean } | null => {
    const value = control.value;

    let formattedDate = moment().format('YYYY-MM');

    let age:number=parseInt(formattedDate.toString().split("-")[0])-parseInt(value.toString().split("-")[0])+
    (parseInt(formattedDate.toString().split("-")[1])-parseInt(value.toString().split("-")[1]))/12;

    if (age<20.0) {
      return { 'Age': true }; // Return an error object if validation fails
    }

    return null; // Return null if validation passes
  };
}

export function hireDate(): ValidatorFn {
    return (control: AbstractControl): { [key: string]: boolean } | null => {
      const value = control.value.toString();
  
      let formattedDate:Date = new Date('2008-01-01T00:00:00');
  
      if (formattedDate>new Date(value)) {
        return { 'hireDate': true }; // Return an error object if validation fails
      }
  
      return null; // Return null if validation passes
    };
  }
