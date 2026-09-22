import { Component, OnInit } from '@angular/core';
import { inject } from '@angular/core';
import { OrdersService } from '../../../api/api/orders.service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { OrderDTO } from '../../../api/model/orderDTO';
import { MatButton } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';

@Component({
  selector: 'app-order-detailed',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatIconModule, MatButtonModule, MatDividerModule, MatExpansionModule, RouterLink],

  templateUrl: './order-detailed.component.html',
  styleUrl: './order-detailed.component.css'
})
export class OrderDetailedComponent implements OnInit {
  ngOnInit(): void {

    this.LOADoRDER()
  }
  private orderService = inject(OrdersService)
  private route = inject(ActivatedRoute)
  order?: OrderDTO;


  LOADoRDER() {
    const id = this.route.snapshot.paramMap.get('id')
    console.log(id);

    if (!id) return

    this.orderService.getorderById(+id).subscribe({

      next: order => this.order = order
    })
  }
 
 
  getStatusClass(status: string | null): string {
    switch (status) {
      case 'Pending':         return 'status-pending';
      case 'PaymentReceived': return 'status-payment';
      case 'Shipped':         return 'status-shipped';
      case 'Delivered':       return 'status-delivered';
      case 'Cancelled':       return 'status-cancelled';
      default:                return 'status-default';
    }
  }
 
  getStatusIcon(status: string | null): string {
    switch (status) {
      case 'Pending':         return 'schedule';
      case 'PaymentReceived': return 'payment';
      case 'Shipped':         return 'local_shipping';
      case 'Delivered':       return 'check_circle';
      case 'Cancelled':       return 'cancel';
      default:                return 'help_outline';
    }
  }
 
  getStatusLabel(status: string | null): string {
    if (status === 'PaymentReceived') return 'Payment Received';
    return status ?? 'Unknown';
  }
 
  getSubtotal(): number {
    return (this.order?.total ?? 0) - (this.order?.shippingPrice ?? 0);
  }
}
