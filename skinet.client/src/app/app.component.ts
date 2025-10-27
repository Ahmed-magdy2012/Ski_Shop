import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from './Layout/header/header.component';
import { RouterOutlet } from '@angular/router';



@Component({
  imports: [HeaderComponent],
  standalone: true,
  selector: 'app-root',
  templateUrl: './app.component.html', 
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'skinet.client';
}


 
    
  

