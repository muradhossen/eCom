import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartComponent } from './cart.component';
import { AddToCartProductCardComponent } from '../../website/add-to-cart-product-card/add-to-cart-product-card.component';
import { AlertModule } from 'ngx-bootstrap/alert';

@NgModule({
  imports: [
    CommonModule,
    AlertModule
  ],
  declarations: [
    CartComponent,
    AddToCartProductCardComponent
  ],
  exports : [
    CartComponent
  ]
})
export class CartModule { }
