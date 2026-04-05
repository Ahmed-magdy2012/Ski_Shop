import { CanActivateFn, Router } from '@angular/router';
import { AccountForclientService } from '../../api/api/account-forclient.service';
import { inject } from '@angular/core';
import { CartSignalService } from '../../api/api/cart-signal.service';
import { SnackbarService } from '../../api/api/snackbar.service';

export const emptyCartGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountForclientService)
  const router = inject(Router)
  const CartSignal = inject(CartSignalService)
  const snackbars = inject(SnackbarService)
  if (!CartSignal.cart() || CartSignal.cart()?.items.length === 0) {
    snackbars.error('your cart is empty')
    router.navigateByUrl('/cart')
    return false
  }
  return true;
};
