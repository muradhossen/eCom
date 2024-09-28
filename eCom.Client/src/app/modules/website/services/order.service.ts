import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Cart } from '../../apps/models/cart';
import { Result } from '../../auth/models/result';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  baseUrl = environment.baseUrl;
  endpoint = `${environment.baseUrl}orders/`;

  constructor(private http: HttpClient) {}

  createByCart(cart: Cart) {
    return this.http.post<Result<any>>(this.endpoint, cart).pipe(
      map((orderResult) => {
        return orderResult;
      })
    );
  }
}
