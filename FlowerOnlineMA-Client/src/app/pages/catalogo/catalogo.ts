import { Component, OnInit, inject, ChangeDetectorRef} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ProductoService } from '../../services/producto.service';
import { CategoriaService } from '../../services/categoria.service';
import { Producto } from '../../models/producto.model';
import { Categoria } from '../../models/categoria.model';
import { debounceTime, distinctUntilChanged } from 'rxjs';
import { ProductoCard } from '../../components/producto-card/producto-card';

@Component({
  selector: 'app-catalogo',
  standalone:true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    ProductoCard
  ],
  templateUrl: './catalogo.html',
  styleUrl: './catalogo.css',
})
export class Catalogo implements OnInit{
  private productoService = inject(ProductoService);
  private categoriaService = inject(CategoriaService);
  private cdr = inject(ChangeDetectorRef);

  readonly backendUrl: string ='https://localhost:44368';
  readonly defaultImageSvg: string = 'data:image/svg+xml;utf8,<svg xmlns="http://www.w3.org/2000/svg" width="60" height="60" viewBox="0 0 24 24" fill="none" stroke="%239CA3AF" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect x="3" y="3" width="18" height="18" rx="2" ry="2"/><circle cx="8.5" cy="8.5" r="1.5"/><polyline points="21 15 16 10 5 21"/></svg>';

  listaProductos: Producto[] = [];
  productosFiltrados: Producto[] = [];
  listaCategorias: Categoria[] = [];

  //Estado del Modal de detalle
  productoSeleccionado: Producto | null = null;
  modalDetalleAbierto: boolean = false;
  cantidadSeleccionada: number = 1;

  // Control para la búsqueda en tiempo real
  searchControl = new FormControl('');
  categoriaSeleccionadaId: number = 0;

  ngOnInit(): void {
    this.cargarProductos();
    this.cargarCategorias();
    this.escucharBuscador();
  }

  //Método para formatear y sanitizar la URL de la imagen
  obtenerImagen(ruta: string | null | undefined): string{
    if(!ruta || ruta.includes('placeholder')){
      return this.defaultImageSvg;
    }
    //Si la ruta apunta a nuestro assets local
    if (ruta.startsWith('assets/')) {
      return ruta;
    }

    //Si la ruta ya es una URL absoluta
    if (ruta.startsWith('http://') || ruta.startsWith('https://')) {
      return ruta;
    }

    const cleanRuta = ruta.startsWith('/')? ruta: `/${ruta}`;
    return `${this.backendUrl}${cleanRuta}`;
  }

  cargarProductos(): void{
      this.productoService.listProductos().subscribe({
        next:(data) => {
          this.listaProductos = data;
          this.aplicarFiltros();
          this.cdr.detectChanges();
        },
        error: (err)=> console.error('Error al cargar productos:', err)
      });
    }

  cargarCategorias(): void{
    this.categoriaService.listCategorias().subscribe({
      next: (data) =>{
        this.listaCategorias = data;
        this.cdr.detectChanges();
      },
      error: (err) => console.error('Error al cargar categorías', err)
    });
  }

  //Optimizar la entrada del usuario en JxJS
  escucharBuscador(): void{
    this.searchControl.valueChanges.pipe(
      debounceTime(300),
      distinctUntilChanged()
    ).subscribe(() =>{
      this.aplicarFiltros();
    });
  }

  seleccionarCategoria(idCategoria: number): void{
    this.categoriaSeleccionadaId = idCategoria;
    this.aplicarFiltros();
  }

  //Filtrado combinado de texto y Categoría
  aplicarFiltros(): void{
    const termino = (this.searchControl.value || '').toLowerCase().trim();

    this.productosFiltrados= this.listaProductos.filter(prod =>{
      const coincideTexto = prod.nombre.toLowerCase().includes(termino) ||
                            (prod.descripcion && prod.descripcion.toLowerCase().includes(termino));

      const coincideCategoria = this.categoriaSeleccionadaId === 0 ||
                                prod.idCategoria === this.categoriaSeleccionadaId;
      return coincideTexto && coincideCategoria;
    });
    this.cdr.detectChanges();
  }

  //Detalle de Producto
  abrirDetalle(producto: Producto): void{
    this.productoSeleccionado = producto;
    this.cantidadSeleccionada = 1;
    this.modalDetalleAbierto= true;
    this.cdr.detectChanges();
  }

  //Cerrar Detalle
  cerrarDetalle(): void{
    this.modalDetalleAbierto= false;
    this.productoSeleccionado= null;
    this.cdr.detectChanges();
  }

  incrementarCantidad(): void{
    if(this.productoSeleccionado && this.cantidadSeleccionada < this.productoSeleccionado.stock){
      this.cantidadSeleccionada++;
    }
  }

  decrementarCantidad(): void{
    if(this.cantidadSeleccionada>1){
      this.cantidadSeleccionada--;
    }
  }

  agregarAlCarrito(): void{
    if(!this.productoSeleccionado) return;

    //Conexión para la fase 3 CartService
    alert(`Añadido al carrito: ${this.cantidadSeleccionada} unidad(es) de ${this.productoSeleccionado.nombre}`);
    this.cerrarDetalle();
  }
}
