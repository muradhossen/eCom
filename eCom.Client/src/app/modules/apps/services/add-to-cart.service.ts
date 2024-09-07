import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class AddToCartService {

private productSource = new Subject<Product>();
product$ = this.productSource.asObservable();

constructor() { }

sendProductToCart(product : Product){
  this.productSource.next(product);
}
}
