import { Component, Input, OnInit } from '@angular/core';
import { PricingItem, Product } from '../../apps/models/product';
import { DiscountType } from '../../apps/enums/discountType';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit {

  @Input() product : Product;
  constructor() { }

  ngOnInit() {
  }

  getProductPrice(pricingItem: PricingItem): number {
    if (this.hasDiscount(pricingItem)) {
      return pricingItem.price - this.getDiscountAmount(pricingItem);
    } else {
      return pricingItem.price;
    }
  }

  public hasDiscount(pricingItem: PricingItem){
    return pricingItem.discountAmount || pricingItem.discountPercentage;
}

public getDiscountAmount(pricingItem: PricingItem){
   
      if (pricingItem.discountType == DiscountType.flat) {
          return pricingItem.discountAmount;
      }
      if (pricingItem.discountType == DiscountType.percentage) {
          return  (pricingItem.price * pricingItem.discountPercentage) / 100;
      }
      return 0;
  }
}
