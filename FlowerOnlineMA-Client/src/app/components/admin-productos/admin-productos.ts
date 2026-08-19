import { Component, OnInit, ChangeDetectorRef} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProductoService } from '../../services/producto.service';
import { Producto, Categoria } from '../../models/producto';


@Component({
  selector: 'app-admin-productos',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './admin-productos.html'
})
export class AdminProductosComponent implements OnInit {
  productos: Producto[] = [];
  categorias: Categoria[] = [];
  
  productoForm!: FormGroup;
  modalAbierto: boolean = false;
  modoEdicion: boolean = false;
  
  readonly defaultImageSvg: string ='data:image/svg+xml;utf8,<svg xmlns="http://www.w3.org/2000/svg" width="60" height="60" viewBox="0 0 24 24" fill="none" stroke="%239CA3AF" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect x="3" y="3" width="18" height="18" rx="2" ry="2"/><circle cx="8.5" cy="8.5" r="1.5"/><polyline points="21 15 16 10 5 21"/></svg>';

  imagenSeleccionada: File | null = null;
  imagenPreview: string | null = this.defaultImageSvg;;

  backendUrl: string = 'https://localhost:44368'; 

  constructor(
    private fb: FormBuilder,
    private productoService: ProductoService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.cargarProductos();
    this.cargarCategorias();
  }

  // Inicializa el formulario reactivo con sus validaciones
  initForm(): void {
    this.productoForm = this.fb.group({
      idProducto: [0],
      nombre: ['', [Validators.required, Validators.minLength(3)]],
      descripcion: [''],
      precio: [0, [Validators.required, Validators.min(0.01)]],
      stock: [0, [Validators.required, Validators.min(0)]],
      idCategoria: ['', [Validators.required]]
    });
  }

  // Consume el servicio HTTP para traer la lista desde C#
  cargarProductos(): void {
    this.productoService.listProductos().subscribe({
    next: (data: any[]) => {
      this.productos = data.map(item => ({
        idProducto: item.IdProducto ?? item.idProducto,
        nombre: item.Nombre ?? item.nombre,
        descripcion: item.Descripcion ?? item.descripcion,
        precio: item.Precio ?? item.precio,
        stock: item.Stock ?? item.stock,
        idCategoria: item.IdCategoria ?? item.idCategoria,
        nombreCategoria: item.NombreCategoria ?? item.nombreCategoria,
        rutaImagen: item.RutaImagen ?? item.rutaImagen
      }));

      this.cdr.detectChanges();
    },
      error: (err) => console.error('Error al listar productos:', err)
    });
  }

  cargarCategorias(): void {
    // Categorías base para vincular con las flores
    this.categorias = [
      { idCategoria: 1, nombre: 'Ramos y Bouquets', estado: true },
      { idCategoria: 2, nombre: 'Cajas de Flores', estado: true },
      { idCategoria: 3, nombre: 'Plantas y Orquídeas', estado: true },
      { idCategoria: 4, nombre: 'Packs Especiales', estado: true }
    ];
  }

  // Prepara el modal para REGISTRAR
  abrirModalNuevo(): void {
    this.modoEdicion = false;
    this.imagenSeleccionada = null;
    this.imagenPreview = this.defaultImageSvg;
    this.productoForm.reset({ idProducto: 0, precio: 0, stock: 0, idCategoria: '' });
    this.modalAbierto = true;
  }

  // Prepara el modal para EDITAR cargando los datos del producto
  abrirModalEditar(prod: Producto): void {
    this.modoEdicion = true;
    this.imagenSeleccionada = null;
    this.imagenPreview = this.obtenerImagen(prod.rutaImagen);
  
    this.productoForm.patchValue({
      idProducto: prod.idProducto,
      nombre: prod.nombre,
      descripcion: prod.descripcion,
      precio: prod.precio,
      stock: prod.stock,
      idCategoria: prod.idCategoria
  });

  this.modalAbierto = true;
  }

  cerrarModal(): void {
    this.modalAbierto = false;
  }

  // Captura la imagen de la pc del usuario y genera el PREVIEW
  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.imagenSeleccionada = file;
      const reader = new FileReader();
      reader.onload = () => {
        this.imagenPreview = reader.result as string;
        this.cdr.detectChanges();
      };
      reader.readAsDataURL(file);
    }
  }

  // Envía el registro hacia C#
  guardarProducto(): void {
    if (this.productoForm.invalid) {
      this.productoForm.markAllAsTouched();
      return;
    }

    const productoData: Producto = this.productoForm.value;

    this.productoService.guardarProducto(productoData, this.imagenSeleccionada || undefined).subscribe({
      next: (res) => {
        if (res.resultado) {
          alert('Producto procesado correctamente');
          this.cerrarModal();
          this.cargarProductos();
        } else {
          alert('Mensaje de C#: ' + res.mensaje);
        }
      },
      error: (err) => console.error('Error al guardar:', err)
    });
  }

  // Elimina invocando el SP de C#
  eliminarProducto(idProducto: number): void {
    if (confirm('¿Deseas eliminar este registro?')) {
      this.productoService.eliminarProducto(idProducto).subscribe({
        next: (res) => {
          if (res.resultado) {
            alert('Producto eliminado');
            this.cargarProductos();
          } else {
            alert('Error: ' + res.mensaje);
          }
        }
      });
    }
  }

  //Otener Imagen
  obtenerImagen(ruta: string | null | undefined): string {
    if (!ruta || ruta.includes('placeholder')) {
      return this.defaultImageSvg;
    }
  
    if (ruta.startsWith('http://') || ruta.startsWith('https://')) {
      return ruta;
    }

    const cleanRuta = ruta.startsWith('/') ? ruta : `/${ruta}`;
    return `${this.backendUrl}${cleanRuta}`;
  }
}