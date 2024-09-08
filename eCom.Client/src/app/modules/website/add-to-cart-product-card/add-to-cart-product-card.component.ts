import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Product } from '../../apps/models/product';
import { ProductContainer } from '../../apps/models/product-container';
 

@Component({
  selector: 'app-add-to-cart-product-card',
  templateUrl: './add-to-cart-product-card.component.html',
  styleUrls: ['./add-to-cart-product-card.component.scss']
})
export class AddToCartProductCardComponent implements OnInit {

  @Input() productContainer : ProductContainer;
  @Output() onRemoveProduct : EventEmitter<ProductContainer> = new EventEmitter<ProductContainer>();
  
  constructor() { }

  ngOnInit() {
  }
  removeProductFromCart(productContainer : ProductContainer){
    this.onRemoveProduct.emit(productContainer);
  }
}
