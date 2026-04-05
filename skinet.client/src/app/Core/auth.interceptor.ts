import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {

  const ClonedRequest = req.clone({
    withCredentials: true
  })


  return next(ClonedRequest);
};
