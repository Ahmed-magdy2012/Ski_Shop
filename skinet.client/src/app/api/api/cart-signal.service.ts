import { Injectable } from '@angular/core';
import { computed, inject, signal } from '@angular/core';
import { CartItem, CartService, Product, ShopCart } from '../';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartSignalService {
  private cartservcie = inject(CartService)

  cart = signal<ShopCart | null>(null)

  itemCount = computed(() => {
    return this.cart()?.items.reduce((sum, item) => sum + item.quantity, 0) ?? 0
  })
  Totals = computed(() => {

    const subTotal = this.cart()?.items.reduce((sum, item) => sum + item.price * item.quantity, 0) ?? 0
    const shipping = 0;
    const discount = 0;


    return {
      subTotal,
      shipping,
      discount,
      total: subTotal + shipping - discount
    }

  })

  getCart(id: string) {
    return this.cartservcie.getCartById(id).pipe(
      map(cart => {
        this.cart.set(cart)
        return cart

      }
        
    ))
    
  }
  removeItemFromeCart(productid: number, quantity = 1) {
    const cart = this.cart()
    if (!cart) return;
    const index = cart.items.findIndex(x => x.productId === productid)
    if (index != -1) {
      if (cart.items[index].quantity > quantity) {
        cart.items[index].quantity -= quantity
      }
      else {
        cart.items.splice(index,1)
      }
      if (cart.items.length === 0) {
        this.deleteCart()
        
      }
      else {
        this.setCart(cart)
      }
    }
  }
  deleteCart() {
    this.cartservcie.deleteCart(this.cart()?.id).subscribe({
      next: () => { 
        localStorage.removeItem("cart_id")
        this.cart.set(null)
      }
    })
    }

  setCart(cart: ShopCart) {
    this.cartservcie.updateCart(cart).subscribe({
      next: cart => this.cart.set(cart)
    })
  }



  addItemToCart(item: CartItem | Product, quantity = 1) {

    const cart = this.cart() ?? this.createCart()

    if (this.isproduct(item)) {
      item = this.mapProductToCartitem(item)
    }
    cart.items = this.addorupdateItem(cart.items, item, quantity)
    this.setCart(cart)
    console.log(cart)
  }

  addorupdateItem(items: CartItem[], item: CartItem, quantity: number): CartItem[] {
    const index = items.findIndex(x => x.productId === item.productId)
    if (index === -1) {
      item.quantity = quantity
      items.push(item)
    }
    else {
      items[index].quantity += quantity
    }

    return items;
  }



  private mapProductToCartitem(item: Product): CartItem {

    return {
      productId: item.id,
      propductName: item.name,
      price: item.price,
      quantity: 0,
      type: item.type,
      brand: item.brand,
      pictureUrl: item.pictureUrl,

    }


  }


  private isproduct(item: CartItem | Product): item is Product {
    return (item as Product).id !== undefined
  }



  createCart(): ShopCart {
    const cart = new ShopCart()
    localStorage.setItem("cart_id", cart.id)
    console.log(cart)
    return cart;
  }
}
