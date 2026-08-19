export interface Producto {
  idProducto: number;
  nombre: string;
  descripcion?: string;
  precio: number;
  stock: number;
  rutaImagen?: string;
  idCategoria: number;
  nombreCategoria?: string;
}

export interface Categoria {
  idCategoria: number;
  nombre: string;
  estado: boolean;
}

export interface RespuestaAPI {
  resultado: boolean;
  mensaje: string;
}