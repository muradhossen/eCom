  
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';  
import { CategoryComponent } from './category.component';

const routes: Routes = [
  {
    path: '',
    component: CategoryComponent,
    children: [ 
      {
        path: 'add',
        component: CategoryComponent,
      },
      { path: '', redirectTo: 'categories', pathMatch: 'full' },
      { path: '**', redirectTo: 'categories', pathMatch: 'full' },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CategoryRoutingModule {}