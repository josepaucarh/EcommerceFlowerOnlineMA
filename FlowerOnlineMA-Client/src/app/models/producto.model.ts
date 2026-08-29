export interface Producto {
  idProducto: number;
  nombre: string;
  descripcion?: string;
  precio: number;
  stock: number;
  rutaImagen?: string;
  idCategoria: number;
  nombreCategoria?: string;
  tipoLuz?: string;
  frecuenciaRiego?: string;
  nivelCuidado?: string;
}

export interface RespuestaAPI {
  resultado: boolean;
  mensaje: string;
}