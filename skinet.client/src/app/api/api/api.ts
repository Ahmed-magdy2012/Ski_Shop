export * from './cart.service';
import { CartService } from './cart.service';
export * from './error.service';
import { ErrorService } from './error.service';
export * from './products.service';
import { ProductsService } from './products.service';
export const APIS = [CartService, ErrorService, ProductsService];
