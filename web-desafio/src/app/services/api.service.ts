import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  
  constructor() {}

  request(controller: string, method: string, param: any, queryable: boolean = false) {
    let request = environment.baseUrl + controller + "/" + method;

    if (queryable) request += this.queryable(param);
    else request += (param !== "" ? "/" + param : "")

    return request;
  }

  queryable(obj: any) {
    let query = '?';
    Object.keys(obj).forEach(k => query += `${k}=${obj[k]}&`);
    return query.substring(0, query.length - 1);
  }
}
