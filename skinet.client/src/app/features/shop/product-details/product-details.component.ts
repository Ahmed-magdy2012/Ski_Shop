import { Component, inject, OnInit } from '@angular/core';
import { Product, ProductsService } from '../../../api';
import { ActivatedRoute } from '@angular/router';
import { CurrencyPipe } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatDivider } from '@angular/material/divider';

@Component({
  selector: 'app-product-details',
  imports: [
    CurrencyPipe, MatButton, MatIcon, MatFormField, MatInput, MatLabel,MatDivider
  ],
  standalone: true,
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css'
})
export class ProductDetailsComponent implements OnInit {

  ngOnInit(): void {
    this.loadproduct()
  }

  private activatedroute = inject(ActivatedRoute)
  private service = inject(ProductsService)
  product?: Product


  loadproduct() {
    const id = this.activatedroute.snapshot.paramMap.get('id')
    if (!id) return
    this.service.getProduct(+id).subscribe({
      next: product => this.product = product
    })
  }

  
 
}
