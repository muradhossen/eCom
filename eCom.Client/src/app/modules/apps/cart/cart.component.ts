import { Component, OnDestroy, OnInit } from '@angular/core';
import { AddToCartService } from '../services/add-to-cart.service';
import { Product } from '../models/product';
import { inject, TemplateRef } from '@angular/core';

import { NgbDatepickerModule, NgbOffcanvas, OffcanvasDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { Cart } from '../models/cart';
import { ProductContainer } from '../models/product-container';


@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  itemsCount : number = 7 ;
  totalPrice : number = 10000 ;

  products : Product[] = [];
  cart : Cart = new Cart();

  constructor(private addToCartService : AddToCartService) { }
 

  ngOnInit() { 

    this.addToCartService.product$.subscribe(product => {
       
        this.products.push(product);

        // this.cart.addProductToCart(product);

    });

    this.addToCartService.cart$.subscribe(cart => {
      this.cart = cart;
    });
  }

  private offcanvasService = inject(NgbOffcanvas);
	closeResult = '';


	openNoBackdrop(content: TemplateRef<any>) {
		this.offcanvasService.open(content, { backdrop: false,scroll: true, position: 'end' });
	}


  removeProductFromCart(productContainer : ProductContainer){
    this.cart.removeProduct(productContainer);
  }
  
}
