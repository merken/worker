import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'calculationtype' })
export class CalculationTypePipe implements PipeTransform {
    transform(value: string, args?: any): string {
        switch (value) {
            case 'addition': return '+';
            case 'subtraction': return '-';
            case 'multiplication': return '*';
            case 'division': return '/';
        }
        return '';
    }
}
