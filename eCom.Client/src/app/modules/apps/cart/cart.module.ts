import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartComponent } from './cart.component';
import { AddToCartProductCardComponent } from '../../website/add-to-cart-product-card/add-to-cart-product-card.component';
import { AlertModule } from 'ngx-bootstrap/alert';
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    CommonModule,
    AlertModule,
    RouterModule
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
