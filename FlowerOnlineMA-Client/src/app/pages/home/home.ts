import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router } from '@angular/router';
import { ProductoCard } from '../../components/producto-card/producto-card';
import { Producto } from '../../models/producto.model';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule,RouterLink,ProductoCard],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
  productos: Producto[]=[
  { 
      idProducto: 1, 
      nombre: 'Monstera Deliciosa', 
      precio: 85, 
      stock: 10, 
      idCategoria: 1,
      nombreCategoria: 'Interior',
      descripcion: 'Planta de interior de hojas grandes y verdes.',
      rutaImagen: 'https://lh3.googleusercontent.com/aida-public/AB6AXuARaqs6z4_l0lQdvFRcp65mNoWBDbP38dERPgGw8oReLoDp3hhvOv8Io8Ds2UZ3zQ3aznF0heCzipx98bLQ5LqJCRddINHblsCrSGaM1US2PDZgQeEzWTaC7PbNcOCx1cqHJ-MWiPyTeUFokwIz-yrcY_-qHoBr-jkP54JgVwuw30apq319Kk_lKNsYnbCuZRJakPveZKPJ-3BhGa6CupnatGYyeb9hJsUdTDIbgO5JJw3GRv4RFo5fuQ' 
    },
    { 
      idProducto: 2, 
      nombre: 'Sansevieria', 
      precio: 45, 
      stock: 5, 
      idCategoria: 1,
      nombreCategoria: 'Interior',
      descripcion: 'Planta purificadora de aire muy resistente.',
      rutaImagen: 'https://lh3.googleusercontent.com/aida-public/AB6AXuAEn1aX98IynEeAzWfW7cpOZQVMFtPumyDZ3L-NNc4R7kMUsvTgng1gf6uCOSM7Wa4cws_zDjHGZTpYC53boGGDhffuV8oKCyPsc1U_mRhksp2adr-42hsoTnqRRQSMYj1oVvai78c6HSS9NL_WtolDgAXSwM3_367tv_MAPAkFOqr9ViWjC_3_NOqUKv9rAPgtc5FxhZx424h7BqxIzRkQtxaYnQYwMfdXwRgh3xJWQbc7JU4HCzo5Aw' 
    },
    { 
      idProducto: 3, 
      nombre: 'Ficus Lyrata', 
      precio: 120, 
      stock: 8, 
      idCategoria: 1,
      nombreCategoria: 'Interior',
      descripcion: 'Planta decorativa ideal para espacios iluminados.',
      rutaImagen: 'https://lh3.googleusercontent.com/aida-public/AB6AXuCNjQ9tmC95hBo59NzKt71ki-oZ1EViMuCCXv6gpnpHM9edTqhYAfiQUjJfU8Z1g4qNWpUKIvNXa4hAdsiTskSL_uGechEICw9SZkoqhSxy0cUsFhP-qK192M_lqNEuoGJrzb7WQMApICeHN7u2xHy7AD7TVxrLTKfjx6qAyuwMguNhJD7hbHDxpN91iC_1bbCZgEeA42ZL4EguexcT5jcnlyvSxYlnWwiF6TztMmTI3wnY8rarXO9CBw' 
    },
    { 
      idProducto: 4, 
      nombre: 'Maceta de Barro Grande', 
      precio: 65, 
      stock: 12, 
      idCategoria: 2,
      nombreCategoria: 'Accesorios',
      descripcion: 'Maceta de terracota natural hecha a mano.',
      rutaImagen: 'https://lh3.googleusercontent.com/aida-public/AB6AXuD2xb0JY2K4PjDHuRC0EhnDhCUgeSYVbJEi_q5G3vrdjPrKG_sFDh-Rz4d6CGvb20DxVCkv5C3BUIdcD7Qhf-tU7_3XzC4OlarcLng6Q3VpUkTBkd8NAu45kAZi721RD1HIng4Js3TKkgPBUvP3U8QfCP2e_h_HEHnai-ibqH89F0l4ENgKONE9gQZYM5uXVH_Uu0CWqegZd-Z2UX4OOFIQOG3bvNvtbRRAFrado7mIXAUg8doDz4p45g' 
    }
  ];

  constructor(private router: Router) {}

  irADetalle(producto: Producto): void {
    // Redirige al catálogo o vista de detalle pasando el ID del producto
    this.router.navigate(['/catalogo'], { queryParams: { productoId: producto.idProducto } });
  }
}
