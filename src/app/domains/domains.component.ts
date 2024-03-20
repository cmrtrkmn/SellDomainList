import { Component, OnInit } from '@angular/core';
import { Model,Domain } from '../Model';
import { DomainService } from '../domain.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'domains',
  templateUrl: './domains.component.html',
  styleUrl: './domains.component.css'
})
export class DomainsComponent implements OnInit{
  domains:Domain[] =[];
  constructor(private domainService:DomainService){}
 

  ngOnInit():void{
   // this.getDomains(); Ekran ilk açıldığında boş gelecek. Arama yapıldı
  } 

  getDomains(){
  this.domainService.getDomains().subscribe(domains=>
  {
    this.domains=domains;
  })
  } 
   
  searchDomainName(domain:string)
  {this.domainService.searchDomainName(domain).subscribe(domains=>
    {
      this.domains=domains;
    })    
  }
}
