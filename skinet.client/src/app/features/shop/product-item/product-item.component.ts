import { Component, Input } from '@angular/core';
import { Product } from '../../../api';
import { MatCard, MatCardActions, MatCardContent, MatCardModule } from '@angular/material/card';
import { CurrencyPipe } from '@angular/common';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { RouterLink } from '@angular/router';

@Component({
  imports: [
    MatCard,
    MatCardContent,
    CurrencyPipe,
    MatCardActions,
    MatIcon,
    MatButton, RouterLink
  ],
  selector: 'app-product-item',
  standalone:true,
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.css'
})
export class ProductItemComponent {
  @Input() product?: Product;
}
