import { Component } from '@angular/core';
import { IPersonaPaciente } from '../../interfaces/PersonaPaciente';
import { Router } from '@angular/router';
import { PacientesService } from '../../services/pacientes.service';
import { DashboardsService } from '../../services/dashboards.service';
import { Chart } from 'chart.js';

@Component({
  selector: 'app-vista-pacientes',
  templateUrl: './vista-pacientes.component.html',
  styleUrls: ['./vista-pacientes.component.css']
})
export class VistaPacientesComponent {
  nombreBuscar = '';
  fecha: Date = new Date();
  listaPacientes: IPersonaPaciente[] = [];
  isResultadoLoaded = false;
  isModalActiveGrafic = false;
  chart: any;
  pacienteSeleccionado: IPersonaPaciente | null = null; 

  constructor(
    private _servicio: PacientesService,
    private router: Router,
    private dashService: DashboardsService
  ) {
    this.cargarPacientes();
  }

  abrirModalGrafica(paciente: IPersonaPaciente): void {
    this.pacienteSeleccionado = paciente;
    this.isModalActiveGrafic = true;
  }

  buscarGrafica(): void {
    if (this.pacienteSeleccionado) {
      const idPaciente = String(this.pacienteSeleccionado.idPaciente);
      console.log(this.fecha, 'fech')

      if (this.chart) {
        this.chart.destroy();
      }

      this.dashService.obtenerRitmoDia(idPaciente, this.fecha).subscribe((data: any) => {
        console.log('Ritmos cardiacos:', data);

        const results = data.$values || data;

        if (Array.isArray(results)) {
          const labels = results.map((item: any) => {
            const fecha = new Date(item.fechaHora);
            const horas = fecha.getHours().toString().padStart(2, '0');
            const minutos = fecha.getMinutes().toString().padStart(2, '0');
            return `${horas}:${minutos}`;
          });
          const dataset = results.map((item: any) => item.ritmoCardiaco);

          this.chart = new Chart('MyChart', {
            type: 'line',
            data: {
              labels: labels,
              datasets: [{
                label: 'Ritmo Cardiaco',
                data: dataset,
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1,
                hoverBackgroundColor: 'rgba(75, 10, 192, 0.2)',
                hoverBorderColor: 'rgba(75, 10, 192, 1)'
              }]
            },
            options: {
              scales: {
                y: {
                  beginAtZero: true
                }
              }
            }
          });
        } else {
          console.error('La respuesta no es un array:', data);
        }
      });
    } else {
      console.error('No se ha seleccionado un paciente.');
    }
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
          console.log(this.listaPacientes);
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
        console.log(this.listaPacientes);
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
        console.log(this.listaPacientes);
      },
      error: (e) => {
        console.log(e);
      }
    });
  }

  cerrarPacientes(): void {
    this.isModalActiveGrafic = false;  // Cierra el modal
    
    if (this.chart) {
        this.chart.destroy();
        this.chart = null; // Limpiar la referencia
    }

    this.fecha = new Date(); // O puedes usar una fecha por defecto, por ejemplo, new Date() si lo prefieres
    
    // Limpiar el contenido del canvas (opcional, solo si es necesario)
    const canvas = document.getElementById('MyChart') as HTMLCanvasElement;
    if (canvas) {
        const ctx = canvas.getContext('2d');
        if (ctx) {
            ctx.clearRect(0, 0, canvas.width, canvas.height);
        }
    }
}

}
