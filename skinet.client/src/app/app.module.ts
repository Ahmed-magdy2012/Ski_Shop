
import { NgModule, ApplicationConfig } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HeaderComponent } from './Layout/header/header.component';


@NgModule({
  declarations: [

  
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
