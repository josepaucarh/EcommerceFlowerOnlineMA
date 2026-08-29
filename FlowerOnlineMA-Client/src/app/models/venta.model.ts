export interface DetalleVenta {
    idProducto:number;
    cantidad: number;
  precio: number;
}

export interface Venta {
  idVenta?: number;
  idUsuario: number;
  total: number;
  tipoEntrega: 'EnvioDomicilio' | 'RecojoTienda';
  costoEnvio: number;
  direccionEnvio?: string;
  mensajeDedicatoria?: string;
  detalleVenta: DetalleVenta[];
}