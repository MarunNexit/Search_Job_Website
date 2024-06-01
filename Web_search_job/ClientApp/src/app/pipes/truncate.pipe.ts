import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'truncate'
})
export class TruncatePipe implements PipeTransform {
  transform(value: string | undefined, limit: number, withDots: boolean = true): string {
    if(value){
      if(withDots){
        if (value.length > limit) {
          return value.substring(0, limit) + '...';
        }
      }
      else{
        if (value.length > limit) {
          return value.substring(0, limit) ;
        }
      }
    }
    return <string>value;
  }
}
