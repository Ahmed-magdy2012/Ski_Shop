import { inject, Injectable } from '@angular/core';
import { forkJoin, of } from 'rxjs';
import { CartSignalService } from './cart-signal.service';
import { AccountForclientService } from './account-forclient.service';

@Injectable({
  providedIn: 'root'
})
export class InitService {
  private CheckingLoggedIn = inject(AccountForclientService)
  private cartservice = inject(CartSignalService)
  init() {
    const cardId = localStorage.getItem("cart_id")
    const cart = cardId ? this.cartservice.getCart(cardId) : of(null)

    return forkJoin({
      cart: cart,
      user: this.CheckingLoggedIn.getUserInfo()
    })
  }
}
