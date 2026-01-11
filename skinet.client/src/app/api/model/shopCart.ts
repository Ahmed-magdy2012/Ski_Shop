
import { CartItem } from './cartItem';
import { nanoid } from "nanoid"

export class ShopCart implements CartType { 
  id = nanoid();
  items: CartItem[] = [];
}

export interface CartType  {
  id: string;
  items: CartItem[]
}
