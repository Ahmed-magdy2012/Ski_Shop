import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { ProductDetailsComponent } from './features/shop/product-details/product-details.component';
import { ShopComponent } from './features/shop/shop.component';
import { TestErrorComponent } from './features/test-error/test-error.component';
import { NotFoundComponent } from './Shared/components/not-found/not-found.component';
import { ServerErrorComponent } from './Shared/components/server-error/server-error.component';

export const routes: Routes = [

  { path: '', component: HomeComponent, pathMatch: "full" },
  { path: 'shop', component: ShopComponent, },
  { path: 'shop/:id', component: ProductDetailsComponent },
  { path: 'test-error', component: TestErrorComponent },
  { path: 'NotFound', component: NotFoundComponent },
  { path: 'server', component: ServerErrorComponent },
  { path: '**', redirectTo: "NotFound", pathMatch: "full" },


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
