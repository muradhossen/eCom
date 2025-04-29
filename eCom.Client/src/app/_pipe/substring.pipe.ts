import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'substring'
})
export class SubstringPipe implements PipeTransform {

  transform(value: string, maxLength: number = 50): string {

    if (!value || maxLength >= value.length) {
      return value;
    }
    
    return value.slice(0, maxLength) + "...";
  }

}
