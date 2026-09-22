
import { CartItem } from './cartItem';
import { nanoid } from "nanoid"

export class ShopCart implements CartType {
  id = nanoid();
  items: CartItem[] = [];
  deliverymethodId?: number;
  PaymentId?: string;
  clientSecret?: string;
}

export interface CartType {
  id: string;
  items: CartItem[]
  deliverymethodId?: number;
  PaymentId?: string;
  clientSecret?: string;
}
