import { Injectable,inject } from '@angular/core';
import { Deliverymethod } from '../model/deliverymethod';
import { PaymentsService } from './payments.service';
import { map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
   methods = inject(PaymentsService)
  deliveryMethods: Deliverymethod[] = []




  getDeliverymethods() {
    if (this.deliveryMethods.length > 0) return of(this.deliveryMethods)
    return this.methods.getDeliveryMethods().pipe(
      map(
        methods => {
          this.deliveryMethods = methods.sort((a, b) => b.price - a.price)
          return methods
        }
      )

    )
  }



}
