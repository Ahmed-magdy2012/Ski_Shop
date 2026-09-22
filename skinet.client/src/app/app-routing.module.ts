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
import { LoginComponent } from './features/account/login/login.component';
import { RegisterComponent } from './features/account/register/register.component';
import { authGuard } from './Core/guards/auth.guard';
import { emptyCartGuard } from './Core/guards/empty-crt.guard';
import { CheckoutSuccessComponent } from './features/chekout/checkout-success/checkout-success.component';
import { OrderComponent } from './features/order/order.component';
import { OrderDetailedComponent } from './features/order/order-detailed/order-detailed.component';
import { orderCompleteGuard } from './Core/guards/order-complete.guard';

export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: "full" },
  { path: 'shop', component: ShopComponent, },
  { path: 'shop/:id', component: ProductDetailsComponent },
  { path: 'cart', component: CartComponent },
  { path: 'checkout/success', component: CheckoutSuccessComponent, canActivate: [authGuard, orderCompleteGuard] },
  { path: 'orders', component: OrderComponent, canActivate: [authGuard] },
  { path: 'orders/:id', component: OrderDetailedComponent, canActivate: [authGuard ] },
  { path: 'checkout', component: ChekoutComponent, canActivate: [authGuard, emptyCartGuard] },
  { path: 'register', component: RegisterComponent, },
  { path: 'login', component: LoginComponent, },
  { path: 'test-error', component: TestErrorComponent },
  { path: 'NotFound', component: NotFoundComponent },
  { path: 'server', component: ServerErrorComponent },
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
