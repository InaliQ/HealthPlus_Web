import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { IPadecimientos } from '../interfaces/padecimientos';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PadecimientosService {

  private _endpoint: string = environment.endPoint;
  private _apiUrl: string = this._endpoint + "Healt/";
  
  constructor(private http: HttpClient) { }

  // ------------- GET PADECIMIENTOS -------------
  getPadecimientos(): Observable<IPadecimientos[]>{
    return this.http.get<IPadecimientos[]>(`${this._apiUrl}ListarPadecimientos`);
  }

  // ------------- POST AGREGAR PADECIMIENTO -------------
  agregarPadecimiento(padecimiento: IPadecimientos): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.post<any>(`${this._apiUrl}AgregarPadecimientos`, padecimiento, { headers });
  }

   // ------------- PUT MODIFICAR PADECIMIENTO -------------
  modificarPadecimiento(padecimiento: IPadecimientos): Observable<void> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.put<void>(`${this._apiUrl}ModificarPadecimiento/${padecimiento.idPadecimiento}`, padecimiento, { headers });
  }

   // ------------- DELETE ELIMINAR PADECIMIENTO -------------
  eliminarPadecimiento(idPadecimiento: number): Observable<void> {
    return this.http.delete<void>(`${this._apiUrl}EliminarPadecimiento/${idPadecimiento}`);
  }

}
