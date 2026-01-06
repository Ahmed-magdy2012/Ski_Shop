import { Component, inject, OnInit } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { ErrorService } from '../../api';
import { CreateProductDto } from '../../api/model/createProductDto'
@Component({
  standalone: true,
  selector: 'app-test-error',
  imports: [MatButton],
  templateUrl: './test-error.component.html',
  styleUrl: './test-error.component.css'
})
export class TestErrorComponent   {


  private service = inject(ErrorService)

  validation?: string[];
  
  get404error() {
    this.service.getNotFound().subscribe({
      error: error => console.log(error)
    })
  }

  Get401Error() {
    this.service.getUnauthorized().subscribe({
      error: error => console.log(error)

    })
  }

  Get400Error() {
    this.service.getBadRequest().subscribe({
      error: error => console.log(error)

    })
  }

  Get500Error() {
    this.service.getinternalError().subscribe({

      error: (error) =>
        console.log(error)

    })
  }
  Get400() {
    const ob = {}
    this.service.getValidation(ob).subscribe({

      error: (error) => {
       
        this.validation = error
        console.log(error)
      }

    })
  }
}

