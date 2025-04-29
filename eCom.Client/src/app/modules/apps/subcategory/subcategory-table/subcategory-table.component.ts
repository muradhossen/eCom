import { ChangeDetectorRef, Component, OnInit } from '@angular/core'; 
import { Category } from '../../models/category';
import { Pagination } from '../../models/pagination';
import { Router } from '@angular/router';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ConfirmService } from 'src/app/_bsCommon/confirm.service';
import { SubcategoryService } from '../../services/subcategory.service';

@Component({
  selector: 'app-subcategory-table',
  templateUrl: './subcategory-table.component.html',
  styleUrls: ['./subcategory-table.component.scss']
})
export class SubcategoryTableComponent implements OnInit {

  pageNumber = 1;
  pageSize = 10;
  categories: Category[] = [];

  pagination: Pagination = {
    currentPage: 1,
    itemsPerPage: this.pageSize,
    totalCount: 0,
    totalItems: 0,
    totalPages : 0
  };


  modalRef?: BsModalRef;
 

  constructor(private subCategoryService: SubcategoryService,
    private cdr: ChangeDetectorRef,
    private router: Router, 
    private confirmService: ConfirmService) { }

  ngOnInit() {

    this.loadSubCategories();
  }

  pageChanged(event: any) {

    this.pageNumber = event.page;
    this.loadSubCategories();
  }

  loadSubCategories() {
    this.subCategoryService.getSubCategories(this.pageSize, this.pageNumber).subscribe(res => {
      this.categories = res.result;
      this.pagination = res.pagination;

      this.cdr.detectChanges();
    });
  }

  edit(id: number) {
    this.router.navigate(['/manage/subcategories/edit/' + id]);
  } 

  deleteSubCategory(id: number, name: string) {

    this.confirmService.confirm("Delete message!", `Are you sure to delete <b> ${name} </b> subcategory?`).subscribe((result) => {
      if (result) {

        this.subCategoryService.deleteSubCategory(id).subscribe(
          {
            next: res => {
              if (res.isSuccess) {
                this.loadSubCategories();
              }
            },
            complete: () => this.modalRef?.hide()
          })
      }
    });

  }

}
