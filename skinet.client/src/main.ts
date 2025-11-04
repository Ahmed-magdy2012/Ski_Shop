import { platformBrowser } from '@angular/platform-browser';
import { AppModule } from './app/app.module';
import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import{ HttpClientModule } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { AppRoutingModule, routes } from './app/app-routing.module';
import { importProvidersFrom } from '@angular/core';


bootstrapApplication(AppComponent, {
  providers: [
   
    importProvidersFrom(HttpClientModule) ,
     provideRouter(routes)  

    

  ]
}).catch(err => console.error(err));;
