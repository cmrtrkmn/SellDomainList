import { Injectable } from '@angular/core';
import { Model,Domain } from './Model';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class DomainService {
  baseUrl:string="http://localhost:5001/";
  model=new Model();
  constructor(private http:HttpClient) { }
 
  getDomains():Observable<Domain[]>{
    return this.http.get<Domain[]>(this.baseUrl+'api/SellDomains');
  }
  searchDomainName(domain:string):Observable<Domain[]>
  {    
    return this.http.get<Domain[]>(this.baseUrl + 'api/selldomains/'+domain);
  }
  
   getDomainById(id:number){
    return this.model.domains.find(i=>i.domainId==id);
   }
}
