import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Producto } from '../../models/producto.model';

@Component({
  selector: 'app-producto-card',
  standalone:true,
  imports: [CommonModule],
  templateUrl: './producto-card.html',
  styleUrl: './producto-card.css',
})
export class ProductoCard {
  @Input({ required: true}) producto!: Producto;
  @Output() verDetalle = new EventEmitter<Producto>();

  readonly backendUrl: string = 'https://localhost:44368';
  readonly defaultImageSvg: string = 'data:image/svg+xml;utf8,<svg xmlns="http://www.w3.org/2000/svg" width="60" height="60" viewBox="0 0 24 24" fill="none" stroke="%239CA3AF" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect x="3" y="3" width="18" height="18" rx="2" ry="2"/><circle cx="8.5" cy="8.5" r="1.5"/><polyline points="21 15 16 10 5 21"/></svg>';

  obtenerImagen(ruta: string | null | undefined): string{
    if(!ruta || ruta.includes('placeholder')){
      return this.defaultImageSvg;
    }
    if(ruta.startsWith('assets/') || ruta.startsWith('http://') || ruta.startsWith('https://')){
      return ruta;
    }
    
    const cleanRuta = ruta.startsWith('/')? ruta : `/${ruta}`;
    return `${this.backendUrl}${cleanRuta}`;
  }

  onVerDetalle(): void{
      this.verDetalle.emit(this.producto);
    }
}
