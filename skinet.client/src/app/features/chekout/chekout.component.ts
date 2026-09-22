import { Component, OnDestroy, OnInit, signal } from '@angular/core';
import { OrdarSummaryComponent } from '../../Shared/components/ordar-summary/ordar-summary.component';
import { MatStepper, MatStepperModule } from '@angular/material/stepper';
import { MatCheckboxChange, MatCheckboxModule } from '@angular/material/checkbox';
import { Router, RouterLink } from '@angular/router';
import { MatButton } from '@angular/material/button';
import { inject } from '@angular/core';
import { StripeService } from '../../Core/stripe.service';
import { ConfirmationToken, Stripe, StripeAddressElement, StripeAddressElementChangeEvent, StripePaymentElement, StripePaymentElementChangeEvent } from '@stripe/stripe-js';
import { SnackbarService } from '../../api/api/snackbar.service';
import { StepperSelectionEvent } from '@angular/cdk/stepper';
import { AccountForclientService } from '../../api/api/account-forclient.service';
import { firstValueFrom } from 'rxjs';
import { AddressDto } from '../../api/model/addressDto';
import { CheckoutDeliveryComponent } from './checkout-delivery/checkout-delivery.component';
import { CheckoutReviewComponent } from './checkout-review/checkout-review.component';
import { CurrencyPipe, JsonPipe } from '@angular/common';
import { CartSignalService } from '../../api/api/cart-signal.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ShippingAddress } from '../../api/model/shippingAddress';
import { CreateOrderDTO } from '../../api/model/createOrderDTO';
import { OrdersService } from '../../api/api/orders.service';
import { Order } from '../../api/model/order';

@Component({
  selector: 'app-chekout',
  standalone: true,
  imports: [
    CheckoutReviewComponent,
    CheckoutDeliveryComponent,
    OrdarSummaryComponent,
    MatStepperModule, MatButton,
    RouterLink,
    MatCheckboxModule,
    CurrencyPipe, JsonPipe,
    MatProgressSpinnerModule
  ],
  templateUrl: './chekout.component.html',
  styleUrl: './chekout.component.css'
})
export class ChekoutComponent implements OnInit, OnDestroy {
  router = inject(Router)
  cartservice = inject(CartSignalService)
  private accountservice = inject(AccountForclientService)
  private stripe = inject(StripeService);
  addressElemet?: StripeAddressElement
  private snackbar = inject(SnackbarService)
  private OrderService = inject(OrdersService) 
  paymentelement?: StripePaymentElement
  saveAddress = false
  completionStatus = signal<{ address: boolean, card: boolean, delivery: boolean }>(
    { address: false, card: false, delivery: false }
  )
  confirmationToken?: ConfirmationToken;
  loading = false;
  orderComplete=false
  async ngOnInit() {
    try {
      this.addressElemet = await this.stripe.createAdressElement();
      this.addressElemet?.mount('#address-element')
      this.addressElemet.on('change', this.handleAddressChange)

      this.paymentelement = await this.stripe.createPaymentElement()
      this.paymentelement?.mount('#payment-element')

      this.paymentelement.on('change', this.handlePaymentChange);
    

    }
    catch (error: any) {
      this.snackbar.error(error.message)
    }
  }
  handlePaymentChange = (event: StripePaymentElementChangeEvent) => {
    this.completionStatus.update(state => ({
      ...state,
      card: event.complete
    }));
  };

  handleDelivery(event: boolean) {
    this.completionStatus.update(state => ({
      ...state,
      delivery: event
    }));
  }
 


  ngOnDestroy(): void {
    this.stripe.disposeElements()
  }
  handleAddressChange=(event: StripeAddressElementChangeEvent) =>{
    this.completionStatus.update(state => {
      state.address = event.complete
      return state
    })
  }

  onSave(event: MatCheckboxChange) {
    this.saveAddress = event.checked 
  }
  async onstepchange(event: StepperSelectionEvent) {
    const cart = this.cartservice.cart();
    console.log(cart)
    if (event.selectedIndex === 1) {

     if (this.saveAddress) {

       const address = await this.getAddressfromstripe() as AddressDto;

       address && firstValueFrom(this.accountservice.updateAddress(address))
     
      }
    }
    if (event.selectedIndex === 2) {
      await firstValueFrom(this.stripe.createOrUpdatePayment())
    }
    if (event.selectedIndex === 3) {
      await this.getConfirmationToken()
    }
  }
  async confirmPayment(stepper: MatStepper) {
    this.loading=true
    try {
      if (this.confirmationToken) {

        const result = await this.stripe.confirmpayment(this.confirmationToken)

        if (result.paymentIntent?.status === 'succeeded') {
          const createOrderDTO = await this.createOrder();
        
          const ordercreated = await firstValueFrom(this.OrderService.createOrder(createOrderDTO))
          if (ordercreated) {
            this.orderComplete=true
            this.cartservice.deleteCart();
            this.cartservice.selectedDelivery.set(null);
            this.router.navigateByUrl("checkout/success")
          }
          else {
            throw new Error("order creation failed")
          }
        }
        else if (result.error) {
          throw new Error(result.error.message)

        }
        else {
          throw new Error("Something went wrong")

        }
     
      }
    }
    catch (error: any) {
      this.snackbar.error(error.message || 'Something went wrong')
      stepper.previous()
    }
    finally {
      this.loading=false
    }
  }

  private async getAddressfromstripe(): Promise<AddressDto | ShippingAddress | null> {
    const result = await this.addressElemet?.getValue()
    const address = result?.value.address
    if (address) {
      return {
        name: result.value.name,
        line1: address.line1,
        line2: address.line2,
        city: address.city,
        country: address.country,
        postalCode: address.postal_code,
        state: address.state

        
      }
    }
    else return null

  }
  async getConfirmationToken() {
    try {
      if (Object.values(this.completionStatus()).every(status => status === true)) {
        const result = await this.stripe.createConfirmationToken()
        this.confirmationToken = result.confirmationToken
      }
    }
    catch (error: any ) {
      this.snackbar.error(error.message)
    }

  }

  private async createOrder(): Promise<CreateOrderDTO> {
    const cart = this.cartservice.cart();
    const shippingAdress = await this.getAddressfromstripe() as ShippingAddress
    const card = this.confirmationToken?.payment_method_preview.card
    if (!cart?.id || !cart?.deliverymethodId || !card) {
      throw new Error("Problem creating order")
    }
  return {
      cartId: cart.id,
      deliveryMethodId: cart.deliverymethodId,
      address: shippingAdress,
      paymentSummary: {
        last4: + card.last4,
        brand: card.brand,
        expMounth: card.exp_month,
        year: card.exp_year
       
      }
    }
  }

}
