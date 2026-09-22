
import { NgModule, ApplicationConfig } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './Layout/header/header.component';
import { AddressPipe } from './Shared/pipes/address.pipe';
import { PaymentPipe } from './Shared/pipes/payment.pipe';


@NgModule({
  declarations: [

  
  
    AddressPipe,
             PaymentPipe
  ],
  imports: [
    BrowserModule, 
    AppRoutingModule,
    HeaderComponent
  ],
  providers: [

  ],
  bootstrap: []
})
export class AppModule { }
