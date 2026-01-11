import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { ProductDetailsComponent } from './features/shop/product-details/product-details.component';
import { ShopComponent } from './features/shop/shop.component';
import { TestErrorComponent } from './features/test-error/test-error.component';
import { NotFoundComponent } from './Shared/components/not-found/not-found.component';
import { ServerErrorComponent } from './Shared/components/server-error/server-error.component';
import { CartComponent } from './features/cart/cart.component';
import { ChekoutComponent } from './features/chekout/chekout.component';

export const routes: Routes = [

  { path: '', component: HomeComponent, pathMatch: "full" },
  { path: 'shop', component: ShopComponent, },
  { path: 'shop/:id', component: ProductDetailsComponent },
  { path: 'cart', component: CartComponent },
  { path: 'test-error', component: TestErrorComponent },
  { path: 'NotFound', component: NotFoundComponent },
  { path: 'server', component: ServerErrorComponent },
  { path: 'checkout', component: ChekoutComponent },
  { path: '**', redirectTo: "NotFound", pathMatch: "full" },


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
