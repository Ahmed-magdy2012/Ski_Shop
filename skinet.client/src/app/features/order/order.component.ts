import { Component, OnInit,inject } from '@angular/core';
import { OrdersService } from '../../api/api/orders.service'
import { OrderDTO } from '../../api/model/orderDTO';
import { MatButton  } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatCard } from '@angular/material/card';
import { DatePipe, CurrencyPipe } from '@angular/common';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-order',
  standalone: true,
  imports: [
    RouterLink, MatButton, MatIcon, MatCard, DatePipe, CurrencyPipe, CommonModule],
  templateUrl: './order.component.html',
  styleUrl: './order.component.css'
})
export class OrderComponent implements OnInit {
  service = inject(OrdersService)
  orders: OrderDTO[] = [];

  ngOnInit(): void {
    this.service.getOrdersForUser().subscribe({
      next: (orders) => {
    
        this.orders = orders
        console.log(this.orders)
      }
    })
}
  getStatusClass(status: string | null): string {
    switch (status) {
      case 'Pending': return 'status-pending';
      case 'PaymentReceived': return 'status-payment';
      case 'Shipped': return 'status-shipped';
      case 'Delivered': return 'status-delivered';
      case 'Cancelled': return 'status-cancelled';
      default: return 'status-default';
    }
  }

  getStatusIcon(status: string | null): string {
    switch (status) {
      case 'Pending': return 'schedule';
      case 'PaymentReceived': return 'payment';
      case 'Shipped': return 'local_shipping';
      case 'Delivered': return 'check_circle';
      case 'Cancelled': return 'cancel';
      default: return 'help_outline';
    }
  }

  getStatusLabel(status: string | null): string {
    if (status === 'PaymentReceived') return 'Payment Received';
    return status ?? 'Unknown';
  }
}
