import { HttpClient, HttpParams } from "@angular/common/http";
import { PaginatedResult } from "../models/pagination";
import { map } from "rxjs/operators";

export function getPaginatedResult<T>(url : string, params: HttpParams,http : HttpClient) {

    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();

    return http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        paginatedResult.result = response.body as T;
        if (response.headers.get('Pagination') != null) {
 
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination') as string);
        }
        return paginatedResult;
      })
    );
  }

 export function getPaginationHeader(pageNumber: number, pageSize: number) {
    let params = new HttpParams();

    params = params.append("PageNumber", pageNumber.toString());
    params = params.append("PageSize", pageSize.toString());
    return params;
  }