import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { InlineSVGModule } from 'ng-inline-svg-2';
import { RouterModule, Routes } from '@angular/router';
import {
  NgbDropdownModule,
  NgbProgressbarModule,
  NgbTooltipModule,
} from '@ng-bootstrap/ng-bootstrap';
import { TranslateModule } from '@ngx-translate/core'; 
import { LayoutComponent } from './layout.component';
import { ExtrasModule } from '../partials/layout/extras/extras.module';
import { Routing } from '../../pages/routing';
import { AsideComponent } from './components/aside/aside.component';
import { HeaderComponent } from './components/header/header.component';
import { ContentComponent } from './components/content/content.component';
import { FooterComponent } from './components/footer/footer.component';
import { ScriptsInitComponent } from './components/scripts-init/scripts-init.component';
import { ToolbarComponent } from './components/toolbar/toolbar.component';
import { AsideMenuComponent } from './components/aside/aside-menu/aside-menu.component';
import { TopbarComponent } from './components/topbar/topbar.component';
import { PageTitleComponent } from './components/header/page-title/page-title.component';
import { HeaderMenuComponent } from './components/header/header-menu/header-menu.component';
import { 
 
  ModalsModule, 
} from '../partials'; 
import { ThemeModeModule } from '../partials/layout/theme-mode-switcher/theme-mode.module';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { SidebarLogoComponent } from './components/sidebar/sidebar-logo/sidebar-logo.component';
import { SidebarMenuComponent } from './components/sidebar/sidebar-menu/sidebar-menu.component';
import { SidebarFooterComponent } from './components/sidebar/sidebar-footer/sidebar-footer.component';
import { NavbarComponent } from './components/header/navbar/navbar.component';
import { ClassicComponent } from './components/toolbar/classic/classic.component';
 
import {SharedModule} from "../shared/shared.module";
import { UserDropdownComponent } from '../partials/layout/user-dropdown/user-dropdown.component';
import { CartModule } from 'src/app/modules/apps/cart/cart.module';
import { CartComponent } from 'src/app/modules/apps/cart/cart.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: Routing,
  },
];

@NgModule({
  declarations: [
    LayoutComponent,
    AsideComponent,
    HeaderComponent,
    ContentComponent,
    FooterComponent,
    ScriptsInitComponent,
    ToolbarComponent,
    AsideMenuComponent,
    TopbarComponent,
    PageTitleComponent,
    HeaderMenuComponent, 
    SidebarComponent,
    SidebarLogoComponent,
    SidebarMenuComponent,
    SidebarFooterComponent,
    NavbarComponent, 
    ClassicComponent, 
    UserDropdownComponent,
    
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes), 
    InlineSVGModule,
    NgbDropdownModule,
    NgbProgressbarModule,
    ExtrasModule,
    ModalsModule,  
    NgbTooltipModule,
    TranslateModule,
    ThemeModeModule,
    SharedModule,
    CartModule
  ],
  exports: [RouterModule],
})
export class LayoutModule {}
