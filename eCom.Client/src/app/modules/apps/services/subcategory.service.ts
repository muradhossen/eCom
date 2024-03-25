import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { getPaginatedResult, getPaginationHeader } from './paginationHelper';
import { Result } from '../../auth/models/result';
import { subCategoryToFormData } from 'src/app/_helpers/mapper';
import { map } from 'rxjs';
import { SubCategory } from '../models/subcategory';

@Injectable({
  providedIn: 'root'
})
export class SubcategoryService {


  baseUrl = environment.baseUrl;
  endpoint = `${environment.baseUrl}SubCategories/`

  constructor(private http: HttpClient) { }

  getSubCategories(pageSize: number, pageNumber: number) {

    let params = getPaginationHeader(pageNumber, pageSize);

    return getPaginatedResult<SubCategory[]>(this.endpoint, params, this.http);
  }

  createSubCategory(subCategory: SubCategory) {

    return this.http.post<Result<SubCategory>>(this.endpoint, subCategoryToFormData(subCategory)).pipe(map(res => {

      return res.data;

    }));
  }

  updateSubCategory(id: number, subCategory: SubCategory) {

    return this.http.put<Result<SubCategory>>(this.endpoint + id, subCategoryToFormData(subCategory)).pipe(map(res => {
      
      return res.data;

    }));
  }

  getSubCategory(id: number) {
     
    return this.http.get<Result<SubCategory>>(this.endpoint + id).pipe(map(res => {

      return res.data;

    }));
  }

  deleteSubCategory(id: number) {
    return this.http.delete<Result<any>>(this.endpoint + id);
  }

}
