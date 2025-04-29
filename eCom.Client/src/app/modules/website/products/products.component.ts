import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CategoryService } from '../../apps/services/category.service';
import { Category } from '../../apps/models/category';
import { Pagination } from '../../apps/models/pagination';
import { Product } from '../../apps/models/product';
import { ProductService } from '../../apps/services/product.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {

  pageNumber = 1;
  pageSize = 30;
  categories: Category[] = [];

  pagination: Pagination = {
    currentPage: 1,
    itemsPerPage: this.pageSize,
    totalCount: 0,
    totalItems: 0,
    totalPages: 0,
  };

  productPageNumber = 1;
  productPageSize = 6;
  products: Product[] = [];

  productPagination: Pagination = {
    currentPage: 1,
    itemsPerPage: this.productPageSize,
    totalCount: 0,
    totalItems: 0,
    totalPages: 0
  }; 

  selectedCategoryId: number[] = []

  constructor(private categoryService: CategoryService,
    private cdr: ChangeDetectorRef,
    private productService: ProductService,
    private activatedRoute: ActivatedRoute,
  ) { }

  ngOnInit() {

    this.activatedRoute.queryParams.subscribe(params => {

      const categoryId = params["id"];
      this.processCategorySelection(parseInt(categoryId));
    });

    this.loadCategories();
    this.loadProducts();


  }

  hasCategoryId(categoryId: number) {
    return this.selectedCategoryId.some(c => c == categoryId);
  }
  processCategorySelection(categoryId: number) {

    if (!categoryId) {
      return;
    }

    if (this.hasCategoryId(categoryId)) {
      this.selectedCategoryId = [...this.selectedCategoryId.filter(c => c !== categoryId)];
    }
    else {
      this.selectedCategoryId = [...this.selectedCategoryId, categoryId];
    }
 
  }

  loadCategories() {
    this.categoryService.getCategories(this.pageSize, this.pageNumber).subscribe(res => {
      this.categories = res.result;
      this.pagination = res.pagination;

      this.cdr.detectChanges();
    });
  }
  loadProducts() {
    this.productService.getProducts(this.productPageSize, this.productPageNumber, true).subscribe(res => {
      
 debugger
      this.products =this.products.concat(res.result.data);
      this.productPagination = res.pagination;
      

      this.cdr.detectChanges();
    });
  }
  loadMoreProducts() {

    this.productPageNumber+=1;
    this.loadProducts();
  }

}
