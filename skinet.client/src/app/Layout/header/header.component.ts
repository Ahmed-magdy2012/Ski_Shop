import { Component, inject, OnInit } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { MatProgressBar } from '@angular/material/progress-bar';
import { MatBadge } from '@angular/material/badge'
import { RouterLink, RouterLinkActive } from '@angular/router';
import { BusyService } from '../../api/api/busy.service';
import { CartSignalService } from '../../api/api/cart-signal.service';

@Component({
  imports: [
    MatIcon,
    MatButton,
    MatBadge, RouterLink, RouterLinkActive,
    MatProgressBar
  ],
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent  {
  busyService = inject(BusyService)
  cartservice = inject(CartSignalService)

}
