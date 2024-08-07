  import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
  import { CommonModule } from '@angular/common';
  import { EnfermeroComponent } from './enfermero/enfermero.component';
  import { PacienteComponent } from './paciente/paciente.component';
  import { FormsModule } from '@angular/forms';
  import { HttpClientModule } from '@angular/common/http';
  import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
  import { PadecimientoComponent } from './padecimiento/padecimiento.component';
  import { PadecimientosService } from '../services/padecimientos.service';
import { HorariosComponent } from './horarios/horarios.component';
import { EnfermerosService } from '../services/enfermeros.service';
import { PacientesService } from '../services/pacientes.service';
import { HorariosService } from '../services/horarios.service';


  @NgModule({
    declarations: [
      EnfermeroComponent,
      PacienteComponent,
      PadecimientoComponent,
      HorariosComponent
    ],
    imports: [
      CommonModule,
      FormsModule,
      HttpClientModule,
      SweetAlert2Module.forRoot()
      
    ],
    exports: [
      EnfermeroComponent,
      PacienteComponent,
      PadecimientoComponent
    ],
    providers: [
      EnfermerosService,
      PacientesService,
      HorariosService,
      PadecimientosService
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
  })
  export class FormulariosModule { }



