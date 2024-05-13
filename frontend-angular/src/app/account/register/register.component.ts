import { Component } from '@angular/core';
import {  AbstractControl, AsyncValidatorFn, FormBuilder, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';
import { debounceTime, finalize, map, switchMap, take } from 'rxjs';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})

export class RegisterComponent {

  constructor(private fb: FormBuilder,
    private accountService: AccountService,
    private router: Router) {

  }

  complexPassword = '(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&\-_])[A-Za-z\d$@$!%*?&\-_].{8,}';

  registerForm = this.fb.group({
    displayName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email], [this.validateEmailNotTaken()]],
    password: ['', [Validators.required, Validators.pattern(this.complexPassword)]]
  })

  onSubmit() {
    let form = this.registerForm.value;
    this.accountService.register(form.email!, form.displayName!, form.password!).subscribe({
      next: () => this.router.navigateByUrl('/shop'),
      error: error => console.log(error)
    })
  }

  validateEmailNotTaken(): AsyncValidatorFn {
    return (control: AbstractControl) => {
      return control.valueChanges.pipe(
        debounceTime(1000),
        take(1),
        switchMap(() => {
          return this.accountService.checkEmailExists(control.value).pipe(
            map(result => result ? { emailExists: true } : null),
            finalize(() => control.markAsTouched())
          )
        })
      )
    };
  }
}
