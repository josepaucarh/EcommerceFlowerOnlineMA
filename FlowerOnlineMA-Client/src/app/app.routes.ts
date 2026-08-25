import { Routes } from '@angular/router';
import {Home} from './pages/home/home';
import { Catalogo } from './pages/catalogo/catalogo';
import { Checkout } from './pages/checkout/checkout';
import { Perfil } from './pages/perfil/perfil';
import { AdminProductosComponent } from './pages/admin-productos/admin-productos';


export const routes: Routes = [
  { path: '', component: Home},
  { path: 'catalogo', component: Catalogo },
  { path: 'checkout', component:Checkout},
  { path: 'perfil', component:Perfil},
  { path: 'admin', component: AdminProductosComponent },
  { path: '**', redirectTo: '' }
];