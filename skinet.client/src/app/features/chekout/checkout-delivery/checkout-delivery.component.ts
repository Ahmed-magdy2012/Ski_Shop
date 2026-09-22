import { Component, OnInit, output, signal } from '@angular/core';
import { inject } from '@angular/core';
import { CheckoutService } from '../../../api/api/checkout.service';
import { MatRadioModule } from '@angular/material/radio';
import { CurrencyPipe } from '@angular/common';
import { CartSignalService } from '../../../api/api/cart-signal.service';
import { Deliverymethod } from '../../../api/model/deliverymethod';

@Component({
  selector: 'app-checkout-delivery',
  standalone: true,
  imports: [
    CurrencyPipe,
    MatRadioModule],
  templateUrl: './checkout-delivery.component.html',
  styleUrl: './checkout-delivery.component.css'
})
export class CheckoutDeliveryComponent implements OnInit {
  cartservice = inject(CartSignalService)
  checkoutService = inject(CheckoutService)
  deliveryComplete = output<boolean>()

  ngOnInit(): void {
    this.checkoutService.getDeliverymethods().subscribe({
      next: methods => {

        if (this.cartservice.cart()?.deliverymethodId) {
          const method = methods.find(x => x.id === this.cartservice.cart()?.deliverymethodId)
          if (method) {
            this.cartservice.selectedDelivery.set(method);
            this.deliveryComplete.emit(true)
           
    }
  }
      


      }


    });


  }
  updateDeliverymethod(method: Deliverymethod) {
    this.cartservice.selectedDelivery.set(method)
    const cart = this.cartservice.cart()
    if (cart) {
      cart.deliverymethodId = method.id
      this.cartservice.setCart(cart)
      this.deliveryComplete.emit(true)
    }
  }
}
