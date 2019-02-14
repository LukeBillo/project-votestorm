import {AbstractControl} from "@angular/forms";

export function notEmptyValidator(control: AbstractControl): {[key: string]: any} | null {
  return control.value.trim().length === 0 ? { 'forbiddenName': {value: control.value} } : null;
}

