import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Category } from '../models/category';
import { getPaginatedResult, getPaginationHeader } from './paginationHelper';
import { Result } from '../../auth/models/result';
import { categoryToFormData } from 'src/app/_helpers/mapper';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  baseUrl = environment.baseUrl;
  endpoint = `${environment.baseUrl}Categories/`

  constructor(private http: HttpClient) { }

  getCategories(pageSize: number, pageNumber: number) {

    let params = getPaginationHeader(pageNumber, pageSize);

    return getPaginatedResult<Category[]>(this.endpoint, params, this.http);
  }

  createCategory(category: Category) {

    return this.http.post<Result<Category>>(this.baseUrl + "Categories", categoryToFormData(category)).pipe(map(res => {

      return res.data;

    }));
  }

  updateCategory(id: number, category: Category) {

    return this.http.put<Result<Category>>(this.baseUrl + "Categories/" + id, categoryToFormData(category)).pipe(map(res => {

      return res.data;

    }));
  }

  getCategory(id: number) {

    return this.http.get<Result<Category>>(this.baseUrl + "Categories/" + id).pipe(map(res => {

      return res.data;

    }));
  }

  deleteCategory(id: number) {
    return this.http.delete<Result<any>>(this.endpoint + id);
  }

}