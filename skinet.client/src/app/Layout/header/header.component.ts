import { Component, OnInit } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { MatBadge } from '@angular/material/badge'
import { ProductsService } from '../../api/api/products.service'
import { Product } from '../../api/model/product'
@Component({
  imports: [
    MatIcon,
    MatButton,
    MatBadge
  ],
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {
  products: Product[]=[];
  constructor(private product: ProductsService) {

  }


  ngOnInit(): void{
    this.product.getProducts().subscribe({
      next: response => this.products = response,
      error: er => console.log(er)
    })
  }
}
