import { Component, input } from '@angular/core';
import { inject } from '@angular/core';
import { CartSignalService } from '../../api/api/cart-signal.service';
import { CartItemComponent } from './cart-item/cart-item.component';
import { OrdarSummaryComponent } from '../../Shared/components/ordar-summary/ordar-summary.component';
import { EmptyStateComponent } from '../../Shared/components/empty-state/empty-state.component';

@Component({
  selector: 'app-cart',
  imports: [CartItemComponent, OrdarSummaryComponent, EmptyStateComponent],
  standalone: true,
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent {
  cartservice = inject(CartSignalService)
  
}
