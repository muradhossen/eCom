import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { ProductService } from '../../apps/services/product.service';
import { Product } from '../../apps/models/product';
import { Pagination } from '../../apps/models/pagination';

@Component({
  selector: 'app-product-container',
  templateUrl: './product-container.component.html',
  styleUrls: ['./product-container.component.scss']
})
export class ProductContainerComponent implements OnInit {

  pageNumber = 1;
  pageSize = 20;
  products: Product[] = [];

  pagination: Pagination = {
    currentPage: 1,
    itemsPerPage: this.pageSize,
    totalCount: 0,
    totalItems: 0,
    totalPages : 0
  };
  
  productOptions: OwlOptions = {
    loop: false,
    autoplay: true,
    mouseDrag: true,
    touchDrag: false,
    pullDrag: false,
    dots: false,
    navSpeed: 500, 
    slideBy : 4,
    navText: ['<i class="fa-solid fa-chevron-left"></i>', '<i class="fa-solid fa-chevron-right"></i>'], 
    responsive: {
      300: {
        items: 1
      },
      400: {
        items: 2
      },
      740: {
        items: 3
      },
      940: {
        items: 5
      }
    },
    nav: true, 
  } 

  constructor( private productService : ProductService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit() {
    this.loadProducts();
  }

  loadProducts() {
    this.productService.getProducts(this.pageSize, this.pageNumber,true).subscribe(res => { 
      this.products = res.result.data;
      this.pagination = res.pagination;

      this.cdr.detectChanges();
    });
  }

}
