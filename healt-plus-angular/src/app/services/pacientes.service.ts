import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IPersonaPaciente } from '../interfaces/PersonaPaciente';
import { Observable } from 'rxjs';
import { PersonaEnfermero } from '../interfaces/PersonaEnfermero';
import { IPadecimientos } from '../interfaces/padecimientos';

@Injectable({
  providedIn: 'root'
})
export class PacientesService {

  private _endpoint: string = environment.endPoint;
  private _apiUrl: string = this._endpoint + "Healt/";
  
  constructor(private http: HttpClient) { }

  private paciente: IPersonaPaciente | null = null;
  private enfermero: PersonaEnfermero | null = null;

  setPaciente(paciente: IPersonaPaciente): void {
    this.paciente = paciente;
  }

  getPaciente(): IPersonaPaciente | null {
    return this.paciente;
  }

  setEnfermero(enfermero: PersonaEnfermero): void {
    this.enfermero = enfermero;
  }

  getEnfermero(): PersonaEnfermero | null {
    return this.enfermero;
  }

  // ------------- GET PACIENTES -------------
  getPacientes(): Observable<IPersonaPaciente[]>{
    return this.http.get<IPersonaPaciente[]>(`${this._apiUrl}ListarPacientes`);
  }

  // ------------- POST AGREGAR PACIENTE -------------
  agregarPaciente(paciente: IPersonaPaciente): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.post<any>(`${this._apiUrl}AgregarPaciente`, paciente, { headers });
  }

   // ------------- PUT MODIFICAR PACIENTE -------------
  modificarPaciente(paciente: IPersonaPaciente): Observable<void> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.put<void>(`${this._apiUrl}ModificarPaciente/${paciente.idPaciente}`, paciente, { headers });
  }

  //---------------- GET BUSCAR PACIENTE POR NOMBRE ------------
  buscarNombre(nombre: string): Observable<IPersonaPaciente[]>{
    return this.http.get<IPersonaPaciente[]>(`${this._apiUrl}BuscarPorNombre?nombre=${nombre}`);
  }

  //---------------- GET BUSCAR PACIENTE POR NOMBRE ------------
  buscarEstatus(estatus: boolean): Observable<IPersonaPaciente[]>{
    return this.http.get<IPersonaPaciente[]>(`${this._apiUrl}BuscarXEstatus?estatus=${estatus}`);
  }
}
