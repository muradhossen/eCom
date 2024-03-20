import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryComponent } from './category.component';
import { CategoryRoutingModule } from './category.routing.module';
import { CategoryTableComponent } from './category-table/category-table.component'; 
import { CategoryFormComponent } from './category-form/category-form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';

@NgModule({
  imports: [
    CommonModule,
    CategoryRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    PaginationModule.forRoot()
  ],
  declarations: [
    CategoryComponent,
    CategoryTableComponent,
    CategoryFormComponent,
  ]
})
export class CategoryModule { }
