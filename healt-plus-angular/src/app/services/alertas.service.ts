import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class AlertasService {

  constructor() { }

  success(message: string, title: string = 'Éxito'): void {
    Swal.fire({
      icon: 'success',
      title: title,
      text: message,
      showConfirmButton: false,
      timer: 1500,
      customClass: {
        popup: 'small-alert'
      }
    });
  }

  error(message: string, title: string = 'Error'): void {
    Swal.fire({
      icon: 'error',
      title: title,
      text: message,
      customClass: {
        popup: 'small-alert'
      }
    });
  }

  confirmarEditar(message: string, title: string = '¿Estás seguro de editar esté registro?'): Promise<any> {
    return Swal.fire({
      title: title,
      text: message,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sí, confirmar',
      cancelButtonText: 'No, cancelar',
      customClass: {
        popup: 'small-alert'
      }
    });
  }

  confirmarEliminar(message: string, title: string = '¿Estás seguro de eliminar esté registro?'): Promise<any> {
    return Swal.fire({
      title: title,
      text: message,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sí, confirmar',
      cancelButtonText: 'No, cancelar',
      customClass: {
        popup: 'small-alert'
      }
    });
  }
}