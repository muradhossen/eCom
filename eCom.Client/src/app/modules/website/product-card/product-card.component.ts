import { Component, Input, OnInit } from '@angular/core';
import { PricingItem, Product } from '../../apps/models/product';
import { DiscountType } from '../../apps/enums/discountType';
import { ConfirmService } from 'src/app/_bsCommon/confirm.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit {

  @Input() product: Product;
   
  productDetailsRoute : string
  modalRef?: BsModalRef;
  config = {
    animated: true,
    class: 'modal-dialog modal-dialog-centered modal-lg' 
  };
  constructor(private modalService: BsModalService) {}
 

  ngOnInit() {

    this.productDetailsRoute = '/products/'+ this.product.name;
  }

  getProductPrice(pricingItem: PricingItem): number {
    if (this.hasDiscount(pricingItem)) {
      return pricingItem.price - this.getDiscountAmount(pricingItem);
    } else {
      return pricingItem.price;
    }
  }

  public hasDiscount(pricingItem: PricingItem) {
    return pricingItem.discountAmount || pricingItem.discountPercentage;
  }

  public getDiscountAmount(pricingItem: PricingItem) {

    if (pricingItem.discountType == DiscountType.flat) {
      return pricingItem.discountAmount;
    }
    if (pricingItem.discountType == DiscountType.percentage) {
      return (pricingItem.price * pricingItem.discountPercentage) / 100;
    }
    return 0;
  }

  showProductQuickView(product: Product,template : any) {
    

    this.modalRef = this.modalService.show(template, this.config);
  }

}
