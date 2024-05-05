import { Component, OnInit } from '@angular/core';
import { Client, ProductDto } from '../../api/http-client';
import { ActivatedRoute } from '@angular/router';
import PictureUrl from '../../../tools/picturesurlfactory';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product?: ProductDto;

  pictureUrl?: string;

  constructor(private client: Client, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id) {
      this.client.v1Products(id).subscribe({
        next: product => {
          this.product = product;
          this.pictureUrl = PictureUrl(this.product?.pictureName);
        },
        error: error => console.log(error)
      });
    }
  }
}
