import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WebsiteComponent } from './website.component';
import { WebsiteRoutingModule } from './website.routing.module';  
import { CarouselModule } from 'ngx-owl-carousel-o';
import { SharedPipeModule } from 'src/app/_pipe/shared-pipe.module';
import { ProductCardComponent } from './product-card/product-card.component';


@NgModule({
  imports: [
    CommonModule,
    WebsiteRoutingModule,
    CarouselModule,
    SharedPipeModule
  ],
  declarations: [
    WebsiteComponent,
    ProductCardComponent
  ]
})
export class WebsiteModule { }
