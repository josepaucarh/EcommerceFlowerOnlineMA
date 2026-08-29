import { Component, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header {
  // Servicio de autenticación para validar si el usuario es Admin o está logueado
  public authService = inject(AuthService);

  // Estado reactivo para controlar la apertura/cierre del menú móvil (Drawer)
  public isDrawerOpen = signal<boolean>(false);

  public toggleDrawer(): void {
    this.isDrawerOpen.update((open) => !open);
  }
}
