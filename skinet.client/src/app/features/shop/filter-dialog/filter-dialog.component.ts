import { Component, OnInit, inject, ViewEncapsulation } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { MatDivider, } from '@angular/material/divider';
import { MatListModule, } from '@angular/material/list';
import { ProductsService } from '../../../api';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms'; 

@Component({
  selector: 'app-filter-dialog',
  standalone: true,
  imports: [
    MatDivider
    , MatButton,
    MatListModule, FormsModule
  ],
  templateUrl: './filter-dialog.component.html',
  styleUrls: ['./filter-dialog.component.css'],

})
export class FilterDialogComponent implements OnInit {
  types?: string[] ;
  Brands?: string[];
  private ref = inject(MatDialogRef<FilterDialogComponent>)
  data = inject(MAT_DIALOG_DATA);
  selectedBrands: string[] = this.data.selectedBrands
  selectedTypes: string[] = this.data.selectedTypes

  constructor(private product: ProductsService) { }
    ngOnInit(): void {
      this.product.getBrands().subscribe({
        next: (response) => {
          this.Brands = response

        }
      })
      this.product.getTypes().subscribe({
        next: (response) => {
          this.types = response
      
        }

      })
  }

  appltfilters() {
    this.ref.close({
      selectedBrands: this.selectedBrands,
      selectedTypes: this.selectedTypes

    })

  }

}
