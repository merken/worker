import { Calculation } from './calculation.model';
import { Injectable } from '@angular/core';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { Observable } from 'rxjs';


export type CalculationCallback = (msg: Calculation) => void;

@Injectable()
export class SignalRService {
    private hubConnection: HubConnection;

    public configure() {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://localhost:5001/hubs/work')
            .configureLogging(signalR.LogLevel.Information)
            .build();
    }

    public initializeConnection(): Observable<boolean> {
        return Observable.create(observer => {
            this.hubConnection
                .start()
                .then(() => {
                    observer.next(true);
                    observer.complete();
                })
                .catch(err => {
                    observer.error(err);
                    observer.complete();
                });
        });
    }

    public subscribe(handler: CalculationCallback) {
        this.hubConnection.on(`WORK_ITEM_PROCESSED`, (msg: Calculation) => {
            handler(msg);
        });
    }
}
