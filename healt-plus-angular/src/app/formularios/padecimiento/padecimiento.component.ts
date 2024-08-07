import { Component } from '@angular/core';
import { PadecimientosService } from '../../services/padecimientos.service';
import { IPadecimientos } from '../../interfaces/padecimientos';
import Swal from 'sweetalert2';
import { AlertasService } from '../../services/alertas.service';

@Component({
  selector: 'app-padecimiento',
  templateUrl: './padecimiento.component.html',
  styleUrl: './padecimiento.component.css'
})
export class PadecimientoComponent {
  isEdit = false;
  idPadecimiento=0;
  nombre= '';
  listaPadecimientos: IPadecimientos[] = [];

  constructor(private _servicio: PadecimientosService, private _servicioA:AlertasService){
    this.cargarPadecimientos();
  }

  cargarPadecimientos() {
    this._servicio.getPadecimientos().subscribe({
      next: (data) => {
        this.listaPadecimientos = data;
      },
      error: (e) => {
        console.log(e);
      }
    });
  }

  obtenerPadecimiento(data: IPadecimientos){
    this.isEdit = true;
    this.nombre = data.nombre;    
    this.idPadecimiento = data.idPadecimiento ?? 0;
  }

  guardarOEditarPadecimiento() {
    if (this.isEdit) {
      this.editarPadecimiento();
    } else {
      this.agregarPadecimiento();
    }
  }

  agregarPadecimiento() {
    const nuevoPadecimiento: IPadecimientos = {
      nombre: this.nombre
    };

    this._servicio.agregarPadecimiento(nuevoPadecimiento).subscribe({
      next: (response) => {
        console.log(response);
        this._servicioA.success('Padecimiento agregado');
        this.cargarPadecimientos();
        this.limpiar();
      },
      error: (e) => {
        console.error('Error al agregar padecimiento:', e);
      }
    });
  }

  editarPadecimiento() {
    const nuevoPadecimiento: IPadecimientos = {
        idPadecimiento: this.idPadecimiento,
        nombre: this.nombre
      };
  
      this._servicio.modificarPadecimiento(nuevoPadecimiento).subscribe({
        next: (response) => {
          console.log(response);
          this._servicioA.success('Padecimiento modificado');
          this.cargarPadecimientos();
          this.limpiar();
        },
        error: (e) => {
          console.error('Error al editar padecimiento:', e);
        }
      });
    
  }

  eliminarPadecimiento(padecimiento: IPadecimientos){
    this._servicio.eliminarPadecimiento(padecimiento.idPadecimiento ?? 0).subscribe({
      next:(data) => {
        this._servicioA.success('Padecimiento eliminado');
        this.cargarPadecimientos();
      }, error: (e) => {console.log(e)}
    })
  }

  limpiar(){
    this.isEdit = false;
    this.idPadecimiento=0;
    this.nombre= '';
  }
}


