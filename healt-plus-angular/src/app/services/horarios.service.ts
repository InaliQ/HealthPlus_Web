import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IHorario } from '../interfaces/horario';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HorariosService {

  private _endpoint: string = environment.endPoint;
  private _apiUrl: string = this._endpoint + "Healt/";
  
  constructor(private http: HttpClient) { }

  // ------------- GET HORARIOS -------------
  horarios(): Observable<IHorario[]>{
    return this.http.get<IHorario[]>(`${this._apiUrl}ListarHorarios`);
  }

  // ------------- POST AGREGAR HORARIOS -------------
  agregarHorario(horario: IHorario): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.post<any>(`${this._apiUrl}AgregarHorario`, horario, { headers });
  }

   // ------------- PUT MODIFICAR HORARIOS -------------
  modificarHorario(horario: IHorario): Observable<void> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.put<void>(`${this._apiUrl}ModificarHorario/${horario.idHorario}`, horario, { headers });
  }

   // ------------- DELETE ELIMINAR HORARIOS -------------
  eliminarHorario(idHorario: number): Observable<void> {
    return this.http.delete<void>(`${this._apiUrl}EliminarHorarioo/${idHorario}`);
  }

}
