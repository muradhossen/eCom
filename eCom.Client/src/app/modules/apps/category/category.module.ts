import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryComponent } from './category.component';
import { CategoryRoutingModule } from './category.routing.module';
import { CategoryTableComponent } from './category-table/category-table.component';

@NgModule({
  imports: [
    CommonModule,
    CategoryRoutingModule
  ],
  declarations: [
    CategoryComponent,
    CategoryTableComponent
  ]
})
export class CategoryModule { }
