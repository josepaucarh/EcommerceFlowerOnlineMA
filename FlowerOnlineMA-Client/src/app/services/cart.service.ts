import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Producto } from '../models/producto.model';
import { DetalleVenta } from '../models/venta.model';

export interface CartItem {
  producto: Producto;
  cantidad: number;
}

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private itemsSubject = new BehaviorSubject<CartItem[]>(this.obtenerCarritoGuardado());
  public items$ = this.itemsSubject.asObservable();

  constructor() {}

  // Lee el carrito almacenado previamente
  private obtenerCarritoGuardado(): CartItem[] {
    const saved = localStorage.getItem('verde_decor_cart');
    return saved ? JSON.parse(saved) : [];
  }

  // Guarda en LocalStorage y notifica a los suscriptores
  private guardarStorage(items: CartItem[]): void {
    localStorage.setItem('verde_decor_cart', JSON.stringify(items));
    this.itemsSubject.next(items);
  }

  // Agrega o incrementa la cantidad de un producto
  agregar(producto: Producto, cantidad: number = 1): void {
    const actual = [...this.itemsSubject.value];
    const index = actual.findIndex(i => i.producto.idProducto === producto.idProducto);

    if (index > -1) {
      actual[index].cantidad += cantidad;
    } else {
      actual.push({ producto, cantidad });
    }
    this.guardarStorage(actual);
  }

  // Reduce o incrementa cantidad directamente
  actualizarCantidad(idProducto: number, cantidad: number): void {
    let actual = [...this.itemsSubject.value];
    const index = actual.findIndex(i => i.producto.idProducto === idProducto);

    if (index > -1) {
      if (cantidad <= 0) {
        actual = actual.filter(i => i.producto.idProducto !== idProducto);
      } else {
        actual[index].cantidad = cantidad;
      }
      this.guardarStorage(actual);
    }
  }

  // Elimina un item del carrito
  eliminar(idProducto: number): void {
    const actual = this.itemsSubject.value.filter(i => i.producto.idProducto !== idProducto);
    this.guardarStorage(actual);
  }

  // Vacía el carrito por completo (después del checkout)
  limpiar(): void {
    localStorage.removeItem('verde_decor_cart');
    this.itemsSubject.next([]);
  }

  // Retorna la suma total de dinero
  obtenerTotal(): number {
    return this.itemsSubject.value.reduce((acc, item) => acc + (item.producto.precio * item.cantidad), 0);
  }

  // Mapea los items para el payload de la Venta en C#
  obtenerDetallesVenta(): DetalleVenta[] {
    return this.itemsSubject.value.map(item => ({
      idProducto: item.producto.idProducto,
      cantidad: item.cantidad,
      precio: item.producto.precio
    }));
  }
}