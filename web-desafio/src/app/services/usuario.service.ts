import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiService } from './api.service';
import { take, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  constructor(
    private http: HttpClient,
    private api: ApiService
  ) { }

  porId(id: number): Observable<any> {
    return this.http.get(`${environment.baseUrl}/usuario/${id}`)
      .pipe(
        take(1),
        map(result => result)
      );
  }

  porFiltro(filtro: any): Observable<any> {
    return this.http.get(this.api.request('/usuario', 'filtro', filtro, true))
      .pipe(
        take(1),
        map(result => result)
      );
  }

  buscarTodos(): Observable<any> {
    return this.http.get(`${environment.baseUrl}/usuario`)
      .pipe(
        take(1),
        map(result => result)
      );
  }

  cadastrar(usuario: any): Observable<any> {
    return this.http.post(`${environment.baseUrl}/usuario`, usuario)
      .pipe(
        take(1),
        map(result => result)
      );
  }

  deletar(id: number): Observable<any> {
    return this.http.delete(`${environment.baseUrl}/usuario/${id}`)
      .pipe(
        take(1),
        map(result => result)
      );
  }

  editar(usuario: any): Observable<any> {
    return this.http.put(`${environment.baseUrl}/usuario`, usuario)
      .pipe(
        take(1),
        map(result => result)
      );
  }
}
