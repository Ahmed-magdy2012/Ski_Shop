import { Injectable } from '@angular/core';
import { ConfirmationToken, ConfirmationTokenCreateParams, loadStripe, Stripe, StripeAddressElement, StripeAddressElementOptions, StripeElements, StripePaymentElement } from '@stripe/stripe-js';
import { environment } from '../../environments/environment';
import { inject } from '@angular/core';
import { CartSignalService } from '../api/api/cart-signal.service';
import { PaymentsService } from '../api/api/payments.service';
import { map, firstValueFrom } from 'rxjs';
import { AccountService } from '../api/api/account.service';
import { AccountForclientService } from '../api/api/account-forclient.service';

@Injectable({
  providedIn: 'root'
})
export class StripeService {
  private accountservice = inject(AccountForclientService)
  private Paymentservice = inject(PaymentsService)
  private cartservice = inject(CartSignalService)
  private stripePromise?: Promise<Stripe | null>;
  private Elements?: StripeElements;
  private addressElement?: StripeAddressElement;
  private PaymentElement?: StripePaymentElement

  constructor() {
    this.stripePromise = loadStripe(environment.stripePublicKey);
  }

  async getstripeInstance() {
    const stripe = await this.stripePromise;
    return stripe;
  }


  async intialize() {
    if (!this.Elements) {

      const stripe = await this.getstripeInstance();

      if (stripe) {
        const cart = await firstValueFrom(this.createOrUpdatePayment())


        this.Elements = stripe.elements({
          clientSecret: cart.clientSecret,
          appearance: { labels: 'floating' }
        })
      }
      else {
        throw new Error('Stripe has not been loaded')
      }
    }
    return this.Elements;
  }

  async createPaymentElement() {
    if (!this.PaymentElement) {
      const elements = await this.intialize();
      if (elements) {
        this.PaymentElement = elements.create('payment')
      }
      else {
        throw new Error('elements has not been intialized')
      }
    }
    return this.PaymentElement
  }

  async createAdressElement() {

    if (!this.addressElement) {


      const user = this.accountservice.currentuser()
      let defaultValues: StripeAddressElementOptions['defaultValues'] = {}
      if (user) {
        defaultValues.name = user.firstName + ' ' + user.lastName;

      }
      if (user?.address) {
        defaultValues.address = {
          line1: user.address.line1,
          line2: user.address.line2,
          country: user.address.country,
          city: user.address.city,
          state: user.address.state,
          postal_code: user.address.postalCode,

        }
      }
      const elements = await this.intialize();

      if (elements) {
        const options: StripeAddressElementOptions = {
          mode: 'shipping',
          defaultValues
        }
        this.addressElement = elements.create('address', options)

      }
      else {
        throw new Error("elements not loaded")
      }
    }
    return this.addressElement
  }



  createOrUpdatePayment() {
    const cart = this.cartservice.cart();

    if (!cart) throw new Error("problem with cart")

    return this.Paymentservice.createOrUpdatePayment(cart.id).pipe(map(
      cart => {

        this.cartservice.setCart(cart)
        return cart;
      }
    ))
  }
  async createConfirmationToken() {
    const stripe = await this.getstripeInstance();
    const elements = await this.intialize()
    const result = await elements.submit()
    if (stripe) {
      return await stripe.createConfirmationToken({ elements })
    }
    else {
      throw new Error('stripe not available')
    }
  }

  disposeElements() {
    this.addressElement = undefined
    this.Elements = undefined
    this.PaymentElement = undefined
  }
  async confirmpayment(confirmationToken: ConfirmationToken) {
    const stripe = await this.getstripeInstance();
    const elements = await this.intialize()
    const result = await elements.submit()
    const clientsecret = this.cartservice.cart()?.clientSecret
    if (stripe && clientsecret) {
      return await stripe.confirmPayment({
        clientSecret: clientsecret,
        confirmParams: {
          confirmation_token: confirmationToken.id
        },
        redirect: 'if_required'
      })
    }
    else {
      throw new Error('Unable to load stripe')
    }
  }
}
