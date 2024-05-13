import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit  {
  title = 'frontend-angular';

  constructor(private _basketService: BasketService, private accountService: AccountService)
  {

  }

  ngOnInit(): void {
    this.loadBasket();
    this.loadCurrentUser();
  }

  loadBasket() {
    this._basketService.getBasket();
  }

  loadCurrentUser() {
    this.accountService.loadCurrentUser().subscribe({
      error: err => {
        localStorage.removeItem('token');
      }
    });
  }
}
