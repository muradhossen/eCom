import { Component, OnInit } from '@angular/core';
import { AddToCartService } from '../../apps/services/add-to-cart.service';
import { Cart } from '../../apps/models/cart';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { OrderService } from '../services/order.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss'],
})
export class CheckoutComponent implements OnInit {
  cart: Cart = new Cart();
  checkoutCartForm: FormGroup;

  constructor(
    private addToCartService: AddToCartService,
    private fb: FormBuilder,
    private orderService: OrderService
  ) {
    this.cart = this.addToCartService.getCart();
    this.initForm();
  }

  ngOnInit() {
    this.addToCartService.cart$.subscribe((cart) => {
      this.cart = cart;
    });
  }

  initForm() {
    this.checkoutCartForm = this.fb.group({
      deliveryAddress: [''],
      mobile: [
        '',
        Validators.compose([
          Validators.minLength(11),
          Validators.maxLength(11),
        ]),
      ],
    });
  }

  onSubmit() {
    if (this.checkoutCartForm.valid) {
      debugger
      this.setCheckoutInforToSave();

      this.orderService.createByCart(this.cart).subscribe((res) => {
        if (res.isSuccess) {
          alert('Order created.');
        } else {
          alert('Failed to create order!!');
        }
      });
    } else {
      console.log('Form is invalid');
    }
  }

  confirmOrder() {
    if (this.checkoutCartForm.valid) {
      this.onSubmit();
    } else {
      this.checkoutCartForm.markAllAsTouched();
    }
  }

  setCheckoutInforToSave() {
     
    this.cart.deliveryAddress = this.checkoutCartForm.get('deliveryAddress')?.value;
    this.cart.mobile = this.checkoutCartForm.get('mobile')?.value;

  }
}
