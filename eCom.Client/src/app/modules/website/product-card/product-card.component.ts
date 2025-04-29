import { Component, Input, OnInit } from '@angular/core';
import { PricingItem, Product } from '../../apps/models/product';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import {
  getProductPrice,
  hasDiscount,
} from 'src/app/_helpers/ProductPriceHelper';
import { AddToCartService } from '../../apps/services/add-to-cart.service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss'],
})
export class ProductCardComponent implements OnInit {
  @Input() product: Product;

  productDetailsRoute: string;
  modalRef?: BsModalRef;
  config = {
    animated: true,
    class: 'modal-dialog modal-dialog-centered modal-lg',
  };
  constructor(
    private modalService: BsModalService,
    private addToCartService: AddToCartService
  ) {}

  ngOnInit() {
    this.productDetailsRoute = `/products/${this.product.name}---${this.product.id}`;
  }

  getProductPrice(pricingItem: PricingItem): number {
    return getProductPrice(pricingItem);
  }

  hasDiscount(pricingItem: PricingItem) {
    return hasDiscount(pricingItem);
  }

  showProductQuickView(product: Product, template: any) {
    this.modalRef = this.modalService.show(template, this.config);
  }

  onAddToCart(product: Product) {
    this.addToCartService.sendProductToCart(product);
  }
}
