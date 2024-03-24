import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryComponent } from './category.component';
import { CategoryRoutingModule } from './category.routing.module';
import { CategoryTableComponent } from './category-table/category-table.component'; 
import { CategoryFormComponent } from './category-form/category-form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
// import { ModalModule } from 'ngx-bootstrap/modal';
import { BsCommonModule } from 'src/app/_bsCommon/bs-common.module';

@NgModule({
  imports: [
    CommonModule,
    CategoryRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    PaginationModule.forRoot(),
    // ModalModule.forRoot(),
    BsCommonModule,
  ],
  declarations: [
    CategoryComponent,
    CategoryTableComponent,
    CategoryFormComponent,
  ]
})
export class CategoryModule { }
