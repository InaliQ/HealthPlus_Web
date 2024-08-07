import { Component, OnInit } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { DashboardsService } from '../../services/dashboards.service';
import { IPacientePadecimiento } from '../../interfaces/PacientePadecimiento';

Chart.register(...registerables);

@Component({
  selector: 'app-dash-pacientes',
  templateUrl: './dash-pacientes.component.html',
  styleUrls: ['./dash-pacientes.component.css']
})
export class DashPacientesComponent implements OnInit {
  chart: any;
  chart2: any;

  constructor(private dashService: DashboardsService) {}

  ngOnInit(): void {
    this.dashService.getPacientesPorPadecimiento().subscribe((data: IPacientePadecimiento[]) => {
      console.log('Pacientes por padecimiento:', data); 
      if (Array.isArray(data)) {
        const labels = data.map((item: IPacientePadecimiento) => item.padecimientoNombre);
        const dataset = data.map((item: IPacientePadecimiento) => item.cantidadPacientes);

        this.chart = new Chart('MyChart', {
          type: 'bar',
          data: {
            labels: labels,
            datasets: [{
              label: 'Cantidad de Pacientes',
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

    this.dashService.getPacientesPorEdad().subscribe((response: any) => {
      console.log('Pacientes por edad:', response);

      // Aquí se espera que 'response' contenga una estructura similar a { $values: [...] }
      const data = response.$values;

      if (Array.isArray(data)) {
        const labels = data.map((item: any) => `Edad ${item.edad}`);
        const dataset = data.map((item: any) => item.cantidadPacientes); // Asegúrate de usar 'cantidadPacientes' si eso es lo que necesitas

        this.chart2 = new Chart('MyChart2', {
          type: 'bar',
          data: {
            labels: labels,
            datasets: [{
              label: 'Cantidad de Pacientes por Edad',
              data: dataset,
              backgroundColor: 'rgba(197, 196, 16, 0.2)',
              borderColor: 'rgba(197, 196, 16, 1)',
              borderWidth: 1,
              hoverBackgroundColor: 'rgba(201, 12, 192, 0.2)',
              hoverBorderColor: 'rgba(201, 12, 192, 1)'
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
        console.error('La respuesta no es un array:', response);
      }
    });
  }
}
