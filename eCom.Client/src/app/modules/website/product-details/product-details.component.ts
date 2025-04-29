import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../apps/services/product.service';
import { PricingItem, Product } from '../../apps/models/product';
import { getProductPrice, hasDiscount } from 'src/app/_helpers/ProductPriceHelper';
import { AddToCartService } from '../../apps/services/add-to-cart.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  product: Product;

  constructor(private activatedRoute: ActivatedRoute,
    private productService: ProductService,
    private cdr : ChangeDetectorRef,
    private addToCartService : AddToCartService
  ) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe(params => {
      const productAndId = params["name"];
      const productId = this.extractProductId(productAndId);
      this.getProduct(productId);
    });
  }
  extractProductId(productAndId: string) {


    const parts = productAndId.split('---');

    if (parts.length === 2) {      
     return parseInt(parts[1].trim());
    } else {
      // Handle invalid text format
      console.error('Invalid text format');
    }
    return 0;
  }

  getProduct(id: number) {
    this.productService.getProduct(id).subscribe(product => {
      this.product = product;
      this.cdr.detectChanges();
    })
  }

  getProductPrice(pricingItem: PricingItem): number {
    return getProductPrice(pricingItem);
  }

  hasDiscount(pricingItem: PricingItem) {
    return hasDiscount(pricingItem)
  }

  onAddToCart(product: Product) {
    this.addToCartService.sendProductToCart(product);
  }
}
