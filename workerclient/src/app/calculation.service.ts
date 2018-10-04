import { Calculation } from './calculation.model';
import { Injectable } from '@angular/core';

import { HttpService } from './http.service';

@Injectable()
export class CalculationService {

    constructor(private httpService: HttpService) { }

    public submitCalculation(calculation: Calculation) {
        return this.httpService.makeRequest('POST', '/api/calculation', { body: calculation });
    }
}
