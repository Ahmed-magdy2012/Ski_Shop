
import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { HttpClientModule, provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { AppRoutingModule, routes } from './app/app-routing.module';
import { importProvidersFrom } from '@angular/core';
import { errorInterceptorInterceptor } from './app/Core/error-interceptor.interceptor';
import { loadingInterceptor } from './app/Core/loading.interceptor';


bootstrapApplication(AppComponent, {
  providers: [
   
    importProvidersFrom(HttpClientModule) ,
    provideRouter(routes),
    provideHttpClient(withInterceptors([errorInterceptorInterceptor, loadingInterceptor])),

    

  ]
});
