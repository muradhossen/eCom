import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Category } from '../../apps/models/category';
import { Pagination } from '../../apps/models/pagination';
import { CategoryService } from '../../apps/services/category.service';
import { OwlOptions } from 'ngx-owl-carousel-o';

@Component({
  selector: 'app-category-container',
  templateUrl: './category-container.component.html',
  styleUrls: ['./category-container.component.scss']
})
export class CategoryContainerComponent implements OnInit {

  pageNumber = 1;
  pageSize = 30;
  categories: Category[] = [];

  pagination: Pagination = {
    currentPage: 1,
    itemsPerPage: this.pageSize,
    totalCount: 0,
    totalItems: 0,
    totalPages : 0,
  };

  categoryOptions: OwlOptions = {
    loop: false,
    // autoplay: true,
    mouseDrag: true,
    touchDrag: false,
    pullDrag: false,
    dots: false,
    navSpeed: 500, 
    slideBy : 10,
    navText: ['<i class="fa-solid fa-chevron-left"></i>', '<i class="fa-solid fa-chevron-right"></i>'], 
    responsive: {
      300: {
        items: 3
      },
      400: {
        items: 5
      },
      740: {
        items: 7
      },
      940: {
        items: 10
      }
    },
    nav: true, 
  } 

  constructor(private categoryService : CategoryService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit() {
    this.loadCategories();
  }
  loadCategories() {
    this.categoryService.getCategories(this.pageSize, this.pageNumber).subscribe(res => {
      this.categories = res.result;
      this.pagination = res.pagination;

      this.cdr.detectChanges();
    });
  } 

}
