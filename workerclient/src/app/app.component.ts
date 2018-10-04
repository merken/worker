import { SignalRService } from './signalr.service';
import { CalculationService } from './calculation.service';
import { Component, OnInit } from '@angular/core';
import { Calculation } from './calculation.model';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  calculationForm = new FormGroup({
    a: new FormControl(),
    b: new FormControl(),
    calculationType: new FormControl()
  });

  connected: boolean;

  requests = 0;
  results: Array<Calculation>;

  constructor(private calculationService: CalculationService, private signalRService: SignalRService) {
    this.results = [];
  }

  ngOnInit(): void {
    this.signalRService.configure();
    this.signalRService.initializeConnection().subscribe(connected => {
      this.connected = connected;

      this.signalRService.subscribe((calculation) => {
        this.results.push(calculation);
      });
    });
  }

  submitCalculation($event) {
    this.calculationService.submitCalculation({
      a: this.calculationForm.value.a,
      b: this.calculationForm.value.b,
      type: this.calculationForm.value.calculationType
    }).subscribe(x => this.requests++);
  }
}
