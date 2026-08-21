import { Component, OnInit, inject, ChangeDetectorRef} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductoService } from '../../services/producto.service';
import { CategoriaService } from '../../services/categoria.service';
import { Producto } from '../../models/producto.model';
import { Categoria } from '../../models/producto.model';

@Component({
  selector: 'app-catalogo',
  standalone:true,
  imports: [CommonModule],
  templateUrl: './catalogo.html',
  styleUrl: './catalogo.css',
})
export class Catalogo implements OnInit{
  private productoService = inject(ProductoService);
  private categoriaService = inject(CategoriaService);
  private cdr = inject(ChangeDetectorRef);

  listaProductos: Producto[] = [];
  listaCategorias: Categoria[] = [];

  ngOnInit(): void {
    this.cargarProductos();
    this.cargarCategorias();
  }

  cargarProductos(): void{
      this.productoService.listProductos().subscribe({
        next:(data) => {
          console.log('Productos recibidos: ', data);
          this.listaProductos = data;
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
}
