import { Component, input } from '@angular/core';
import { CartItem } from '../../../api';
import { RouterLink } from '@angular/router';
import { MatButton, MatIconButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { CurrencyPipe } from '@angular/common';
import { inject } from '@angular/core';
import { CartSignalService } from '../../../api/api/cart-signal.service';

@Component({
  selector: 'app-cart-item',
  standalone: true,
  imports: [
    RouterLink,
    MatButton,
    MatIcon,
    MatIconButton, CurrencyPipe
  ],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.css'
})
export class CartItemComponent {
  item = input.required<CartItem>()
  cartservice = inject(CartSignalService)


}
