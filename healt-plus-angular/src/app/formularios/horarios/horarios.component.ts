import { Component, ɵbypassSanitizationTrustResourceUrl } from '@angular/core';
import { IHorario } from '../../interfaces/horario';
import { HorariosService } from '../../services/horarios.service';
import { AlertasService } from '../../services/alertas.service';

@Component({
  selector: 'app-horarios',
  templateUrl: './horarios.component.html',
  styleUrl: './horarios.component.css'
})
export class HorariosComponent {
  formulario = false;
  isEdit = false;
  idHorario=0;
  horaInicio= '';
  horaFin= '';
  listaHorarios: IHorario[] = [];

  constructor(private _servicio: HorariosService, private _servicioA: AlertasService){
    this.cargarHorarios();
  }

  cargarHorarios() {
    this._servicio.horarios().subscribe({
      next: (data) => {
        this.listaHorarios = data;
      },
      error: (e) => {
        console.log(e);
      }
    });
  }

  obtenerHorario(data: IHorario){
    this.isEdit = true;
    this.horaInicio = data.horaInicio;    
    this.horaFin = data.horaFin;    
    this.idHorario = data.idHorario ?? 0;
  }

  guardarOEditarHorario() {
    this.formulario = true;
    
    if (this.ValidarHoraInicio || this.ValidarHoraFim) {
      this._servicioA.error('Por favor, completa todos los campos obligatorios.');
      return; 
    }
  
    if (this.isEdit) {
      this._servicioA.confirmarEditar('Se guardarán los cambios').then((result) => {
        if (result.isConfirmed) {
          this.editarHorario();
        }
      });
    } else {
      this.agregarHorario();
    }
  }

  agregarHorario() {
    const nuevoHorario: IHorario = {
      horaInicio: this.horaInicio,
      horaFin: this.horaFin
    };

    this._servicio.agregarHorario(nuevoHorario).subscribe({
      next: (response) => {
        console.log(response);
        this._servicioA.success('Horario agregado');
        this.cargarHorarios();
        this.limpiar();
      },
      error: (e) => {
        console.error('Error al agregar horario:', e);
      }
    });
  }

  editarHorario() {
    this._servicioA.confirmarEditar('Se guardarán los cambios. ¿Deseas continuar?').then((result) => {
      if (result.isConfirmed) {
        const nuevoHorario: IHorario = {
          idHorario: this.idHorario,
          horaInicio: this.horaInicio,
          horaFin: this.horaFin
        };
        this._servicio.modificarHorario(nuevoHorario).subscribe({
          next: (response) => {
            console.log(response);
            this._servicioA.success('Horario modificado');
            this.cargarHorarios();
            this.limpiar();
          },
          error: (e) => {
            console.error('Error al editar horario:', e);
            this._servicioA.error('No se pudo editar el horario');
          }
        });
      }
    });
  }

  

  eliminarHorario(horario: IHorario){
    this._servicio.eliminarHorario(horario.idHorario ?? 0).subscribe({
      next:(data) => {
        this.cargarHorarios();
      }, error: (e) => {console.log(e)}
    })
  }

  get ValidarHoraInicio() {
    return this.formulario && this.horaInicio.trim().length === 0;
  }

  get ValidarHoraFim() {
    return this.formulario && this.horaFin.trim().length === 0;
  }

  limpiar() {
    this.formulario = false;
    this.idHorario = 0;
    this.horaInicio = '';
    this.horaFin = '';
    this.isEdit = false;
  }
}


