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

  private cartSource = new Subject<Cart>();
  cart$ = this.cartSource.asObservable();

  constructor() {}

  sendProductToCart(product: Product) {
    this.cart.addProductToCart(product);
    
    this.cartSource.next(this.cart);
    this.productSource.next(product);
  }
  setCart(cart: Cart) {
    this.cart = cart;
  }
  getCart(){
    return this.cart;
  }
}
