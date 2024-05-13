import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
  });
  returnUrl: string;

  constructor(private accountService: AccountService,
    private router: Router,
    private activatedRout: ActivatedRoute)
  {
    this.returnUrl = this.activatedRout.snapshot.queryParams['returnUrl'] || '/shop'
  }

  onSubmit() {
    const formData = this.loginForm.value;
    this.accountService.login(formData.email!, formData.password!).subscribe({
      next: responce => this.router.navigateByUrl(this.returnUrl)
    });
  }
}
