import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { IUsuario } from '../../interfaces/usuario';
import { AuthService } from '../../services/auth.service';
import { AlertasService } from '../../services/alertas.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  user = '';
  Contrasenia= '';

  constructor(private _servicio: AuthService, private _servicioA: AlertasService){  }
  private router = inject(Router);

  
  login() {
    const usuario: IUsuario = {
      IdUsuario: 0,
      user: this.user,
      Contrasenia: this.Contrasenia,
    };

    console.log('Datos a enviar:', usuario);

    this._servicio.login(usuario).subscribe({
      next: (data) => {
        if (data.isSuccess) {
          // localStorge
          localStorage.setItem('authToken', data.token); 
          localStorage.setItem('userName', data.userName);
          this._servicioA.success('Inicio e sesión exitoso')
          this.router.navigate(['health/enfermeros'])
        }
      },
      error: (e) => {
        this._servicioA.error('Usuario o contraseña incorrecto')
        console.error('Error', e);
      }
    });
  }

}
