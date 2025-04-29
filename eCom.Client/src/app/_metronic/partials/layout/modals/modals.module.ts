import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {RouterModule} from '@angular/router';
import {InlineSVGModule} from 'ng-inline-svg-2';
 
import {NgbModalModule} from '@ng-bootstrap/ng-bootstrap';
import {SharedModule} from "../../../shared/shared.module";

@NgModule({
  declarations: [ 
  ],
  imports: [
    CommonModule,
    InlineSVGModule,
    RouterModule,
    NgbModalModule,
    SharedModule,
  ],
  exports: [ 
  ],
})
export class ModalsModule {
}
