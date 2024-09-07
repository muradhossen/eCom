import { Product } from './product';
import { ProductContainer } from './product-container';

export class Cart {
  items: ProductContainer[] = [];

  getTotalPrice() {
    let totalPrice = 0;

    this.items.forEach((i) => {
      totalPrice += i.getTotalPrice();
    });

    return totalPrice;
  }

  getTotalDiscount() {
    return 0;
  }

  getTotalItems() {
    return this.items.length;
  }

  addProductToCart(product: Product) {
    if (this.items.some((c) => c.key == product)) {
      this.items.forEach((pc) => {
        if (pc.key == product) {
          pc.values.push(product);
        }
      });
    } else {
      // Add to the key and value.
      const pc = new ProductContainer();
      pc.add(product);
      this.items.push(pc);
    }
  }

  removeProduct(productContainer : ProductContainer){

    this.items = [...this.items.filter(i => i !== productContainer)];

  }
}
