import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router"; 
import { CheckoutComponent } from "./checkout.component";

const routes: Routes = [
  {
    path: '', 
    children: [
      {path : '', component : CheckoutComponent},
      // {
      //   path: '',
      //   redirectTo: '',
      //   pathMatch: 'full',
      // }, 
      // { path: '', redirectTo: 'web', pathMatch: 'full' },
      // { path: '**', redirectTo: 'web', pathMatch: 'full' },
    ],

  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CheckoutRoutingModule { }