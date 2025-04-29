import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountRoutingModule } from './account-routing.module';
import { AccountComponent } from './account.component'; 
import { SettingsComponent } from './settings/settings.component';
import { ProfileDetailsComponent } from './settings/forms/profile-details/profile-details.component';
 
import {SharedModule} from "../../_metronic/shared/shared.module";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AccountComponent, 
    SettingsComponent,
    ProfileDetailsComponent 
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,  
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
  ],
})
export class AccountModule {}
