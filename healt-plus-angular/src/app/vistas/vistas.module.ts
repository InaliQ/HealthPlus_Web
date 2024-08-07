import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { VistaPacientesComponent } from './vista-pacientes/vista-pacientes.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { VistaEnfermerosComponent } from './vista-enfermeros/vista-enfermeros.component';
import { Router } from '@angular/router';
import { PacientesService } from '../services/pacientes.service';
import { EnfermerosService } from '../services/enfermeros.service';


@NgModule({
  declarations: [
    VistaPacientesComponent,
    VistaEnfermerosComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule
  ],
  exports:[
    VistaPacientesComponent
  ],
  providers:[
    PacientesService,
    EnfermerosService
  ]
})
export class VistasModule { }
