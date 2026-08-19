import { Component} from '@angular/core';
import { AdminProductosComponent } from './components/admin-productos/admin-productos';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [AdminProductosComponent],
  template: `<app-admin-productos></app-admin-productos>`,
})
export class App {
  title = 'FlowerOnlineMA-Client';
}