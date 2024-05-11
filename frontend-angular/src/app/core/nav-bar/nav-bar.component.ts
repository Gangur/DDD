import { Component } from '@angular/core';
import { BasketService } from '../../basket/basket.service';
import { LineItemDto } from '../../api/http-client';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {

  constructor(public basketService: BasketService) { }

  getCount(items: LineItemDto[]) {
    return items.reduce((sum, item) => sum + item.quantity, 0)
  }
}
