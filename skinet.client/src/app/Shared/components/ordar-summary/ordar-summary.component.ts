import { Component } from '@angular/core';
import { inject } from '@angular/core';
import { MatButton, MatIconButton } from '@angular/material/button';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { RouterLink } from '@angular/router';
import { CartSignalService } from '../../../api/api/cart-signal.service';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-ordar-summary',
  standalone: true,
  imports: [MatButton, RouterLink, MatFormField, MatLabel, MatInput, MatIconButton, CurrencyPipe],
  templateUrl: './ordar-summary.component.html',
  styleUrl: './ordar-summary.component.css'
})
export class OrdarSummaryComponent {
  cartservice = inject(CartSignalService)
}
