import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IPacientePadecimiento } from '../interfaces/PacientePadecimiento';
import { IMonitoreoSalud } from '../interfaces/MonitoreSalud';

@Injectable({
  providedIn: 'root'
})
export class DashboardsService {

  private _endpoint: string = environment.endPoint;
  private _apiUrl: string = this._endpoint + "Healt/";
  
  constructor(private http: HttpClient) { }  

   // ------------- GET PACIENTES POR PADECIMIENTO -------------
  getPacientesPorPadecimiento(): Observable<IPacientePadecimiento[]> {
    return this.http.get<IPacientePadecimiento[]>(`${this._apiUrl}PacientesXPadecimiento`);
  }

   // ------------- GET PACIENTES POR PADECIMIENTO -------------
   getPacientesPorEdad(): Observable<{ edad: number, cantidad: number }[]> {
    return this.http.get<{ edad: number, cantidad: number }[]>(`${this._apiUrl}PacientesPorEdad`);
  }

    //---------------- GET BUSCAR RITMOS PACIENTE POR DÍA ------------
    obtenerRitmoDia(idPaciente: string, fecha: Date): Observable<IMonitoreoSalud[]> {
    
      return this.http.get<IMonitoreoSalud[]>(`${this._apiUrl}RitmoDia/${idPaciente}/${fecha}`);
    }
}