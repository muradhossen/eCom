
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoryComponent } from './category.component';
import { CategoryFormComponent } from './category-form/category-form.component';

const routes: Routes = [
  {
    path: '',
    component: CategoryComponent,
    children: [
      { path: '', redirectTo: 'categories', pathMatch: 'full' },
      { path: '**', redirectTo: 'categories', pathMatch: 'full' },
    ],
  },
  {
    path: 'create',
    component: CategoryFormComponent,
  },
  {
    path: 'edit/:id',
    component: CategoryFormComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CategoryRoutingModule { }