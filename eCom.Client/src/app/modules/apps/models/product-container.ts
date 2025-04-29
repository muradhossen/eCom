import { DiscountType } from '../enums/discountType';
import { Product } from './product';

export class ProductContainer {
  key: Product;
  values: Product[] = [];
  getTotalPrice() {
    return this.getItemPrice() * this.getTotalItems();
  }
  getTotalDiscount() {
    return this.getItemDiscountPrice()  * this.getTotalItems();
  }

  getItemPrice() {
     
    const pricingItem = this.key.section.pricingItems[0];
    return pricingItem.price - this.getItemDiscountPrice();
  }
  getItemDiscountPrice() {
    const pricingItem = this.key.section.pricingItems[0];

    if (!pricingItem.discountType) {
      return 0;
    }
    if (pricingItem.discountType == DiscountType.flat) {
      return pricingItem.discountAmount;
    }

    if (pricingItem.discountType == DiscountType.percentage) {
      return (pricingItem.discountPercentage / 100) * pricingItem.price;
    }
    return 0;
  }

  getTotalItems() {
    return this.values.length;
  }

  add(product : Product){
    this.key = product;
    this.values .push(product);
  }
 
}
