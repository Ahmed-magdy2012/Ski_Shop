import { Component, inject,  } from '@angular/core';
import { Product } from '../../api/model/product';
import { ProductsService } from '../../api/api/products.service';
import { MatCard } from '@angular/material/card'
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog'
import {  FormsModule } from '@angular/forms'
import { ProductItemComponent } from './product-item/product-item.component';
import { FilterDialogComponent } from './filter-dialog/filter-dialog.component';
import { MatListModule, MatSelectionListChange } from '@angular/material/list';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { ProductPagination } from '../../api';
import { shopParams } from '../../api/model/shopParams';


@Component({
  selector: 'app-shop',
  imports: [
    MatCard, ProductItemComponent, MatButton, MatIcon, MatListModule, MatMenu, MatMenuTrigger, MatPaginator, FormsModule
  ],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.css',
  standalone: true,
})
export class ShopComponent {
  private dialogService = inject(MatDialog)
  products?: ProductPagination;
  types: string[] = [];
  Brands?: string[];

  sortOptions = [
    { name: 'Alphabetical', value: 'name' }, { name: 'Price: Low-High', value: 'PriceAsc' }
    , { name: 'Price: High-Low', value: 'PriceDesc' }
  ]

   shopparams :shopParams = new shopParams()
  pageSizeOptions=[5,10,15,15,20]

  constructor(private product: ProductsService) {
    }


 

  ngOnInit(): void {

    
    this.getProducts();

  }
  
  getProducts() {
    
    this.product.getProducts(this.shopparams.pageIndex,
      this.shopparams.pageSize, this.shopparams.brand, this.shopparams.types, this.shopparams.sort, this.shopparams.search

         


    ).subscribe({
      next: (response) => {
        this.products = response;
        console.log(response)
      },
      error: (err) => console.error(err)

    });
  }

  onSearchchange() {
    this.shopparams.pageIndex = 1

      this.getProducts()

 
  }

  handelpageEvent(event: PageEvent) {
    this.shopparams.pageIndex = event.pageIndex + 1
    this.shopparams.pageSize = event.pageSize
    this.getProducts()

  }

  onSortChange(event: MatSelectionListChange) {
    const selection = event.options[0]
    if (selection) {
      this.shopparams.pageIndex = 1
      this.shopparams.sort = selection.value
      this.getProducts() 
    }
  }


  openDialog() {
    const dialog = this.dialogService.open(FilterDialogComponent, {
      minWidth: '500px',
      data: {
        selectedBrands: this.shopparams.brand,
        selectedTypes: this.shopparams.types
      }
    })

    dialog.afterClosed().subscribe({
      next: result => {
 
        console.log(result)
        this.shopparams.pageIndex = 1
        this.shopparams.brand = result.selectedBrands
        this.shopparams.types = result.selectedTypes
        this.getProducts();
      
}
 

    })
  }
  

  }
