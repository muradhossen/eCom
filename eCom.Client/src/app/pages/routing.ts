import { Routes } from '@angular/router';

const Routing: Routes = [
  {
    path: 'dashboard',
    loadChildren: () =>
      import('./dashboard/dashboard.module').then((m) => m.DashboardModule),
  }, 
  {
    path: 'crafted/account',
    loadChildren: () =>
      import('../modules/account/account.module').then((m) => m.AccountModule),
    // data: { layout: 'dark-header' },
  },
  {
    path: 'manage/categories',
    loadChildren: () =>
      import('../modules/apps/category/category.module').then((m) => m.CategoryModule),
     
  },
  {
    path: 'manage/subcategories',
    loadChildren: () =>
      import('../modules/apps/subcategory/subcategory.module').then((m) => m.SubcategoryModule), 
  },
  {
    path: 'manage/products',
    loadChildren: () =>
      import('../modules/apps/product/product.module').then((m) => m.ProductModule), 
  },
  {
    path: '',
    loadChildren: () =>
      import('../modules/website/website.module').then((m) => m.WebsiteModule), 
      data: { layout: 'light-header' },
  },
 
  {
    path: '',
    redirectTo: '',
    pathMatch: 'full',
  },
  {
    path: '**',
    redirectTo: 'error/404',
  },
];

export { Routing };
