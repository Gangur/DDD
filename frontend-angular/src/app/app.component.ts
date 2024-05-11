import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit  {
  title = 'frontend-angular';

  constructor(private _basketService: BasketService)
  {

  }

  ngOnInit(): void {
    let basket = this._basketService.getBasket();
  }
}
