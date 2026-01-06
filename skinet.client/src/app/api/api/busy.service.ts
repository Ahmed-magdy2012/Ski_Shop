import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BusyService {

  loading = false;
  busyrequest=0
  busy() {
    this.busyrequest++
    this.loading=true
  }

  idle() {
    this.busyrequest--
    if (this.busyrequest <= 0) {
      this.busyrequest = 0
      this.loading = false
    }
  }
}
