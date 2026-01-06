import { Injectable, inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SnackbarService {

  private snack=inject(MatSnackBar)


  error(message: string) {
    this.snack.open(message, 'close', {
      duration: 5000,
      panelClass:['snack-error']
    })
  }

  Success(message: string) {
    this.snack.open(message, 'close', {
      duration: 5000,
      panelClass: ['snack-success']
    })
  }

}


