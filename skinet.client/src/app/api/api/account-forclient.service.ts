import { Injectable, signal,inject } from '@angular/core';
import { User } from '../model/registerDto';
import { AccountService } from './account.service';
import { shopParams } from '../model/shopParams';
import { SKINETServerService } from './sKINETServer.service';
import { Address } from '../model/addressDto';
import { Router } from '@angular/router';
import { map, Observable, switchMap, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountForclientService {
  customizedApi = inject(AccountService);
  LoginServiceFromIDENTITY = inject(SKINETServerService)
  currentuser = signal<User | null>(null)
    params = new shopParams()
  private router = inject(Router)
   

  login(values: any, returnUrl: string) {

   this.LoginServiceFromIDENTITY.hTTPPOSTLogin(true, true, values).pipe(
      switchMap(() => this.getUserInfo())).subscribe({
        next: () => this.router.navigateByUrl(returnUrl)

      })
   

  }
  register(values: User) {
    return this.customizedApi.register(values)
  }



  getUserInfo() {
    return this.customizedApi.getUserInfo().pipe(
      map(user => {
        this.currentuser.set(user)
        return user
      })
    ) 
  }

  logout() {

    return this.customizedApi.logout();

  }

  updateAddress(address: Address) {

    return this.customizedApi.createOrUpdateAddres(address)

  }

}
