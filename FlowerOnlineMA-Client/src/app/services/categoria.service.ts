import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Categoria } from '../models/categoria.model';


@Injectable({
  providedIn: 'root'
})
//Ajusta la URL base según el puerto de tu API en C#
export class CategoriaService {
  private apiUrl = 'https://localhost:44368/api/categorias';

  constructor(private http: HttpClient){}

  //Obtener lista de categorias
  listCategorias(): Observable<Categoria[]> {
  return this.http.get<Categoria[]>(`${this.apiUrl}/listar`);
  }
}
