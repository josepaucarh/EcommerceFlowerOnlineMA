import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Producto, RespuestaAPI } from '../models/producto';

@Injectable({
  providedIn: 'root'
})
export class ProductoService {
  // Ajusta el puerto según tu API C#
  private apiUrl = 'https://localhost:44368/Productos'; 

  constructor(private http: HttpClient) { }

  listProductos(): Observable<Producto[]> {
    return this.http.get<Producto[]>(`${this.apiUrl}/ListarProductos`);
  }

  guardarProducto(producto: Producto, imagenFile?: File): Observable<RespuestaAPI> {
    const formData = new FormData();
    formData.append('IdProducto', producto.idProducto.toString());
    formData.append('Nombre', producto.nombre);
    formData.append('Descripcion', producto.descripcion || '');
    formData.append('Precio', producto.precio.toString());
    formData.append('Stock', producto.stock.toString());
    formData.append('IdCategoria', producto.idCategoria.toString());

    if (imagenFile) {
      formData.append('imagenFile', imagenFile);
    }

    return this.http.post<RespuestaAPI>(`${this.apiUrl}/GuardarProducto`, formData);
  }

  eliminarProducto(idProducto: number): Observable<RespuestaAPI> {
    return this.http.post<RespuestaAPI>(`${this.apiUrl}/EliminarProducto`, { IdProducto: idProducto });
  }
}
