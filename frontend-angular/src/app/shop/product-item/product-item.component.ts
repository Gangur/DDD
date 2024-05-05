import { Component, Input, OnInit } from '@angular/core';
import { ProductDto } from 'src/app/api/http-client';
import PictureUrl from '../../../tools/picturesurlfactory';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit {
  @Input() product?: ProductDto
  pictureUrl?: string;

  ngOnInit(): void {
    this.pictureUrl = PictureUrl(this.product?.pictureName);
  }
}
