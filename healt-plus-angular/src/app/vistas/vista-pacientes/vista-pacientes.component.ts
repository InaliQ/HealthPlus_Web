import { Component } from '@angular/core';
import { IPaciente } from '../../interfaces/pacientes';
import { IPersonaPaciente } from '../../interfaces/PersonaPaciente';
import { Router } from '@angular/router';
import { PacientesService } from '../../services/pacientes.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-vista-pacientes',
  templateUrl: './vista-pacientes.component.html',
  styleUrl: './vista-pacientes.component.css'
})
export class VistaPacientesComponent {
  nombreBuscar= '';
  listaPacientes: IPersonaPaciente[] = [];
  isResultadoLoaded = false;

  constructor(private _servicio: PacientesService, private router: Router) { 
    this.cargarPacientes();
  }

  togglePacienteInfo(paciente: IPersonaPaciente) {
    paciente.showInfo = !paciente.showInfo;
    paciente.isExpanded = !paciente.isExpanded;
  }

  navigateTo(route: string, paciente: IPersonaPaciente) {
    this._servicio.setPaciente(paciente);
    this.router.navigate([route]);
  }

  buscarPorNombre() {
    if (this.nombreBuscar === '') {
        this.cargarPacientes();
        return;
    } else {
      this._servicio.buscarNombre(this.nombreBuscar).subscribe({
        next: (data) => {
          this.listaPacientes = data;
          this.isResultadoLoaded = true;
          console.log(this.listaPacientes)
        },
        error: (e) => {
          console.log(e);
        }
      });
    }
  }

  buscarEstatus(event: Event): void {
    const input = event.target as HTMLInputElement;
    const nuevoEstatus = input.checked;
  
  this._servicio.buscarEstatus(nuevoEstatus).subscribe({
    next: (data) => {
      this.listaPacientes = data;
      this.isResultadoLoaded = true;
      console.log(this.listaPacientes)
    },
    error: (e) => {
      console.log(e);
    }
  });
  }

  cargarPacientes() {
    this._servicio.getPacientes().subscribe({
      next: (data) => {
        this.listaPacientes = data;
        this.isResultadoLoaded = true;
        console.log(this.listaPacientes)
      },
      error: (e) => {
        console.log(e);
      }
    });
  }
}
