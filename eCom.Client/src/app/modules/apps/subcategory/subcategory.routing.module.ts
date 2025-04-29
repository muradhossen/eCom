import { Routes, RouterModule } from '@angular/router';
import { SubcategoryComponent } from './subcategory.component';
import { NgModule } from '@angular/core';
import { SubcategoryFormComponent } from './subcategory-form/subcategory-form.component';

const routes: Routes = [
  {
    path: '',
    component: SubcategoryComponent,
    children: [
      { path: '', redirectTo: 'subcategories', pathMatch: 'full' },
      { path: '**', redirectTo: 'subcategories', pathMatch: 'full' },
    ],
  },
  {
    path: 'create',
    component: SubcategoryFormComponent,
  },
  {
    path: 'edit/:id',
    component: SubcategoryFormComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class SubcategoryRoutingModule{};
