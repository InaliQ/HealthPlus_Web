import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { PersonaEnfermero } from '../interfaces/PersonaEnfermero';
import { IHorario } from '../interfaces/horario';
import { Observable } from 'rxjs';
import { IPersonaPaciente } from '../interfaces/PersonaPaciente';
import { IPacienteEnfermero } from '../interfaces/PacienteEnfermero';

@Injectable({
  providedIn: 'root'
})
export class EnfermerosService {

  private _endpoint: string = environment.endPoint;
  private _apiUrl: string = this._endpoint + "Healt/";
  
  constructor(private http: HttpClient) { }

  private enfermero: PersonaEnfermero | null = null;

  setEnfermero(enfermero: PersonaEnfermero): void {
    this.enfermero = enfermero;
  }

  getEnfermero(): PersonaEnfermero | null {
    return this.enfermero;
  }

  //---------------- GET BUSCAR ENFERMERO POR NOMBRE ------------
  buscarNombre(nombre: string): Observable<PersonaEnfermero[]>{
    return this.http.get<PersonaEnfermero[]>(`${this._apiUrl}BuscarEPorNombre?nombre=${nombre}`);
  }

  // ------------- GET ENFERMEROS -------------
  getEnfermeros(): Observable<PersonaEnfermero[]>{
    return this.http.get<PersonaEnfermero[]>(`${this._apiUrl}ListarEnfermeros`);
  }

  // ------------- POST AGREGAR ENFERMERO -------------
  agregarEnfermero(enfermero: PersonaEnfermero): Observable<any> {
      const headers = new HttpHeaders({
        'Content-Type': 'application/json'
      });
      return this.http.post<any>(`${this._apiUrl}AgregarEnfermero`, enfermero, { headers });
  }

  // ------------- PUT MODIFICAR ENFERMERO -------------
  modificarEnfermero(enfermero: PersonaEnfermero): Observable<void> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.put<void>(`${this._apiUrl}ModificarEnfermero/${enfermero.idEnfermero}`, enfermero, { headers });
  }
  
  // ------------- PUT MODIFICAR PACIENTE -------------
  modificarEstatusPaciente(id: number | any, paciente: IPersonaPaciente): Observable<any> {
    return this.http.put<any>(`${this._apiUrl}ModificarEstatusPaciente/${id}`, paciente);
  }

  // ------------- POST AGREGAR ENFERMERO -------------
  agregarPacienteEnfermero(enfermero: IPacienteEnfermero): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.post<any>(`${this._apiUrl}AgregarPacienteEnfermero`, enfermero, { headers });
  }


   // ------------- GET PACIENTES POR ENFERMERO -------------
  getPacientesPorEnfermero(idEnfermero: number): Observable<any> {
    return this.http.get<any>(`${this._apiUrl}VerPacientesXEnfermero/${idEnfermero}`);
  }


}





  

  

