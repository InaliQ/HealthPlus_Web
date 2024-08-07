import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { AlertasService } from '../services/alertas.service';

@Component({
  selector: 'app-layout-main',
  standalone: true,
  imports: [RouterOutlet, CommonModule],
  templateUrl: './layout-main.component.html',
  styleUrl: './layout-main.component.css'
})
export class LayoutMainComponent {
  userName: string = '';
  backgroundColor: string = '';

  constructor(private _servicioA: AlertasService){  }
  private router = inject(Router);

  ngOnInit(): void {
    this.loadUserData();
    this.setRandomBackgroundColor();
  }

  setRandomBackgroundColor(): void {
    this.backgroundColor = '#' + Math.floor(Math.random()*16777215).toString(16);
  }

  private loadUserData(): void {
    const storedUser = localStorage.getItem('userName'); // Suponiendo que el nombre de usuario se guarda como 'userName'
    this.userName = storedUser || 'Usuario'; // Usa un valor por defecto si no hay nombre guardado
  }

  logout() {
    localStorage.removeItem('authToken');
    localStorage.removeItem('userName');
    
    this._servicioA.success('Has cerrado sesión exitosamente');

    this.router.navigate(['login']);
  }
}
