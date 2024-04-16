import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CategoryService } from '../apps/services/category.service';
import { Category } from '../apps/models/category';
import { Pagination } from '../apps/models/pagination';
 

@Component({
  selector: 'app-website',
  templateUrl: './website.component.html',
  styleUrls: ['./website.component.scss']
})
export class WebsiteComponent implements OnInit {
  categories : Category[] = [];
  pageNumber = 1;
  pageSize = 10;

  pagination: Pagination = {
    currentPage: 1,
    itemsPerPage: this.pageSize,
    totalCount: 0,
    totalItems: 0,
    totalPages : 0,
  };

  myInterval = 1500;
  activeSlideIndex = 0;
  slides: {image: string; text?: string}[] = [
    {image: './assets/media/avatars/300-1.jpg'},
    {image: './assets/media/avatars/300-2.jpg'},
    {image: './assets/media/avatars/300-3.jpg'}
  ];

  slidesStore: {id : string, src : string,alt? : string,title? : string }[]
  = [
    {src: './assets/media/avatars/300-1.jpg', id: '01'},
    {src: './assets/media/avatars/300-2.jpg', id: '02'},
    {src: './assets/media/avatars/300-3.jpg', id: '03'}
  ];;
 
  constructor(private categoryService : CategoryService,
    private cdr: ChangeDetectorRef) { }

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
