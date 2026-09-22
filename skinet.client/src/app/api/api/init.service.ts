import { inject, Injectable } from '@angular/core';
import { forkJoin, map, of, tap } from 'rxjs';
import { CartSignalService } from './cart-signal.service';
import { SignalRService } from './signal-r.service';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class InitService {
  private CheckingLoggedIn = inject(AccountService)
  private cartservice = inject(CartSignalService)
  private signalservice = inject(SignalRService)
  init() {
    const cardId = localStorage.getItem("cart_id")
    const cart = cardId ? this.cartservice.getCart(cardId) : of(null)

    return forkJoin({
      cart: cart,
      user: this.CheckingLoggedIn.getUserInfo().pipe(
        tap(user => {
          if (user) this.signalservice.createHub()

        })

      )
    })
  }
}
