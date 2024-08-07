import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashPacientesComponent } from './dash-pacientes/dash-pacientes.component';
import { PacientesService } from '../services/pacientes.service';
import { EnfermerosService } from '../services/enfermeros.service';
import { DashboardsService } from '../services/dashboards.service';




@NgModule({
  declarations: [
    DashPacientesComponent
  ],
  imports: [
    CommonModule
  ],
  exports:
  [
    DashPacientesComponent
  ],providers:[
    PacientesService,
    EnfermerosService,
    DashboardsService
  ]
})
export class DashboardsModule { }
