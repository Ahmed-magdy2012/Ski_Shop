import { inject, Injectable } from '@angular/core';
import { of } from 'rxjs';
import { CartSignalService } from './cart-signal.service';

@Injectable({
  providedIn: 'root'
})
export class InitService {
  private cartservice = inject(CartSignalService)
  init() {
    const cardId = localStorage.getItem("cart_id")
    const cart = cardId ? this.cartservice.getCart(cardId) : of(null)

    return cart
  }
}
