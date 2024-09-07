import { Component, Input, OnInit } from '@angular/core';
import { Product } from '../../apps/models/product';

@Component({
  selector: 'app-add-to-cart-product-card',
  templateUrl: './add-to-cart-product-card.component.html',
  styleUrls: ['./add-to-cart-product-card.component.scss']
})
export class AddToCartProductCardComponent implements OnInit {

  @Input() product : Product;
  
  constructor() { }

  ngOnInit() {
  }

}
