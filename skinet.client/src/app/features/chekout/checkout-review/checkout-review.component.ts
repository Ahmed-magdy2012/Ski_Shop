import { Component, inject, Input } from '@angular/core';
import { CartSignalService } from '../../../api/api/cart-signal.service';
import { CurrencyPipe, CommonModule } from '@angular/common';
import { ConfirmationToken } from '@stripe/stripe-js';
import { AddressPipe } from '../../../Shared/pipes/address.pipe';

@Component({
  selector: 'app-checkout-review',
  standalone: true,
  imports: [CommonModule,
    AddressPipe,
    CurrencyPipe,
  ],
  templateUrl: './checkout-review.component.html',
  styleUrl: './checkout-review.component.css'
})
export class CheckoutReviewComponent {

  cartservice = inject(CartSignalService)
  @Input() confirmation?: ConfirmationToken
}
