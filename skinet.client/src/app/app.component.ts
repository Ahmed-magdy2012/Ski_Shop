import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { HeaderComponent } from './Layout/header/header.component';
import { RouterOutlet } from '@angular/router';
import { ShopComponent } from './features/shop/shop.component';
import { FilterDialogComponent } from './features/shop/filter-dialog/filter-dialog.component';



@Component({
  imports: [RouterOutlet, HeaderComponent, ShopComponent, ],
  standalone: true,
  selector: 'app-root',
  templateUrl: './app.component.html', 
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'skinet.client';
}


 
    
  

