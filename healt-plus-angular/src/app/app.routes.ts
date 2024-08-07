import { Routes } from '@angular/router';
import { EnfermeroComponent } from './formularios/enfermero/enfermero.component';
import { PacienteComponent } from './formularios/paciente/paciente.component';
import { VistaPacientesComponent } from './vistas/vista-pacientes/vista-pacientes.component';
import { LoginComponent } from './auth/login/login.component';
import { VistaEnfermerosComponent } from './vistas/vista-enfermeros/vista-enfermeros.component';
import { DashPacientesComponent } from './dashboards/dash-pacientes/dash-pacientes.component';
import { PadecimientoComponent } from './formularios/padecimiento/padecimiento.component';
import { HorariosComponent } from './formularios/horarios/horarios.component';
import { LayoutMainComponent } from './layout-main/layout-main.component';
import { LayoutAuthComponent } from './layout-auth/layout-auth.component';
import { AuthGuard } from './auth/auth';

export const routes: Routes = [
    {path: 'health', component: LayoutMainComponent,
        canActivate: [AuthGuard],
        children: [
            {path: 'enfermeros', component: EnfermeroComponent},
            {path: 'pacientes', component: PacienteComponent},
            {path: 'padecimientos', component: PadecimientoComponent},
            {path: 'horarios', component: HorariosComponent},
            {path: 'verpacientes', component: VistaPacientesComponent},
            {path: 'verenfermeros', component: VistaEnfermerosComponent},
            {path: 'dashP', component: DashPacientesComponent},
        ]
    },
    {path: '',
        component: LayoutAuthComponent,
        children: [
            {path: 'login', component: LoginComponent},
            {path: '', redirectTo: 'login', pathMatch: 'full' },
        ]
},
    { path: '**', redirectTo: 'login' } 
];

export class AppRoutingModule { }

