import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AccountForclientService } from '../../api/api/account-forclient.service';
import { AccountService } from '../../api/api/account.service';
import { map, of } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {

  const accountService = inject(AccountForclientService)
  const Service = inject(AccountService)
  const router = inject(Router)
  if (accountService.currentuser()) {

    return of (true)
  }

  else {
    return Service.getAuthState().pipe(
      map(auth => {
   
        if (auth==true) {
          return true
        }
        else {
          router.navigate(['/login'], { queryParams: { returnUrl: state.url } })
          return false
        } 
      })

    )
  
  }

};
