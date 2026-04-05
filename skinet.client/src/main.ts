
import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { HttpClientModule, provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideRouter } from '@angular/router';
import { AppRoutingModule, routes } from './app/app-routing.module';
import { APP_INITIALIZER, importProvidersFrom } from '@angular/core';
import { errorInterceptorInterceptor } from './app/Core/error-interceptor.interceptor';
import { loadingInterceptor } from './app/Core/loading.interceptor';
import { InitService } from './app/api/api/init.service';
import { lastValueFrom } from 'rxjs';
import { authInterceptor } from './app/Core/auth.interceptor';


function intializeapp(initservice: InitService) {
  return () => lastValueFrom(initservice.init()).finally(() => {
    const splash = document.getElementById('splash');
    if (splash) {
      splash.remove()
    }
  })
}

bootstrapApplication(AppComponent, {
  providers: [
   
    importProvidersFrom(HttpClientModule) ,
    provideRouter(routes),
    provideHttpClient(withInterceptors([
      errorInterceptorInterceptor,
      loadingInterceptor,
      authInterceptor
    ])),
    {

      provide: APP_INITIALIZER,
      useFactory: intializeapp,
      multi: true,
      deps: [InitService]
    }
    

  ]
});
