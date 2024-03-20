import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CategoryService } from '../../services/category.service';
import { Category } from '../../models/category';
import { Pagination } from '../../models/pagination';
import { Router } from '@angular/router';

@Component({
  selector: 'app-category-table',
  templateUrl: './category-table.component.html',
  styleUrls: ['./category-table.component.scss']
})
export class CategoryTableComponent implements OnInit {
  pageNumber = 1;
  pageSize = 10;
  categories: Category[] = [];

  pagination: Pagination = {
    currentPage: 1,
    itemsPerPage: this.pageSize,
    totalCount: 0,
    totalItems: 0
  };


  constructor(private categoryService: CategoryService,
     private cdr: ChangeDetectorRef,
     private router : Router) { }

  ngOnInit() {

    this.loadCategories();
  }

  pageChanged(event: any) {

    this.pageNumber = event.page;
    this.loadCategories();
  }

  loadCategories() {
    this.categoryService.getCategories(this.pageSize, this.pageNumber).subscribe(res => {
      this.categories = res.result;
      this.pagination = res.pagination;

      this.cdr.detectChanges();
    });
  }

  edit(id : number){ 
    this.router.navigate(['/manage/categories/edit/'+id]);
  }

}
