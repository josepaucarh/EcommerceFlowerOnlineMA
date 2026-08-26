import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ProductoCard } from '../../components/producto-card/producto-card';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule,RouterLink,ProductoCard],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
  productos=[
    { id: 1, nombre: 'Monstera Deliciosa', precio: 85, etiqueta: 'Best Seller', imagenUrl: 'https://lh3.googleusercontent.com/aida-public/AB6AXuARaqs6z4_l0lQdvFRcp65mNoWBDbP38dERPgGw8oReLoDp3hhvOv8Io8Ds2UZ3zQ3aznF0heCzipx98bLQ5LqJCRddINHblsCrSGaM1US2PDZgQeEzWTaC7PbNcOCx1cqHJ-MWiPyTeUFokwIz-yrcY_-qHoBr-jkP54JgVwuw30apq319Kk_lKNsYnbCuZRJakPveZKPJ-3BhGa6CupnatGYyeb9hJsUdTDIbgO5JJw3GRv4RFo5fuQ' },
    { id: 2, nombre: 'Sansevieria', precio: 45, imagenUrl: 'https://lh3.googleusercontent.com/aida-public/AB6AXuAEn1aX98IynEeAzWfW7cpOZQVMFtPumyDZ3L-NNc4R7kMUsvTgng1gf6uCOSM7Wa4cws_zDjHGZTpYC53boGGDhffuV8oKCyPsc1U_mRhksp2adr-42hsoTnqRRQSMYj1oVvai78c6HSS9NL_WtolDgAXSwM3_367tv_MAPAkFOqr9ViWjC_3_NOqUKv9rAPgtc5FxhZx424h7BqxIzRkQtxaYnQYwMfdXwRgh3xJWQbc7JU4HCzo5Aw' },
    { id: 3, nombre: 'Ficus Lyrata', precio: 120, etiqueta: 'New', imagenUrl: 'https://lh3.googleusercontent.com/aida-public/AB6AXuCNjQ9tmC95hBo59NzKt71ki-oZ1EViMuCCXv6gpnpHM9edTqhYAfiQUjJfU8Z1g4qNWpUKIvNXa4hAdsiTskSL_uGechEICw9SZkoqhSxy0cUsFhP-qK192M_lqNEuoGJrzb7WQMApICeHN7u2xHy7AD7TVxrLTKfjx6qAyuwMguNhJD7hbHDxpN91iC_1bbCZgEeA42ZL4EguexcT5jcnlyvSxYlnWwiF6TztMmTI3wnY8rarXO9CBw' },
    { id: 4, nombre: 'Maceta de Barro Grande', precio: 65, imagenUrl: 'https://lh3.googleusercontent.com/aida-public/AB6AXuD2xb0JY2K4PjDHuRC0EhnDhCUgeSYVbJEi_q5G3vrdjPrKG_sFDh-Rz4d6CGvb20DxVCkv5C3BUIdcD7Qhf-tU7_3XzC4OlarcLng6Q3VpUkTBkd8NAu45kAZi721RD1HIng4Js3TKkgPBUvP3U8QfCP2e_h_HEHnai-ibqH89F0l4ENgKONE9gQZYM5uXVH_Uu0CWqegZd-Z2UX4OOFIQOG3bvNvtbRRAFrado7mIXAUg8doDz4p45g' }
  ];
}
