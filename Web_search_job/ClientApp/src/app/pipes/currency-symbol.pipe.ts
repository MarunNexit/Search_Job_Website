import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'currencySymbol'
})
export class CurrencySymbolPipe implements PipeTransform {
  transform(value: number, currencyType: string): string {
    switch (currencyType) {
      case 'UAH':
        return value + ' грн';
      case 'USD':
        return value + ' $';
      // Додайте інші валюти за необхідності
      default:
        return value.toString();
    }
  }
}
