import { Routes } from '@angular/router';
import { Catalogo } from './components/catalogo/catalogo';
import { AdminProductosComponent } from './components/admin-productos/admin-productos';

export const routes: Routes = [
  { path: 'catalogo', component: Catalogo },
  { path: 'admin', component: AdminProductosComponent },
  { path: '', redirectTo: 'catalogo', pathMatch: 'full' }
];