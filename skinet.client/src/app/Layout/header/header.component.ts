import { Component, inject, OnInit } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { MatProgressBar } from '@angular/material/progress-bar';
import { MatBadge } from '@angular/material/badge'
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { BusyService } from '../../api/api/busy.service';
import { CartSignalService } from '../../api/api/cart-signal.service';
import { AccountForclientService } from '../../api/api/account-forclient.service';
import { MatMenu, MatMenuItem, MatMenuTrigger } from '@angular/material/menu';
import { MatDivider } from '@angular/material/divider';

@Component({
  imports: [
    MatIcon,
    MatButton,
    MatBadge, RouterLink, RouterLinkActive,
    MatProgressBar,
    MatMenuTrigger,
    MatDivider,
    MatMenu,
    MatMenuItem
  ],
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent  {

  busyService = inject(BusyService)
  cartservice = inject(CartSignalService)
  accountService = inject(AccountForclientService)
  private router = inject(Router)
  logout() {
 
    this.accountService.logout().subscribe({

      next: () => {
        this.accountService.currentuser.set(null)
        this.router.navigateByUrl("/")

      }

    })
  }

    
  
}
