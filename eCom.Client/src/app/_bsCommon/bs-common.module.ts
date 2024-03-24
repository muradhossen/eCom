import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsConfirmModalComponent } from './bs-confirm-modal/bs-confirm-modal.component';
import { ModalModule } from 'ngx-bootstrap/modal';

@NgModule({
  imports: [
    CommonModule,
    ModalModule.forRoot(),
  ],
  declarations: [BsConfirmModalComponent]
})
export class BsCommonModule { }
