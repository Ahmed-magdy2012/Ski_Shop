import { Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HubConnection, HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';
import { Order } from '../model/order';
@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  HubUrl = environment.hubUrl
  hubConeect?: HubConnection;
  ordersignal= signal<Order | null>(null);


  createHub() {
    this.hubConeect = new HubConnectionBuilder().withUrl(
      this.HubUrl, {
      withCredentials: true
    }).withAutomaticReconnect().build();

    this.hubConeect.start().catch(error => console.log(error))

    this.hubConeect.on('OrderCompletNotification', (order: Order) => {
      this.ordersignal.set(order)
    })
  }

  stopConnection() {
    if (this.hubConeect?.state === HubConnectionState.Connected) {
      this.hubConeect.stop().catch(error => console.log(error))
    }
  }

}
