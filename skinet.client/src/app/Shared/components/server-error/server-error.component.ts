import { Component, OnInit } from '@angular/core';
import { MatCard } from '@angular/material/card';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  imports: [MatCard],
  standalone: true,
  templateUrl: './server-error.component.html',
  styleUrl: './server-error.component.css'
})
export class ServerErrorComponent {
  error?: Modelerror;
  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation()
    this.error = navigation?.extras.state?.['error'];


    console.log(this.error?.StatusCode)

  }
 
 
}
export interface Modelerror {
 message: string;
  Details: string;
  StatusCode: number;
}
