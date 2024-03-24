import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SubcategoryComponent } from './subcategory.component';
import { SubcategoryRoutingModule } from './subcategory.routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SubcategoryFormComponent } from './subcategory-form/subcategory-form.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { BsCommonModule } from 'src/app/_bsCommon/bs-common.module';
import { SubcategoryTableComponent } from './subcategory-table/subcategory-table.component';

@NgModule({
  imports: [
    CommonModule,
    SubcategoryRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    PaginationModule.forRoot(), 
    BsCommonModule,
  ],
  declarations: [
    SubcategoryComponent,
    SubcategoryFormComponent,
    SubcategoryTableComponent
  ]
})
export class SubcategoryModule { }
