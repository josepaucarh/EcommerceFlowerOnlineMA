import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginRequest, LoginResponse, Usuario } from '../models/usuario.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:44368/api/usuarios';

  constructor(private http: HttpClient) { }

  login(credentials: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, credentials);
  }

  // Guardar usuario en LocalStorage para mantener la sesión
  guardarSesion(usuario: Usuario): void {
    localStorage.setItem('usuario_flower', JSON.stringify(usuario));
  }

  obtenerSesion(): Usuario | null {
    const user = localStorage.getItem('usuario_flower');
    return user ? JSON.parse(user) : null;
  }

  cerrarSesion(): void {
    localStorage.removeItem('usuario_flower');
  }
}