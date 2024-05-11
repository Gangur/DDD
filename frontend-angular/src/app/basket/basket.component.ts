import { Component } from '@angular/core';
import { BasketService } from './basket.service';
import PictureUrl from '../../tools/picturesurlfactory';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent {

  constructor(public basketService: BasketService) { }

  PictureUrl(pictureName: string) {
    return PictureUrl(pictureName);
  }

}
