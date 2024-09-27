import { Component, OnInit } from '@angular/core';
import { AddToCartService } from '../../apps/services/add-to-cart.service';
import { Cart } from '../../apps/models/cart';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {
  cart: Cart = new Cart();

  constructor(private addToCartService : AddToCartService) { 
    this.cart = this.addToCartService.getCart();
   }

  ngOnInit() {
  }

}
