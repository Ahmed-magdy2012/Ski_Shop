import { Component, OnInit } from '@angular/core';
import { inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatCard } from '@angular/material/card';
import { MatFormField, MatInput, MatLabel } from '@angular/material/input';
import { AccountForclientService } from '../../../api/api/account-forclient.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  standalone:true,
  selector: 'app-login',
  imports: [ReactiveFormsModule,
    MatCard, MatInput, MatLabel, MatFormField, MatButton
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  private fb = inject(FormBuilder);
  private accountService = inject(AccountForclientService);
  private router = inject(Router)
  private activatedRoute = inject(ActivatedRoute)
  returnUrl = '/shop'
  ngOnInit() {
    const url = this.activatedRoute.snapshot.queryParams['returnUrl'];
    if (url) this.returnUrl = url
  }

  loginForm = this.fb.group({
    email: [""],
    password:[""]
  })
  onSubmit() {
    this.accountService.login(this.loginForm.value, this.returnUrl)
  

  }
}
