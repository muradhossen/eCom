import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WebsiteComponent } from './website.component';
import { WebsiteRoutingModule } from './website.routing.module';  
import { CarouselModule } from 'ngx-owl-carousel-o';
import { SharedPipeModule } from 'src/app/_pipe/shared-pipe.module';
import { ProductCardComponent } from './product-card/product-card.component';
import { ProductContainerComponent } from './product-container/product-container.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { ProductsComponent } from './products/products.component';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { CategoryContainerComponent } from './category-container/category-container.component';
import { CategoryCardComponent } from './category-card/category-card.component';
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    WebsiteRoutingModule,
    CarouselModule,
    SharedPipeModule,
    ModalModule.forRoot(),
    TabsModule,
    AccordionModule,
    FormsModule
  ],
  declarations: [
    WebsiteComponent,
    ProductCardComponent,
    ProductContainerComponent,
    ProductDetailsComponent,
    ProductsComponent,
    CategoryContainerComponent,
    CategoryCardComponent,
    
  ]
})
export class WebsiteModule { }
