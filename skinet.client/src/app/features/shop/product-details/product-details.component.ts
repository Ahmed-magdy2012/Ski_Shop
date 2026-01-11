import { Component, inject, OnInit } from '@angular/core';
import { Product, ProductsService } from '../../../api';
import { ActivatedRoute } from '@angular/router';
import { CurrencyPipe } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatDivider } from '@angular/material/divider';
import { CartSignalService } from '../../../api/api/cart-signal.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-product-details',
  imports: [
    CurrencyPipe, MatButton, MatIcon, MatFormField, MatInput, MatLabel, MatDivider, FormsModule
  ],
  standalone: true,
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.css'
})
export class ProductDetailsComponent implements OnInit {

  ngOnInit(): void {
    this.loadproduct()
  }
  private activatedroute = inject(ActivatedRoute);
  private service = inject(ProductsService);
  private CartSignal=inject(CartSignalService);

  product?: Product
  quantityInCart = 0;
  quantity = 1;

  loadproduct() {
    const id = this.activatedroute.snapshot.paramMap.get('id')
    if (!id) return
    this.service.getProduct(+id).subscribe({
      next: product => {

        this.product = product
        this.UpdateQuantityInCart()
      } 
    })
  }

  UpdateQuantityInCart() {
    this.quantityInCart = this.CartSignal.cart()?.items.find(x => x.productId === this.product?.id)?.quantity || 0
    this.quantity = this.quantityInCart||1

  }

  getButtonText() {

    return this.quantityInCart>0 ?'Update cart':'Add to cart'
  }
  updatecart() {
    if (!this.product) return
    if (this.quantity > this.quantityInCart) {
      const itemstoAdd = this.quantity - this.quantityInCart
      this.quantityInCart = this.quantity
      this.CartSignal.addItemToCart(this.product, itemstoAdd)
    }
    else {
      const itemstoremove = this.quantityInCart - this.quantity
      this.quantityInCart = this.quantity
      this.CartSignal.removeItemFromeCart(this.product.id, itemstoremove)
    }
  }
 
}
