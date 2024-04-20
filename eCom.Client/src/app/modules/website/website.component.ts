import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CategoryService } from '../apps/services/category.service';
import { Category } from '../apps/models/category';
import { Pagination } from '../apps/models/pagination';
import { OwlOptions } from 'ngx-owl-carousel-o';
import { Hierarchy } from '../apps/models/category copy';
import { ProductService } from '../apps/services/product.service';
import { Product } from '../apps/models/product';
 

@Component({
  selector: 'app-website',
  templateUrl: './website.component.html',
  styleUrls: ['./website.component.scss']
})
export class WebsiteComponent implements OnInit {

  hierarchy : Hierarchy[] = [];
 
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

 
  slides: {image: string; text?: string}[] = [
    {text : "Title 1", image: 'https://res.cloudinary.com/do7pdjcnd/image/upload/v1661704779/samples/ecommerce/leather-bag-gray.jpg'},
    {text : "Title 1", image: 'https://res.cloudinary.com/do7pdjcnd/image/upload/v1661704779/samples/ecommerce/leather-bag-gray.jpg'},
    {text : "Title 1", image: 'https://res.cloudinary.com/do7pdjcnd/image/upload/v1661704779/samples/ecommerce/leather-bag-gray.jpg'} 
  ];
 
  customOptions: OwlOptions = {
    loop: true,
    autoplay: true,
    mouseDrag: true,
    touchDrag: false,
    pullDrag: false,
    dots: false,
    navSpeed: 500, 
    navText: ['<i class="fa-solid fa-chevron-left"></i>', '<i class="fa-solid fa-chevron-right"></i>'], 
    responsive: {
      0: {
        items: 1
      } 
    },
    nav: true, 
  } 

  productOptions: OwlOptions = {
    loop: false,
    // autoplay: true,
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
  menuSubcategories: Hierarchy[] = [];

  constructor(private categoryService : CategoryService,
    private cdr: ChangeDetectorRef,
  private productService : ProductService) { }

  ngOnInit() {
   this.loadProducts();
    this.loadCategoryHierarchy();
    
  }

 
  loadCategoryHierarchy() {
    this.categoryService.getCategoryHierarchy(7).subscribe(res => {
      this.hierarchy = res; 

      this.cdr.detectChanges();
    });
  }

  renderData(data: any[]): string {
    let html = '<ul>';
    data.forEach(item => {
      html += `<li>${item.key} - ${item.value} - ${item.code}`;
      if (item.child && item.child.length > 0) {
        html += this.renderData(item.child); // Recursively call renderData
      }
      html += '</li>';
    });
    html += '</ul>';
    return html;
  }

  setMenuSubcategory(item : Hierarchy){
    this.menuSubcategories = item.child;
  }

  loadProducts() {
    this.productService.getProducts(this.pageSize, this.pageNumber,true).subscribe(res => { 
      this.products = res.result.data;
      this.pagination = res.pagination;

      this.cdr.detectChanges();
    });
  }

}
