import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ProductDto } from '../api/http-client';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:44370/v1/';

  constructor(private http: HttpClient) {

   }

   getProducts() {
     return this.http.get<ProductDto[]>(this.baseUrl + 'products/list')
   }
}
