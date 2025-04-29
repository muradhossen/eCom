import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Pagination } from '../../models/pagination';
import { Router } from '@angular/router';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ConfirmService } from 'src/app/_bsCommon/confirm.service';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product';

@Component({
  selector: 'app-product-table',
  templateUrl: './product-table.component.html',
  styleUrls: ['./product-table.component.scss'],
  providers:[ConfirmService]
})
export class ProductTableComponent implements OnInit {

  pageNumber = 1;
  pageSize = 10;
  products: Product[] = [];

  pagination: Pagination = {
    currentPage: 1,
    itemsPerPage: this.pageSize,
    totalCount: 0,
    totalItems: 0,
    totalPages : 0
  };


  modalRef?: BsModalRef;
 

  constructor(private productService: ProductService,
    private cdr: ChangeDetectorRef,
    private router: Router, 
    private confirmService: ConfirmService) { }

  ngOnInit() {

    this.loadProducts();
  }

  pageChanged(event: any) {

    this.pageNumber = event.page;
    this.loadProducts();
  }

  loadProducts() {
    this.productService.getProducts(this.pageSize, this.pageNumber).subscribe(res => { 
      this.products = res.result.data;
      this.pagination = res.pagination;

      this.cdr.detectChanges();
    });
  }

  edit(id: number) {
    this.router.navigate(['/manage/products/edit/' + id]);
  } 

  deleteProduct(id: number, name: string) {

    this.confirmService.confirm("Delete message!", `Are you sure to delete <b> ${name} </b> product?`).subscribe((result) => {
      if (result) {

        this.productService.deleteProduct(id).subscribe(
          {
            next: res => {
              if (res.isSuccess) {
                this.loadProducts();
              }
            },
            complete: () => this.modalRef?.hide()
          })
      }
    });

  }

}
