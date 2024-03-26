import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { getPaginatedResult, getPaginationHeader } from './paginationHelper';
import { Result } from '../../auth/models/result';
import { productToFormData, subCategoryToFormData } from 'src/app/_helpers/mapper';
import { map } from 'rxjs';
import { SubCategory } from '../models/subcategory';
import { Product } from '../models/product';


@Injectable({
  providedIn: 'root'
})
export class ProductService {



  baseUrl = environment.baseUrl;
  endpoint = `${environment.baseUrl}Products/`

  constructor(private http: HttpClient) { }

  getProducts(pageSize: number, pageNumber: number) {

    let params = getPaginationHeader(pageNumber, pageSize);

    return getPaginatedResult<Result<Product[]>>(this.endpoint, params, this.http);
  }

  createProduct(product: Product) {

    return this.http.post<Result<Product>>(this.endpoint, productToFormData(product)).pipe(map(res => {

      return res.data;

    }));
  }

  updateProduct(id: number, product: Product) {

    return this.http.put<Result<Product>>(this.endpoint + id, productToFormData(product)).pipe(map(res => {

      return res.data;

    }));
  }

  getProduct(id: number) {

    return this.http.get<Result<Product>>(this.endpoint + id).pipe(map(res => {

      return res.data;

    }));
  }

  deleteProduct(id: number) {
    return this.http.delete<Result<any>>(this.endpoint + id);
  } 
  
}
