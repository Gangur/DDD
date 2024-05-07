import { Component, OnInit } from '@angular/core';
import { Client, ProductDto } from '../../api/http-client';
import { ActivatedRoute } from '@angular/router';
import PictureUrl from '../../../tools/picturesurlfactory';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product?: ProductDto;

  pictureUrl?: string;

  constructor(private client: Client,
    private activatedRoute: ActivatedRoute,
    private bcService: BreadcrumbService)
  {
    this.bcService.set('@productDetails', ' ');
  }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id) {
      this.client.v1Products(id).subscribe({
        next: product => {
          {
            this.product = product;
            this.bcService.set('@productDetails', product.name!);
          };
          this.pictureUrl = PictureUrl(this.product?.pictureName);
        },
        error: error => console.log(error)
      });
    }
  }
}
