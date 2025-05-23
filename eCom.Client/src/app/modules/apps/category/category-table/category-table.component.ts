import { ChangeDetectorRef, Component, OnInit, TemplateRef } from '@angular/core';
import { CategoryService } from '../../services/category.service';
import { Category } from '../../models/category';
import { Pagination } from '../../models/pagination';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ConfirmService } from 'src/app/_bsCommon/confirm.service';

@Component({
  selector: 'app-category-table',
  templateUrl: './category-table.component.html',
  styleUrls: ['./category-table.component.scss'],
  providers: [ConfirmService]
})
export class CategoryTableComponent implements OnInit {
  pageNumber = 1;
  pageSize = 10;
  categories: Category[] = [];

  pagination: Pagination = {
    currentPage: 1,
    itemsPerPage: this.pageSize,
    totalCount: 0,
    totalItems: 0,
    totalPages : 0,
  };


  modalRef?: BsModalRef;
 

  constructor(private categoryService: CategoryService,
    private cdr: ChangeDetectorRef,
    private router: Router, 
    private confirmService: ConfirmService) { }

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

  edit(id: number) {
    this.router.navigate(['/manage/categories/edit/' + id]);
  } 

  deleteCategory(id: number, name: string) {

    this.confirmService.confirm("Delete message!", `Are you sure to delete <b> ${name} </b> category?`).subscribe((result) => {
      if (result) {

        this.categoryService.deleteCategory(id).subscribe(
          {
            next: res => {
              if (res.isSuccess) {
                this.loadCategories();
              }
            },
            complete: () => this.modalRef?.hide()
          })
      }
    });

  }

}
