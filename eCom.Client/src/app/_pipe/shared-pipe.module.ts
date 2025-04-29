import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { SubstringPipe } from './substring.pipe';

@NgModule({
  imports: [
    CommonModule,
  ],
  declarations: [SubstringPipe],
  exports: [SubstringPipe]

})
export class SharedPipeModule { }
