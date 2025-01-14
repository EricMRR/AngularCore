
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, Subject, forkJoin, throwError } from 'rxjs';
import { mergeMap, tap, catchError, retry } from 'rxjs/operators';

import { Dispositivo } from '../models/Dispositivo';

@Injectable({ providedIn: 'root' })
export class DispositivoService {
  constructor(protected http: HttpClient) {
  }
  ENDPOINT_URL: string = "http://localhost:5000/Dispositivo";

  getDispositivo<Dispositivo>(): Observable<Dispositivo> {
    return this.http.get<Dispositivo>(this.ENDPOINT_URL).pipe(retry(3), catchError(this.handleError));
  }
  getDispositivoId<Dispositivo>(Id: number): Observable<Dispositivo> {
    return this.http.get<Dispositivo>(this.ENDPOINT_URL + "/?Id=" + Id).pipe(retry(3), catchError(this.handleError));
  }
  postDispositivo<Dispositivo>(filtro: Dispositivo): Observable<Dispositivo> {
    return this.http.post<Dispositivo>(this.ENDPOINT_URL, filtro).pipe(retry(3), catchError(this.handleError));
  }
  putDispositivo<Dispositivo>(filtro: Dispositivo): Observable<Dispositivo> {
    return this.http.put<Dispositivo>(this.ENDPOINT_URL, filtro).pipe(retry(3), catchError(this.handleError));
  }
  deleteDispositivo<Dispositivo>(Id: number): Observable<Dispositivo> {
    return this.http.delete<Dispositivo>(this.ENDPOINT_URL + "/?Id=" + Id).pipe(retry(3), catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = 'Error: ${error.error.message}';
    } else {
      errorMessage = 'Código de error: ${error.status}\nMensaje: ${error.message}';
    }
    return throwError(errorMessage);
  }
}
