import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductComponent } from './product.component';
import { ProductRoutingModule } from './product.routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductFormComponent } from './product-form/product-form.component';
import { ProductTableComponent } from './product-table/product-table.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { BsCommonModule } from 'src/app/_bsCommon/bs-common.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgxEditorModule } from 'ngx-editor';
import { PopoverModule } from 'ngx-bootstrap/popover'; 
import { SharedPipeModule } from 'src/app/_pipe/shared-pipe.module';

@NgModule({
  imports: [
    CommonModule,
    ProductRoutingModule,
    ReactiveFormsModule,
    PaginationModule.forRoot(), 
    BsCommonModule,
    FormsModule,
    NgSelectModule,
    PopoverModule.forRoot(),
    SharedPipeModule,
    NgxEditorModule.forRoot({
      locals: {
       
        bold: 'Bold',
        italic: 'Italic', 
        blockquote: 'Blockquote',
        underline: 'Underline',
        strike: 'Strike',
        bullet_list: 'Bullet List',
        ordered_list: 'Ordered List',
        heading: 'Heading',
        h1: 'Header 1',
        h2: 'Header 2',
        h3: 'Header 3',
        h4: 'Header 4',
        h5: 'Header 5',
        h6: 'Header 6', 
        text_color: 'Text Color',
        background_color: 'Background Color', 
        url: 'URL',
        text: 'Text',
        openInNewTab: 'Open in new tab',
        insert: 'Insert',
        altText: 'Alt Text',
        title: 'Title',
        remove: 'Remove',
      },
    })
  ],
  declarations: [
    ProductComponent,
    ProductFormComponent,
    ProductTableComponent 
  ]
})
export class ProductModule { }
