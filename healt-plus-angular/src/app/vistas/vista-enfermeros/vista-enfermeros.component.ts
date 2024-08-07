import { Component } from '@angular/core';
import { PersonaEnfermero } from '../../interfaces/PersonaEnfermero';
import { IPersonaPaciente } from '../../interfaces/PersonaPaciente';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';
import { EnfermerosService } from '../../services/enfermeros.service';
import { IPacienteEnfermero } from '../../interfaces/PacienteEnfermero';
import { PacientesService } from '../../services/pacientes.service';
import { AlertasService } from '../../services/alertas.service';

@Component({
  selector: 'app-vista-enfermeros',
  templateUrl: './vista-enfermeros.component.html',
  styleUrls: ['./vista-enfermeros.component.css']
})
export class VistaEnfermerosComponent {
  nombreBuscar= '';
  listaEnfermeros: PersonaEnfermero[] = [];
  listaPacientes: IPersonaPaciente[] = [];
  pacientes: IPersonaPaciente[] = [];
  isModalActive: boolean = false;
  isModalActive2: boolean = false;
  pacienteSeleccionado: number = 0; 
  enfermeroSeleccionado: number = 0;

  constructor(private _servicio: EnfermerosService, private router: Router, 
    private _servicioP: PacientesService,
    private _servicioA: AlertasService
  ){
    this.cargarEnfermeros();
    this.cargarPacientes();
  }

  buscarPorNombre() {
    if (this.nombreBuscar === '') {
        this.cargarPacientes();
        return;
    } else {
      this._servicio.buscarNombre(this.nombreBuscar).subscribe({
        next: (data) => {
          this.listaEnfermeros = data;
          console.log(this.listaPacientes)
        },
        error: (e) => {
          console.log(e);
        }
      });
    }
  }

  togglePacienteInfo(enfermero: PersonaEnfermero) {
    enfermero.showInfo = !enfermero.showInfo;
    enfermero.isExpanded = !enfermero.isExpanded;
  }

  irEnfermeros(route: string, enfermero: PersonaEnfermero) {
    this._servicio.setEnfermero(enfermero);
    this.router.navigate([route]);
  }

  cargarEnfermeros() {
    this._servicio.getEnfermeros().subscribe({
      next: (data) => {
        this.listaEnfermeros = data;
      },
      error: (e) => {
        console.log(e);
      }
    });
  }

  verPacientes(idEnfermero: any): void {
    this.enfermeroSeleccionado = idEnfermero; 
    this._servicio.getPacientesPorEnfermero(idEnfermero).subscribe(
      data => {
        this.listaPacientes = data; 
        console.log(this.listaPacientes, 'lista')
        this.isModalActive = true;
      },
      error => {
        console.error('Error al obtener pacientes', error);
      }
    );
  }
  
  cambiarEstatus(paciente: IPersonaPaciente, event: Event): void {
    const input = event.target as HTMLInputElement;
    const nuevoEstatus = input.checked;
  
    const pacienteEditado: IPersonaPaciente = {
      ...paciente,
      estatus: nuevoEstatus
    };
  
    console.log('Datos enviados para modificar estatus:', pacienteEditado);
  
    this._servicio.modificarEstatusPaciente(pacienteEditado.idPaciente, pacienteEditado).subscribe({
      next: (response) => {
        console.error(response);
        this._servicioA.success('Estatus modificado') ;
      },
      error: (e) => {
        console.error('Error al cambiar estatus', e);
      }
    });
  }
  
  cargarPacientes() {
    this._servicioP.getPacientes().subscribe({
      next: (data) => {
        this.pacientes = data;
      },
      error: (e) => {
        console.log(e);
      }
    });
  }

  agregarPacienteEnfermero() {
    const nuevoPacienteEnfermero: IPacienteEnfermero = {
      idEnfermero: this.enfermeroSeleccionado, 
      idPaciente: this.pacienteSeleccionado,   
    };
    console.log('Datos a enviar:', nuevoPacienteEnfermero);

    this._servicio.agregarPacienteEnfermero(nuevoPacienteEnfermero).subscribe({
      next: (response) => {
        console.error(response);
        this._servicioA.success('Paciente asignado') ;
        this.cerrarPacientes(); 
        this.verPacientes(nuevoPacienteEnfermero.idEnfermero);
      },
      error: (e) => {
        console.error('Error al asignar paciente:', e);
      }
    });
  }

  closeModal(): void {
    this.isModalActive = false;  
  }

  abrir(): void {
    this.isModalActive2 = true;  
  }

  cerrarPacientes(): void {
    this.isModalActive2 = false;  
  }
}
