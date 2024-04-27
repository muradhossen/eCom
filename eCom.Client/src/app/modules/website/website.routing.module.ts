import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { WebsiteComponent } from "./website.component";
import { ProductDetailsComponent } from "./product-details/product-details.component";
import { ProductsComponent } from "./products/products.component";

const routes: Routes = [
  {
    path: '', 
    children: [
      {path : '', component : WebsiteComponent},
      {
        path: '',
        redirectTo: '',
        pathMatch: 'full',
      },
      {
        path: 'products',
        children: [
          { path: '', component: ProductsComponent },
          { path: ':name', component: ProductDetailsComponent },
        ]
      },
      {path : "categories" , component : ProductsComponent},
      { path: '', redirectTo: 'web', pathMatch: 'full' },
      { path: '**', redirectTo: 'web', pathMatch: 'full' },
    ],

  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class WebsiteRoutingModule { }