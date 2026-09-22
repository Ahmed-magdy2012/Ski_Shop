 import { Component, OnDestroy } from '@angular/core';
import { inject } from '@angular/core';
import { RouterLink } from '@angular/router'
import { MatButton } from '@angular/material/button';
import { SignalRService } from '../../../api/api/signal-r.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { CurrencyPipe, DatePipe, NgIf } from '@angular/common';
import { AddressPipe } from '../../../Shared/pipes/address.pipe';
import { PaymentPipe } from '../../../Shared/pipes/payment.pipe'
import { OrdersService } from '../../../api/api/orders.service';
import { ChekoutComponent } from '../chekout.component';
@Component({
  selector: 'app-checkout-success',
  standalone: true,
  imports: [
    MatButton,
    RouterLink,
    DatePipe,
    AddressPipe,
    CurrencyPipe,
    PaymentPipe, 
    MatProgressSpinnerModule,
    NgIf, 
    ChekoutComponent
  ],
  templateUrl: './checkout-success.component.html',
  styleUrl: './checkout-success.component.css'
})
export class CheckoutSuccessComponent implements OnDestroy {
  signalService = inject(SignalRService);
  private orderService = inject(OrdersService)
  private check = inject(ChekoutComponent)

  ngOnDestroy(): void {
    this.check.orderComplete = false
    this.signalService.ordersignal.set(null)

  }
}
