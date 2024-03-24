import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductComponent } from './product.component';
import { ProductRoutingModule } from './product.routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductFormComponent } from './product-form/product-form.component';
import { ProductTableComponent } from './product-table/product-table.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { BsCommonModule } from 'src/app/_bsCommon/bs-common.module';

@NgModule({
  imports: [
    CommonModule,
    ProductRoutingModule,
    ReactiveFormsModule,
    PaginationModule.forRoot(), 
    BsCommonModule,
    FormsModule,
  ],
  declarations: [
    ProductComponent,
    ProductFormComponent,
    ProductTableComponent
  ]
})
export class ProductModule { }
