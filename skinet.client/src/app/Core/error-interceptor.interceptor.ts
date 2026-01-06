import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { NavigationExtras, Router } from '@angular/router';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { SnackbarService } from '../api/api/snackbar.service';

export const errorInterceptorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router)
  const snack = inject(SnackbarService)

  return next(req).pipe(
    catchError((err: HttpErrorResponse) => {
      if (err.status === 400) {
        if (err.error.errors) {
          const modelstate = []
          for (const key in err.error.errors) {
            if (err.error.errors[key]) {
              modelstate.push(err.error.errors[key])
            }
          }
          throw modelstate.flat()
        }   
        else { 
          snack.error(err.error.title)
        }
     
      } if (err.status === 401) {
        snack.error(err.error.title )
      } if (err.status === 404) {
        router.navigateByUrl('/NotFound')
      }
      if (err.status === 500) {
        const navigationExtras: NavigationExtras = { state: { error: err.error } }
        router.navigateByUrl('/server', navigationExtras)
      }
      return throwError(()=>err)
    })

    
  );
};
