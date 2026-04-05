import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatError, MatFormField, MatLabel } from '@angular/material/form-field';
import { MatCard } from '@angular/material/card';
import { MatInput } from '@angular/material/input';
import { AccountForclientService } from '../../../api/api/account-forclient.service';
import { Router } from '@angular/router';
import { JsonPipe } from '@angular/common';

import { SnackbarService } from '../../../api/api/snackbar.service';
import { User } from '../../../api';
import { TextInputComponent } from '../../../Shared/components/text-input/text-input.component';

@Component({
  selector: 'app-register',
  imports: [
    ReactiveFormsModule,
    MatCard,
    MatInput,
    MatFormField,
    MatButton,
    MatLabel,
    JsonPipe, MatError,
    TextInputComponent
    
  ],
  standalone: true,
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private accountservice = inject(AccountForclientService)
  private snack = inject(SnackbarService)
  validationErrors?: string[];

  registerForm = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: ['',Validators.required]


  })
 
  onSubmit() {
     const user:User = this.registerForm.getRawValue() 
    this.accountservice.register(user).subscribe({

      next: () => {
        this.snack.Success('Resgistration successful - you can login now')
        this.router.navigateByUrl('/login')
      },
      error: errors => this.validationErrors = errors

    })



  }

}
