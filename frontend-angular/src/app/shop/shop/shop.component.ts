import { Component, OnInit } from '@angular/core';
import { ProductDto } from 'src/app/api/http-client';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})

export class ShopComponent implements OnInit {
  products: ProductDto[] = [];

  constructor(private shopService: ShopService) {

  }

  ngOnInit(): void {
    this.shopService.getProducts().subscribe({
      next: respose => this.products = respose,
      error: error => console.log(error)
    });
  }
}
