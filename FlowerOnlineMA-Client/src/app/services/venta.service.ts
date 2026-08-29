import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Venta } from '../models/venta.model';
import { RespuestaAPI } from '../models/producto.model';

@Injectable({
  providedIn: 'root'
})
export class VentaService {
  private apiUrl = 'https://localhost:44368/api/ventas';

  constructor(private http: HttpClient) { }

  registrarVenta(venta: Venta): Observable<RespuestaAPI> {
    return this.http.post<RespuestaAPI>(`${this.apiUrl}/registrar`, venta);
  }
}