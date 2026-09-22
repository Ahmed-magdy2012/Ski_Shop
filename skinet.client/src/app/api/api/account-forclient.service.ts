import { Injectable, signal,inject } from '@angular/core';
import { User } from '../model/registerDto';
import { AccountService } from './account.service';
import { shopParams } from '../model/shopParams';
import { SKINETServerService } from './sKINETServer.service';
import { AddressDto } from '../model/addressDto';
import { Router } from '@angular/router';
import { map, switchMap, tap } from 'rxjs';
import { SignalRService } from './signal-r.service';

@Injectable({
  providedIn: 'root'
})
export class AccountForclientService {

  private signalservice = inject(SignalRService)
  customizedApi = inject(AccountService);
  LoginServiceFromIDENTITY = inject(SKINETServerService)
  currentuser = signal<User | null>(null)
  params = new shopParams()
  private router = inject(Router)
   

  login(values: any, returnUrl: string) {

  this.LoginServiceFromIDENTITY.hTTPPOSTLogin(true, true, values).pipe(
    switchMap(() => this.getUserInfo())).subscribe({
      next: () => {
        this.signalservice.createHub()
        this.router.navigateByUrl(returnUrl)
      }

      })
   

  }
  register(values: User) {
    return this.customizedApi.register(values)
  }



  getUserInfo() {
    return this.customizedApi.getUserInfo().pipe(
      map(user => {
        this.currentuser.set(user)
      })
    ) 
  }

  logout() {

    return this.customizedApi.logout().pipe(
      tap(() => this.signalservice.stopConnection()))

  }

  updateAddress(addressdto: AddressDto) {

    return this.customizedApi.createOrUpdateAddres(addressdto).pipe(
      tap(
        () => {
          this.currentuser.update(user => {
            if (user) user.address = addressdto
            return user
          })

        }
      )
    )

  }

}
