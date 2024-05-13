import { Component } from '@angular/core';
import { BasketService } from '../../basket/basket.service';
import { LineItemDto } from '../../api/http-client';
import { AccountService } from '../../account/account.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {

  constructor(public basketService: BasketService, public accountService: AccountService)
  {

  }

  getCount(items: LineItemDto[]) {
    return items.reduce((sum, item) => sum + item.quantity, 0)
  }
}
