export interface Usuario {
  idUsuario: number;
  nombre: string;
  apellido: string;
  correo: string;
  esAdmin: boolean;
  estado: boolean;
  rutaAvatar?: string;
}

export interface LoginRequest {
  correo: string;
  clave: string;
}

export interface LoginResponse {
  resultado: boolean;
  mensaje: string;
  usuario?: Usuario;
}