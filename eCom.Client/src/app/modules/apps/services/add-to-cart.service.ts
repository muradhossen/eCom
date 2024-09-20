import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Product } from '../models/product';
import { Cart } from '../models/cart';

@Injectable({
  providedIn: 'root',
})
export class AddToCartService {
  private productSource = new Subject<Product>();
  product$ = this.productSource.asObservable();
  private cart: Cart = new Cart();

  constructor() {}

  sendProductToCart(product: Product) {
    this.productSource.next(product);
  }
  setCart(cart: Cart) {
    this.cart = cart;
  }
  getCart(){
    return this.cart;
  }
}
