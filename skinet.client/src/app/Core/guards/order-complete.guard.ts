import { CanActivateFn, Router } from '@angular/router';
import { OrdersService } from '../../api/api/orders.service';
import { ChekoutComponent } from '../../features/chekout/chekout.component';
import { inject } from '@angular/core';

export const orderCompleteGuard: CanActivateFn = (route, state) => {


  const router = inject(Router)
  const comp = (ChekoutComponent)
  if (comp) {
    return true
  } else {
    router.navigateByUrl('/shop')
    return false
  }
  return true; 
};
